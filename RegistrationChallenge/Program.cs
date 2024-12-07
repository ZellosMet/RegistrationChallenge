using RegistrationChallenge.Api.Middleware;
using RegistrationChallenge.Model.Crypto;
using RegistrationChallenge.Model.DataBase;
using RegistrationChallenge.Model.Email;
using RegistrationChallenge.Model.Service;
using RegistrationChallenge.Model.Users;
using RegistrationChallenge.Stub;

var builder = WebApplication.CreateBuilder(args);

// сервисы приложения
builder.Services.AddControllers();
builder.Services.AddDbContext<UsersDBContext>();
builder.Services.AddTransient<IEmailService, SendEmailService>();
builder.Services.AddTransient<IUserStorage, DBUserService>();
builder.Services.AddKeyedTransient<IEncoder, EncodingToMD5>("api-key-encoder");
builder.Services.AddKeyedTransient<IEncoder, EncodingToBCrypt>("api-key-generator");
builder.Services.AddTransient<UserService>();


var app = builder.Build();

// маппинг контроллеров
app.MapControllers();

// добавление middleware
app.UseMiddleware<ErrorMiddleware>();


app.Run();
