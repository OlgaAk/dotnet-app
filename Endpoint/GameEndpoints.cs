using System;
using GameStore.Dto;

namespace GameStore.Endpoint;

public static class GameEndpoints
{
    const string GetGameEndPointName = "GetGame";

    private static readonly List<GameDto> games = [
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

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app){

        var group = app.MapGroup("games")
                    .WithParameterValidation();

        group.MapGet("/", () => games);

        //  GET by id
        group.MapGet("/{id}", (int id) => {
            GameDto? game = games.Find(game => game.Id == id);
            return game is null? Results.NotFound() : Results.Ok(game);
            })
        .WithName(GetGameEndPointName);


        // POST
        group.MapPost("/", (CreateGameDto game) => {
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
        group.MapPut("/{id}", (int id, UpdateGameDto updateGameDto) => {
            int index = games.FindIndex(game => id == game.Id);
            if(index == -1) {
                return Results.NotFound();
            }
                games[index] = new GameDto(
                    id,
                    updateGameDto.Name,
                    updateGameDto.Genre,
                    updateGameDto.Price,
                    updateGameDto.ReleaseDate
                );
                return Results.NoContent(); 

        });

        // DELETE
        group.MapDelete("/{id}", (int id) => {
            games.RemoveAll(game => game.Id == id);

        return Results.NoContent();
    });

    return group;
}

}
