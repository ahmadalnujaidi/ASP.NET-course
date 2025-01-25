using gamestore.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const string GetGameEndpointName = "GetGame";

List<GameDto> games = [
    new (1, "Cyberpunk 2077", "RPG", 200, new DateOnly(2020, 12, 10)),
    new (2, "The Witcher 3", "RPG", 100, new DateOnly(2015, 5, 19)),
    new (3, "Doom Eternal", "FPS", 150, new DateOnly(2020, 3, 20))
];

// GET /games
app.MapGet("games", () => games);

// GET /games/:id
app.MapGet("games/{id}", (int id) => {
    GameDto? game = games.Find(game => game.Id == id);

    return game is null ? Results.NotFound() : Results.Ok(game);
})
.WithName(GetGameEndpointName);

// POST /games
app.MapPost("games", (createGameDto newGame) => {
    GameDto game = new(
        games.Count + 1,
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate
    );

    games.Add(game);

    return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
});

// PUT /games/:id
app.MapPut("games/{id}", (int id, UpdateGameDto updatedGame) => {
    
    var index = games.FindIndex(game => game.Id == id);

    if (index == -1)
    {
        return Results.NotFound();
    }

    games[index] = new GameDto(
        id,
        updatedGame.Name,
        updatedGame.Genre,
        updatedGame.Price,
        updatedGame.ReleaseDate
    );

     return Results.NoContent();

});

// DELETE /games/:id
app.MapDelete("games/{id}", (int id) => {

    // var index = games.FindIndex(game => game.Id == id);
    // games.RemoveAt(index);

    games.RemoveAll(game => game.Id == id);

    return Results.NoContent();
});

app.Run();
