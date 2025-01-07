using GameStore.Dto;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const string GetGameEndPointName = "GetGame";

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

//  GET by id
app.MapGet("games/{id}", (int id) => games.Find(game => game.Id == id))
.WithName(GetGameEndPointName);


// POST
app.MapPost("games", (CreateGameDto game) => {
    GameDto newGame = new(
        games.Count+1, 
        game.Name, 
        game.Genre, 
        game.Price, 
        game.ReleaseDate
        );
    games.Add(newGame);

    return Results.CreatedAtRoute(GetGameEndPointName, new{id = newGame.Id}, game);
    });

// PUT
app.MapPut("games/{id}", (int id, UpdateGameDto updateGameDto) => {
    int index = games.FindIndex(game => id == game.Id);
    if(index != -1) {
        games[index] = new GameDto(
            id,
            updateGameDto.Name,
            updateGameDto.Genre,
            updateGameDto.Price,
            updateGameDto.ReleaseDate
        );
        
    }
    return Results.NoContent();
});

app.Run();
