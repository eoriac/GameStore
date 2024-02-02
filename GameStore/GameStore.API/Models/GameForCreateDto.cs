namespace GameStore.API;

public class GameForCreateDto
{
    public required string Name { get; set; }

    public required string Description { get; set; }

    public decimal Price { get; set; }
}
