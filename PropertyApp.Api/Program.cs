using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PropertyApp.Application.Mappings;
using PropertyApp.Infrastructure;
using PropertyApp.Infrastructure.Mappings;



var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));

builder.Services.AddSingleton(sp => {
    var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    var client = new MongoClient(settings.ConnectionString);
    return client.GetDatabase(settings.DatabaseName);
});

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
builder.Services.AddAutoMapper(cfg =>
{

}, typeof(InfrastructureProfile).Assembly);

builder.Services.AddAutoMapper(cfg =>
{
    
}, typeof(ApplicationProfile).Assembly);


var app = builder.Build();
if (app.Environment.IsDevelopment()) { app.UseSwagger(); app.UseSwaggerUI(); }
app.MapControllers();
app.Run();
