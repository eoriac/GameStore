using System.ComponentModel.DataAnnotations;

namespace GameStore.API;

public class GameDto
{
    public required string Id { get; set; }

    [Required]    
    [MaxLength(50, ErrorMessage = "Name should be less than 50 chars")]
    public required string Name { get; set; }

    public required string Description { get; set; }

    public decimal Price { get; set; }
}
