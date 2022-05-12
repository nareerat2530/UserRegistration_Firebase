using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.EntityFrameworkCore;
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

FirebaseApp.Create(new AppOptions()
{
    Credential = GoogleCredential.FromFile(@"C:\Users\twish\source\firebase-with-dotnet-firebase-adminsdk-ncdij-ede0e5d681.json"),
});

//FirebaseApp.Create(new AppOptions()
//{
//    Credential = GoogleCredential.FromFile("path/to/refreshToken.json"),
//});


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
