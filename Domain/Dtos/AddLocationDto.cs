using System.ComponentModel.DataAnnotations;
namespace Domain.Dtos;

public class AddLocationDto
{
    public int Id { get; set; }
    [Required, MaxLength(200)]
    public string? Name { get; set; }
    public string? Description { get; set; }
}
