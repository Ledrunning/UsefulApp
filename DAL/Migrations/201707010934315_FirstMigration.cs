namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NoteEntities", "Time", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.NoteEntities", "Time");
        }
    }
}
