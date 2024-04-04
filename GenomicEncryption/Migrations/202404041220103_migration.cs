namespace GenomicEncryption.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GenomicCodesTimes", "CreatedDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.GenomicCodesTimes", "CreatedDate");
        }
    }
}
