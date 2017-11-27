namespace LibraryProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mod : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Arguments", new[] { "Essay_Id" });
            CreateIndex("dbo.Arguments", "essay_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Arguments", new[] { "essay_Id" });
            CreateIndex("dbo.Arguments", "Essay_Id");
        }
    }
}
