using AutoMapper;
using TimeManagementAPI.Models;
using TimeManagementAPI.Models.Responses;

namespace TimeManagementAPI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserModel, GetByUsernameResponse>();
        }
    }
}
