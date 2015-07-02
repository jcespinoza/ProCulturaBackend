namespace ProCulturaBackEnd.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSortOrder1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.userdetails", "Name", c => c.String());
            AlterColumn("dbo.userdetails", "Email", c => c.String());
            AlterColumn("dbo.userdetails", "Password", c => c.String());
            DropColumn("dbo.userdetails", "Mensaje");
            DropColumn("dbo.userdetails", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.userdetails", "Status", c => c.Int(nullable: false));
            AddColumn("dbo.userdetails", "Mensaje", c => c.String());
            AlterColumn("dbo.userdetails", "Password", c => c.String(nullable: false));
            AlterColumn("dbo.userdetails", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.userdetails", "Name", c => c.String(nullable: false));
        }
    }
}
