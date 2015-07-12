namespace ProCultura.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddSortOrder : DbMigration
    {
        public override void Up()
        {
            this.CreateTable(
                "dbo.userdetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Salt = c.String(),
                        Role = c.Int(nullable: false),
                        Mensaje = c.String(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            this.DropTable("dbo.userdetails");
        }
    }
}
