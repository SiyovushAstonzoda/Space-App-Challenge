using System.ComponentModel.DataAnnotations;
namespace Domain.Entities;

public class Group
{
    public int Id { get; set; }
    [MaxLength(500), Required]
    public string? GroupNick { get; set; }
    public bool NeededMember { get; set; }
    public string? TeamSlogan { get; set; }
    public DateTime CreatedAt { get; set; }

    public int ChallengeId { get; set; }
    public virtual Challenge? Challenge { get; set; }
    public virtual List<Participant>? Participants { get; set; }
}
