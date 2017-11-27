namespace LibraryProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstStart : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Arguments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Source = c.String(nullable: false),
                        Content = c.String(nullable: false),
                        Kind = c.Int(nullable: false),
                        Rozprawka_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rozprawka", t => t.Rozprawka_Id)
                .Index(t => t.Rozprawka_Id);
            
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Rozprawka",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Question = c.String(nullable: false),
                        Side = c.String(nullable: false),
                        LibraryResource = c.String(nullable: false),
                        Author_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Authors", t => t.Author_Id, cascadeDelete: true)
                .Index(t => t.Author_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rozprawka", "Author_Id", "dbo.Authors");
            DropForeignKey("dbo.Arguments", "Rozprawka_Id", "dbo.Rozprawkas");
            DropIndex("dbo.Rozprawka", new[] { "Author_Id" });
            DropIndex("dbo.Arguments", new[] { "Rozprawka_Id" });
            DropTable("dbo.Rozprawka");
            DropTable("dbo.Authors");
            DropTable("dbo.Arguments");
        }
    }
}
