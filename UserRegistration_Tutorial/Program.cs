using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication;
using Microsoft.OpenApi.Models;
using UserRegistration_Tutorial.Authentication;
using UserRegistration_Tutorial.Helpers;
using UserRegistration_Tutorial.Interfaces;
using UserRegistration_Tutorial.Mapper;
using UserRegistration_Tutorial.Services;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<AppSetting>(builder.Configuration.GetSection("Appsettings"));
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<EventsMapper>();
builder.Services.AddScoped<UserMapper>();


builder.Services.AddSingleton(FirebaseApp.Create(
    new AppOptions
    {
        Credential =
            GoogleCredential.GetApplicationDefault()
    })
);
builder.Services.AddSingleton(_ => new FirestoreDbBuilder
{
    ProjectId = "firebase-with-dotnet",
    Credential =
        GoogleCredential.GetApplicationDefault()
    // <-- service account json file
}.Build());


builder.Services.AddAuthentication("FirebaseAuthentication")
    .AddScheme<AuthenticationSchemeOptions, FirebaseAuthenticationHandler>("FirebaseAuthentication", _ => { });

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
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

//app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();