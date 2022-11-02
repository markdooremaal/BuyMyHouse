using AutoMapper;
using Models;
using Models.Requests;

namespace BuyMyHouse;

public class AutoMappingProfile : Profile
{
    public AutoMappingProfile()
    {
        CreateMap<UserRequest, User>();
        
        CreateMap<HouseRequest, House>();
    }
}