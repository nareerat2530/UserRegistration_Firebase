using System.Security.Claims;
using System.Text.Encodings.Web;
using FirebaseAdmin;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using UserRegistration_Tutorial.Models.Users;

namespace UserRegistration_Tutorial.Authentication;

public class FirebaseAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly FirebaseApp _firebaseApp;
    private static FirestoreDb? _db;
    public static User User { get; set; } = new();


    public FirebaseAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,
        FirebaseApp firebaseApp, FirestoreDb? db) : base(options, logger, encoder, clock)
    {
        _firebaseApp = firebaseApp;
        _db = db;
    }

    public static async Task<User> GetUser()
    {
        var usersFromDb = _db.Collection("User");
        var snapshot = await usersFromDb.GetSnapshotAsync();
        var userList = snapshot.Documents.Select(x => x.ConvertTo<User>()).ToList();
        return userList.FirstOrDefault(u => u.Email == User.Email);
    }

    private void SetUser(IReadOnlyDictionary<string, object> claims)
    {
        User.Email = claims["email"].ToString()!;
        User.UserName = claims["name"].ToString()!;

        
    }
    

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Context.Request.Headers.ContainsKey("Authorization"))
        {
            return AuthenticateResult.NoResult();
        }
        string bearerToken = Context.Request.Headers["Authorization"];
        if (bearerToken == null || !bearerToken.StartsWith("Bearer ")) return AuthenticateResult.Fail("Invalid scheme");
        var token = bearerToken.Substring("Bearer ".Length);
        try
        {
            var firebaseToken = await FirebaseAuth.GetAuth(_firebaseApp).VerifyIdTokenAsync(token);
            SetUser(firebaseToken.Claims);
            return AuthenticateResult.Success(new AuthenticationTicket(new ClaimsPrincipal(new List<ClaimsIdentity>
            {
                new(ToClaims(firebaseToken.Claims), nameof(FirebaseAuthenticationHandler))
            }), JwtBearerDefaults.AuthenticationScheme));
        }
        catch (Exception ex)
        {
            return AuthenticateResult.Fail(ex);
        }
    }

    private IEnumerable<Claim> ToClaims(IReadOnlyDictionary<string, object> claims)
    {
        
        
        return new List<Claim>
        {
            new("uid", claims["user_id"].ToString()!), 
            new("email", claims["email"].ToString()!),
            new("name", claims["name"].ToString()!)
        };
    }
    
}