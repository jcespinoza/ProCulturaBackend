namespace ProCultura.Data.Context
{
    using System.Data.Entity;

    using ProCultura.Domain.Entities.Account;
    using ProCultura.Domain.Entities.Security;

    public class ProCulturaBackEndContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        public ProCulturaBackEndContext()
            : base("name=ProCulturaBackEndContext")
        {
            Database.SetInitializer(new ProCulturaContextInitializer());
        }



        public IDbSet<UserEntity> UserModels { get; set; }
        public IDbSet<UserRole> UserRoles { get; set; }
        public IDbSet<Role> Roles { get; set; }
    }
}
