using Domain.Entities;
using Domain.Dtos;
using AutoMapper;

namespace Infrastructure.AutoMapper;

public class ServiceProfile : Profile
{
    public ServiceProfile()
    {
        CreateMap<Challenge, GetChallengeDto>().ReverseMap();
        CreateMap<Group, GetGroupDto>().ReverseMap();
        CreateMap<Location, GetLocationDto>().ReverseMap();
        CreateMap<Participant, GetParticipantDto>().ReverseMap();

        CreateMap<Challenge, AddChallengeDto>().ReverseMap();
        CreateMap<Group, AddGroupDto>().ReverseMap();
        CreateMap<Location, AddLocationDto>().ReverseMap();
        CreateMap<Participant, AddParticipantDto>().ReverseMap();

        CreateMap<GetGroupDto, GetChallengeDto>().ReverseMap();
        CreateMap<GetChallengeDto, GetGroupDto>().ReverseMap();
    }
}
