using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using UserRegistration_Tutorial.Authorization;
using UserRegistration_Tutorial.Helpers;
using UserRegistration_Tutorial.Interfaces;
using UserRegistration_Tutorial.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.Configure<AppSetting>(builder.Configuration.GetSection("Appsetting"));
builder.Services.AddScoped<IUserService,UserService>();
builder.Services.AddScoped<IJwtUtils, JwtUtils>();

FirebaseApp.Create(new AppOptions()
{
    Credential = GoogleCredential.FromFile(@"C:\Users\twish\source\firebase-with-dotnet-firebase-adminsdk-ncdij-ede0e5d681.json"),
    ProjectId  = "firebase-with-dotnet",
});
builder.Services.AddSwaggerGen(options => {
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   //var firebaseProjectName = builder.Configuration["Firebase-dotnet"];
                   options.Authority =
                           "https://securetoken.google.com/firebase-with-dotnet";
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuer = true,
                       ValidIssuer = "https://securetoken.google.com/firebase-with-dotnet",
                       ValidateAudience = true,
                       ValidAudience = "firebase-with-dotnet",
                       ValidateLifetime = true
                   };
               });




var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(x => x
       .AllowAnyOrigin()
       .AllowAnyMethod()
       .AllowAnyHeader());

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();
//app.UseMiddleware<JwtMiddleware>();

app.MapControllers();

app.Run();
