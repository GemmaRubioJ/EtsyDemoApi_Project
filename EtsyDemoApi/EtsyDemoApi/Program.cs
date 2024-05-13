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

var builder = WebApplication.CreateBuilder(args);

// Add .env
DotEnv.Load();
var config = builder.Configuration;

// Add services to the container.

// Add Configuración Cliente HTTP
//builder.Services.AddHttpClient("etsyClient", client =>
//{
//    client.BaseAddress = new Uri("https://openapi.etsy.com/v3/application/");
//    // Configura aquí otros aspectos como headers si es necesario
//});
builder.Services.AddHttpClient("FakeStoreClient", client =>
{
    client.BaseAddress = new Uri("https://fakestoreapi.com/");
});

// Add DB
builder.Services.AddDbContext<ApiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BD_Etsy")));


//Add EtsyService
builder.Services.AddTransient<IEtsyRepository, EtsyRepository>();
builder.Services.AddTransient<IEtsyQuery, EtsyQuery>();
builder.Services.AddTransient<ICartRepository, CartRepository>();
builder.Services.AddTransient<ICartService, CartService>();
builder.Services.AddTransient<ICreateEtsyService, CreateEtsyService>();
builder.Services.AddTransient<IUserQuery, UserQuery>();
builder.Services.AddTransient<IGetEtsyService, GetEtsyService>();


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
app.UseCors("AllowSpecificOrigin");

app.UseAuthorization();

app.MapControllers();

app.Run();
