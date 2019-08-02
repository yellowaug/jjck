namespace JJCKManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addfk2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("JJCK.OtherAccountName", "datastatus", c => c.Int(nullable: false));
            AddColumn("JJCK.OtherAccountName", "dataStatus_DasId", c => c.Int());
            CreateIndex("JJCK.OtherAccountName", "dataStatus_DasId");
            AddForeignKey("JJCK.OtherAccountName", "dataStatus_DasId", "JJCK.DataStatus", "DasId");
        }
        
        public override void Down()
        {
            DropForeignKey("JJCK.OtherAccountName", "dataStatus_DasId", "JJCK.DataStatus");
            DropIndex("JJCK.OtherAccountName", new[] { "dataStatus_DasId" });
            DropColumn("JJCK.OtherAccountName", "dataStatus_DasId");
            DropColumn("JJCK.OtherAccountName", "datastatus");
        }
    }
}
