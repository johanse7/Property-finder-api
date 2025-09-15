using DotNetEnv;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PropertyApp.Application.Mappings;
using PropertyApp.Infrastructure;
using PropertyApp.Infrastructure.Mappings;

var builder = WebApplication.CreateBuilder(args);

// Load variables from .env
Env.Load();

// Configure MongoDB from env
builder.Services.Configure<MongoDbSettings>(options =>
{
    options.ConnectionString = Environment.GetEnvironmentVariable("MONGO_URI") 
                               ?? throw new InvalidOperationException("MONGO_URI not set");
    options.DatabaseName = Environment.GetEnvironmentVariable("MONGO_DB") 
                           ?? throw new InvalidOperationException("MONGO_DB not set");
});

builder.Services.AddSingleton(sp => {
    var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    var client = new MongoClient(settings.ConnectionString);
    return client.GetDatabase(settings.DatabaseName);
});

// Repositories
builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();
builder.Services.AddScoped<IOwnerRepository, OwnerRepository>();
builder.Services.AddScoped<IPropertyImageRepository, PropertyImageRepository>();
builder.Services.AddScoped<IPropertyTraceRepository, PropertyTraceRepository>();

// Use cases
builder.Services.AddScoped<GetPropertiesUseCase>();
builder.Services.AddScoped<GetPropertyDetailsUseCase>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// AutoMapper
builder.Services.AddAutoMapper(typeof(InfrastructureProfile).Assembly);
builder.Services.AddAutoMapper(typeof(ApplicationProfile).Assembly);

var app = builder.Build();


app.UseMiddleware<PropertyApp.Api.Middlewares.ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();
