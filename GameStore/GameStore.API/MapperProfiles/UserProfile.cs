using AutoMapper;

namespace GameStore.API;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>();
    }    
}
