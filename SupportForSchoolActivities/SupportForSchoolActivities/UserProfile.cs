using AutoMapper;
using SupportForSchoolActivities.Domain.Entity;
using SupportForSchoolActivities.Models.RegisterModels;

namespace SupportForSchoolActivities
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserRegister, Admin>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));
            CreateMap<UserRegister, Student>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));
            CreateMap<UserRegister, Teacher>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));
            CreateMap<UserRegister, Parent>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));
        }
    }
}
