using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using MotorcycleRentals.src.API.Extensions;
using MotorcycleRentals.src.Infrastructure.Messaging.RabbitMQ;
using MotorcycleRentals.src.Infrastructure.Persistence.MongoDb;
using MotorcycleRentals.src.Infrastructure.Persistence.Repositories;
using MotorcycleRentals.src.API.Middlewares;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.OperationFilter<FileUploadOperationFilter>();
});

builder.Services.AddMongoDb(builder.Configuration);

builder.Services.AddRabbitMqMessaging(builder.Configuration);

builder.Services.AddHostedService<MotorcycleCreatedConsumerService>();

BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

// Repositories
builder.Services.AddRepositories();

// Services
builder.Services.AddAppServices();

// ... configuração de DI
builder.Services.AddSingleton<MongoDbContext>();

builder.Services.AddScoped<IMotorcycleNotificationRepository, MotorcycleNotificationRepository>();
builder.Services.AddSingleton<MotorcycleCreatedConsumer>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var consumer = scope.ServiceProvider.GetRequiredService<MotorcycleCreatedConsumer>();
    consumer.StartConsuming();
}

// Seed do banco
using (var scope = app.Services.CreateScope())
{
    var mongoContext = scope.ServiceProvider.GetRequiredService<MongoDbContext>();
    await MongoDbSeeder.SeedAsync(mongoContext);
}


app.UseExceptionHandlingMiddleware();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
