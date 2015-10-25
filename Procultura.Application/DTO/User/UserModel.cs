namespace Procultura.Application.DTO.User
{
    using System.Collections.Generic;

    using DTO;

    public class UserModel: ResponseBase
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Email { get; set; }
        
        public ICollection<RoleModel> UserRoles { get; set; }
    }
}