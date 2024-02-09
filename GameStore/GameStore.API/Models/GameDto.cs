﻿namespace GameStore.API;

public class GameDto
{
    public required string Id { get; set; }

    public required string Name { get; set; }

    public required string Description { get; set; }

    public decimal Price { get; set; }
}
