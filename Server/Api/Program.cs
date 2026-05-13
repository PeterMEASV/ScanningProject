using api;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using StateleSSE.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

var appOptions = builder.Services.AddAppOptions(builder.Configuration);

builder.Services.AddCors();

builder.Services.AddDbContext<MyDbContext>((sp, options) =>
{
    options.UseNpgsql(appOptions.DBConnectionString, npgsqlOptions => npgsqlOptions.EnableRetryOnFailure());
    options.AddEfRealtimeInterceptor(sp);
});

builder.Services.AddControllers();
builder.Services.AddInMemorySseBackplane();
builder.Services.AddEfRealtime();
builder.Services.AddOpenApiDocument();
builder.Services.AddControllers();



var app = builder.Build();
app.UseCors(config => config.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.MapControllers();
app.UseOpenApi();
app.UseSwaggerUi();

await app.GenerateApiClientsFromOpenApi("/../../client/src/generated-ts-client.ts");

app.Run();
