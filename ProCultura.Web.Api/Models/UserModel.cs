namespace ProCultura.Web.Api.Models
{
    using System.Collections.Generic;

    using Procultura.Application.DTO;

    public class UserModel: ResponseBase
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Email { get; set; }
        
        public ICollection<RoleModel> Role { get; set; }
    }

    public class RoleModel
    {
        public string RoleId { get; set; }
    }
}