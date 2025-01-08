using GameStore.Dto;
using GameStore.Endpoint;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGamesEndpoints();

app.Run();
