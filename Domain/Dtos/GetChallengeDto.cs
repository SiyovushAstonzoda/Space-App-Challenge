namespace Domain.Dtos;
using Domain.Dtos;

public class GetChallengeDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public List<GetGroupDto>? Groups { get; set; }
}
