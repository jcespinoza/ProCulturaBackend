namespace ProCultura.Web.Api.Models
{
    using System.Collections.Generic;

    public class UserModel
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