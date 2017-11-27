namespace LibraryProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeNames : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Rozprawkas", newName: "Essays");
            RenameColumn(table: "dbo.Arguments", name: "Rozprawka_Id", newName: "Essay_Id");
            RenameIndex(table: "dbo.Arguments", name: "IX_Rozprawka_Id", newName: "IX_Essay_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Arguments", name: "IX_Essay_Id", newName: "IX_Rozprawka_Id");
            RenameColumn(table: "dbo.Arguments", name: "Essay_Id", newName: "Rozprawka_Id");
            RenameTable(name: "dbo.Essays", newName: "Rozprawkas");
        }
    }
}
