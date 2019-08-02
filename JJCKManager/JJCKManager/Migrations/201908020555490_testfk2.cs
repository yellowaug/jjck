namespace JJCKManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testfk2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("JJCK.OtherAccountName", "DaId", c => c.Int(nullable: false));
            CreateIndex("JJCK.OtherAccountName", "DaId");
            AddForeignKey("JJCK.OtherAccountName", "DaId", "JJCK.Datastatus", "DaId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("JJCK.OtherAccountName", "DaId", "JJCK.Datastatus");
            DropIndex("JJCK.OtherAccountName", new[] { "DaId" });
            DropColumn("JJCK.OtherAccountName", "DaId");
        }
    }
}
