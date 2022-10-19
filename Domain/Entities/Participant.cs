using System.ComponentModel.DataAnnotations;
namespace Domain.Entities;

public class Participant
{
    public int Id { get; set; }
    [MaxLength(50), Required]
    public string? FullName { get; set; }
    [EmailAddress]
    public string? Email { get; set; }
    [Required,MaxLength(13)]
    public string? Phone { get; set; }
    public string? Password { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    

    public int GroupId { get; set; }
    public virtual Group? Group { get; set; }

    public int LocationId { get; set; }
    public virtual Location? Location { get; set; }

    public Participant()
    {
        CreatedAt = DateTimeOffset.Now;
    }

}
