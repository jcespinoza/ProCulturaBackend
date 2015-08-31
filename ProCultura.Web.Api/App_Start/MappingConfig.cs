namespace ProCultura.Web.Api.App_Start
{
    using AutoMapper;

    using Procultura.Application.DTO.Events;
    using Procultura.Application.DTO.User;

    using ProCultura.Domain.Entities.Account;
    using ProCultura.Domain.Entities.Events;

    public class MappingConfig
    {
        public static void RegisterMappings()
        {
            Mapper.CreateMap<UserEntity, UserModel>().ReverseMap();
            Mapper.CreateMap<RegisterModel, UserEntity>().ReverseMap();
            Mapper.CreateMap<Event, EventModel>().ReverseMap();
            Mapper.CreateMap<Event, NewEventModel>().ReverseMap();
        }
    }
}