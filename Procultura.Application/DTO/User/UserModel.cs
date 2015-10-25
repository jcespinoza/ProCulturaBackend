using System.Collections.Generic;

namespace Procultura.Application.DTO.User
{
    public class UserModel: ResponseBase
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Email { get; set; }
        
        public ICollection<RoleModel> UserRoles { get; set; }
    }
}