namespace ProCultura.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddSortOrder1 : DbMigration
    {
        public override void Up()
        {
            this.AlterColumn("dbo.userdetails", "Name", c => c.String());
            this.AlterColumn("dbo.userdetails", "Email", c => c.String());
            this.AlterColumn("dbo.userdetails", "Password", c => c.String());
            this.DropColumn("dbo.userdetails", "Mensaje");
            this.DropColumn("dbo.userdetails", "Status");
        }
        
        public override void Down()
        {
            this.AddColumn("dbo.userdetails", "Status", c => c.Int(nullable: false));
            this.AddColumn("dbo.userdetails", "Mensaje", c => c.String());
            this.AlterColumn("dbo.userdetails", "Password", c => c.String(nullable: false));
            this.AlterColumn("dbo.userdetails", "Email", c => c.String(nullable: false));
            this.AlterColumn("dbo.userdetails", "Name", c => c.String(nullable: false));
        }
    }
}
