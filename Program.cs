using GameStore.Data;
using GameStore.Dto;
using GameStore.Endpoint;

var builder = WebApplication.CreateBuilder(args);

var connectionString = "Data Source=GameStore.db";

builder.Services.AddSqlite<GameStoreContext>(connectionString);

var app = builder.Build();

app.MapGamesEndpoints();

app.Run();
