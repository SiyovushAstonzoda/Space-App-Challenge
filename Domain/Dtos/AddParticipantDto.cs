using System.ComponentModel.DataAnnotations;
namespace Domain.Dtos;

public class AddParticipantDto
{
    public int Id { get; set; }
    [MaxLength(50), Required]
    public string? FullName { get; set; }
    [EmailAddress]
    public string? Email { get; set; }
    [Required,MaxLength(13)]
    public string? Phone { get; set; }
    public string? Password { get; set; }
    public DateTime CreatedAt { get; set; }
}