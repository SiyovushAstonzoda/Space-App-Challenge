using System.ComponentModel.DataAnnotations;
namespace Domain.Dtos;

public class AddGroupDto
{
    public int Id { get; set; }
    [MaxLength(500), Required]
    public string? GroupNick { get; set; }
    public bool NeededMember { get; set; }
    [MaxLength(300), Required]
    public string? TeamSlogan { get; set; }
    public DateTime CreatedAt { get; set; }
    public int ChallengeId { get; set; }
}
