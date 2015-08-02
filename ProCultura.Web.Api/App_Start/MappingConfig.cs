namespace ProCultura.Web.Api.App_Start
{
    using AutoMapper;

    using Procultura.Application.DTO.User;

    using ProCultura.Domain.Entities.Account;

    public class MappingConfig
    {
        public static void RegisterMappings()
        {
            Mapper.CreateMap<UserEntity, UserModel>().ReverseMap();
            Mapper.CreateMap<RegisterModel, UserEntity>().ReverseMap();
        }
    }
}