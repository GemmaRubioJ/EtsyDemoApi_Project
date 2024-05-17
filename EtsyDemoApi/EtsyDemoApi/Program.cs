using Api.Infraestructura.Context;
using dotenv.net;
using Microsoft.EntityFrameworkCore;
using Api.Data.Repository.Queries;
using Api.Service.Queries;
using Api.Data.Repository.Queries.Contracts;
using Api.Data.Repository.Commands;
using Api.Service.Commands;
using Api.Data.Repository.Commands.Contracts;
using Api.Service.Commands.Contracts;
using Api.Service.Queries.Contracts;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Configura TLS 1.2 y TLS 1.3 para todas las conexiones HTTP salientes
ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;

// Add .env
DotEnv.Load();
var sendGridApiKey = Environment.GetEnvironmentVariable("EMAIL_APIKEY");
Console.WriteLine($"SendGrid API Key: {sendGridApiKey}");
var config = builder.Configuration;

//Add HttpClient
builder.Services.AddHttpClient("FakeStoreClient", client =>
{
    client.BaseAddress = new Uri("https://fakestoreapi.com/");
});

// Add DB
builder.Services.AddDbContext<ApiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BD_Etsy")));

//Add Services
builder.Services.AddTransient<IEtsyRepository, EtsyRepository>();
builder.Services.AddTransient<IEtsyQuery, EtsyQuery>();
builder.Services.AddTransient<ICartRepository, CartRepository>();
builder.Services.AddTransient<ICartQuery, CartQuery>();
builder.Services.AddTransient<ICartService, CartService>();
builder.Services.AddTransient<IGetCartService, GetCartService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IUserQuery, UserQuery>();
builder.Services.AddTransient<IGetEtsyService, GetEtsyService>();
builder.Services.AddScoped<IEmailService>(provider => {
    var logger = provider.GetRequiredService<ILogger<EmailService>>();
    return new EmailService(sendGridApiKey, logger);
});



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

// Configuración de CORS (FRONT ANGULAR)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("http://localhost:4200")
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});

//necesario para Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowSpecificOrigin");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
