namespace gamestore.Dtos;

public record class createGameDto(
    string Name,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate);
