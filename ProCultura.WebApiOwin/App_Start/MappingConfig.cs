namespace ProCultura.WebApiOwin.App_Start
{
    using AutoMapper;

    using Procultura.Application.DTO.Events;
    using Procultura.Application.DTO.User;

    using Domain.Entities.Account;
    using Domain.Entities.Events;
    using Domain.Entities.Security;

    public class MappingConfig
    {
        public static void RegisterMappings()
        {
            Mapper.CreateMap<UserEntity, UserModel>().ReverseMap();
            Mapper.CreateMap<RegisterModel, UserEntity>().ReverseMap();
            Mapper.CreateMap<UpdateUserModel, UserEntity>().ReverseMap();
            Mapper.CreateMap<UserRole, RoleModel>();
            Mapper.CreateMap<Event, EventModel>().ReverseMap();
            Mapper.CreateMap<Event, NewEventModel>().ReverseMap();
        }
    }
}