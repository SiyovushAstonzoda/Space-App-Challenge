using System.ComponentModel.DataAnnotations;
namespace Domain.Entities;

public class Participant
{
    public int Id { get; set; }
    [MaxLength(500), Required]
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Password { get; set; }
    public DateTime CreatedAt { get; set; }
    

    public int GroupId { get; set; }
    public virtual Group? Group { get; set; }

    public int LocationId { get; set; }
    public virtual Location? Location { get; set; }
}
