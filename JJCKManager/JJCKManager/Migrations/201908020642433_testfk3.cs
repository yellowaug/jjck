namespace JJCKManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testfk3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("JJCK.Account", "DaId", c => c.Int());
            CreateIndex("JJCK.Account", "DaId");
            AddForeignKey("JJCK.Account", "DaId", "JJCK.Datastatus", "DaId");
        }
        
        public override void Down()
        {
            DropForeignKey("JJCK.Account", "DaId", "JJCK.Datastatus");
            DropIndex("JJCK.Account", new[] { "DaId" });
            DropColumn("JJCK.Account", "DaId");
        }
    }
}
