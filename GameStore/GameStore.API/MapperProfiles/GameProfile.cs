using AutoMapper;

namespace GameStore.API;

public class GameProfile : Profile
{
    public GameProfile()
    {
        CreateMap<GameForCreateDto, Game>();
        CreateMap<GameForUpdateDto, Game>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<Game, GameDto>();
    }
}
