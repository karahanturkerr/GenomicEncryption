namespace GenomicEncryption.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrationUsers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GenomicCodes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AD = c.String(),
                        DEGER = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        PasswordConfirm = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
            DropTable("dbo.GenomicCodes");
        }
    }
}
