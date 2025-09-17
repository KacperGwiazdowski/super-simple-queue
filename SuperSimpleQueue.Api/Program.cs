using LiteDB;
using SuperSimpleQueue.Core.Configuration;
using SuperSimpleQueue.Core.Services;
using SuperSimpleQueue.Core.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IQueuesService, QueueService>();
builder.Services.AddScoped<IMessageService, MessageService>();

var liteDbConfig = builder.Configuration.GetSection(nameof(ConnectionStringConfiguration)).Get<ConnectionStringConfiguration>();

builder.Services.AddSingleton<ILiteDatabase, LiteDatabase>(_ =>
{
    return new LiteDatabase(ConnectionStringBuilder.GetLiteDbConnectionString(liteDbConfig ?? new ConnectionStringConfiguration()));
});

var app = builder.Build();

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
