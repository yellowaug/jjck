namespace JJCKManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class accstatus : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "JJCK.AccStatus",
                c => new
                    {
                        AccId = c.Int(nullable: false, identity: true),
                        AccStatusDesc = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.AccId);
            
            AddColumn("JJCK.Account", "AccId", c => c.Int());
            CreateIndex("JJCK.Account", "AccId");
            AddForeignKey("JJCK.Account", "AccId", "JJCK.AccStatus", "AccId");
        }
        
        public override void Down()
        {
            DropForeignKey("JJCK.Account", "AccId", "JJCK.AccStatus");
            DropIndex("JJCK.Account", new[] { "AccId" });
            DropColumn("JJCK.Account", "AccId");
            DropTable("JJCK.AccStatus");
        }
    }
}
