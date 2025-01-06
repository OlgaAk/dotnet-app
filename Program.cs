using GameStore.Dto;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<GameDto> games = [
    new (
    1, 
    "War Craft", 
    "Strategy", 
    12.3M, 
    new DateOnly(1999, 1, 1)
    ),
    new (
    2, 
    "Daggerfol", 
    "Roleplaying", 
    1.5M, 
    new DateOnly(1996, 12, 12)
    ),
];

app.MapGet("games", () => games);

app.Run();
