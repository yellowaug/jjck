namespace JJCKManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class delfkdatastatus : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("JJCK.OtherAccountName", "datastatus", "JJCK.DataStatus");
            DropIndex("JJCK.OtherAccountName", new[] { "datastatus" });
            DropColumn("JJCK.OtherAccountName", "datastatus");
        }
        
        public override void Down()
        {
            AddColumn("JJCK.OtherAccountName", "datastatus", c => c.Int(nullable: false));
            CreateIndex("JJCK.OtherAccountName", "datastatus");
            AddForeignKey("JJCK.OtherAccountName", "datastatus", "JJCK.DataStatus", "DasId", cascadeDelete: true);
        }
    }
}
