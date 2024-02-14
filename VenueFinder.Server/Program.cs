using MongoDB.Driver;
using VenueFinder.Application;
using VenueFinder.Application.Interfaces;
using VenueFinder.Application.Queries;
using VenueFinder.Domain.Repositories;
using VenueFinder.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

// MongoDB
builder.Services.AddSingleton<IMongoClient>(serviceProvider =>
{
    var settings = MongoClientSettings.FromConnectionString(
        builder.Configuration["MongoDbSettings:ConnectionString"]);
    return new MongoClient(settings);
});

builder.Services.AddScoped(serviceProvider =>
{
    var client = serviceProvider.GetRequiredService<IMongoClient>();
    return client.GetDatabase(builder.Configuration["MongoDbSettings:DatabaseName"]);
});

// Repositories
builder.Services.AddScoped<IUserRepository, MongoUserRepository>();
builder.Services.AddScoped<IVenueRepository, MongoVenueRepository>();
builder.Services.AddScoped<IVenueCategoryRepository, MongoVenueCategoryRepository>();

// Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IVenueService, VenueService>();
builder.Services.AddScoped<IVenueCategoryService, VenueCategoryService>();
builder.Services.AddScoped<ICoinmapService, CoinmapService>();

// GraphQL
builder.Services
    .AddGraphQLServer()
    .AddQueryType(d => d.Name("Query"))
        .AddTypeExtension<VenueQuery>()
        .AddTypeExtension<VenueCategoryQuery>();

// External API
builder.Services.AddHttpClient<CoinmapService>(client =>
{
    client.BaseAddress = new Uri("https://coinmap.org/");
});

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGraphQL();

app.MapFallbackToFile("/index.html");

app.Run();
