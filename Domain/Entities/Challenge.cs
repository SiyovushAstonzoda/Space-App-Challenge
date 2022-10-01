using System.ComponentModel.DataAnnotations;
namespace Domain.Entities;

public class Challenge
{
    public int Id { get; set; }
    [MaxLength(500), Required]
    public string? Name { get; set; }
    public string? Description { get; set; }

    public virtual List<Group>? Groups { get; set; }
}
