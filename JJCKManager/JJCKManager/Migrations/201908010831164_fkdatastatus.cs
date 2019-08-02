namespace JJCKManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fkdatastatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("JJCK.OtherAccountName", "datastatus", c => c.Int(nullable: false));
            CreateIndex("JJCK.OtherAccountName", "datastatus");
            AddForeignKey("JJCK.OtherAccountName", "datastatus", "JJCK.DataStatus", "DasId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("JJCK.OtherAccountName", "datastatus", "JJCK.DataStatus");
            DropIndex("JJCK.OtherAccountName", new[] { "datastatus" });
            DropColumn("JJCK.OtherAccountName", "datastatus");
        }
    }
}
