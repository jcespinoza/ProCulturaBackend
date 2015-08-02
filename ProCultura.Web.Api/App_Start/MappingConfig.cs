namespace ProCulturaBackEnd.App_Start
{
    using AutoMapper;

    using ProCultura.Domain.Entities.Account;
    using ProCultura.Web.Api.Models;

    public class MappingConfig
    {
        public static void RegisterMappings()
        {
            Mapper.CreateMap<UserEntity, UserModel>().ReverseMap();
            Mapper.CreateMap<RegisterModel, UserEntity>().ReverseMap();
        }
    }
}