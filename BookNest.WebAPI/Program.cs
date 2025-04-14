using BookNest.WebAPI.BusinessLogic.Implementations;
using BookNest.WebAPI.BusinessLogic.Interfaces;
using BookNest.WebAPI.DataAccess.HttpClients;
using BookNest.WebAPI.DataAccess.Implementations;
using BookNest.WebAPI.DataAccess.Interfaces;
using BookNest.WebAPI.Middleware;
using BookNest.WebAPI.Models.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// DI Services
builder.Services.AddHttpClient("ElasticClient").ConfigurePrimaryHttpMessageHandler(() =>
{
    return new HttpClientHandler
    {
        // This line disables certificate validation (ONLY do this for internal services)
        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    };
});
builder.Services.Configure<ElasticConfiguration>(builder.Configuration.GetSection("ElasticSearch"));
builder.Services.AddScoped<IHttpClientHelper, HttpClientHelper>();
builder.Services.AddScoped<IProductServiceBL, ProductServiceBL>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductFilterServiceBL, ProductFilterServiceBL>();
builder.Services.AddScoped<IProductFilterService, ProductFilterService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
