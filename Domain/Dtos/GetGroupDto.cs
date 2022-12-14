namespace Domain.Dtos;
using Domain.Dtos;

public class GetGroupDto
{
    public int Id { get; set; }
    public string? GroupNick { get; set; }
    public bool NeededMember { get; set; }
    public string? TeamSlogan { get; set; }
    public DateTimeOffset CreatedAt { get; set; }

    public int ChallengeId { get; set; }
    public string? ChallengeName { get; set; }
    public List<GetParticipantDto>? Participants { get; set; }

     public GetGroupDto()
    {
        CreatedAt = DateTimeOffset.Now;
    }
}
