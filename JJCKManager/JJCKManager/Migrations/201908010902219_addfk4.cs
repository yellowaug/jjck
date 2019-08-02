namespace JJCKManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addfk4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("JJCK.OtherAccountName", "dataStatus_DasId", "JJCK.DataStatus");
            DropIndex("JJCK.OtherAccountName", new[] { "dataStatus_DasId" });
            RenameColumn(table: "JJCK.OtherAccountName", name: "dataStatus_DasId", newName: "datastatus");
            AlterColumn("JJCK.OtherAccountName", "datastatus", c => c.Int(nullable: false));
            CreateIndex("JJCK.OtherAccountName", "datastatus");
            AddForeignKey("JJCK.OtherAccountName", "datastatus", "JJCK.DataStatus", "DasId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("JJCK.OtherAccountName", "datastatus", "JJCK.DataStatus");
            DropIndex("JJCK.OtherAccountName", new[] { "datastatus" });
            AlterColumn("JJCK.OtherAccountName", "datastatus", c => c.Int());
            RenameColumn(table: "JJCK.OtherAccountName", name: "datastatus", newName: "dataStatus_DasId");
            CreateIndex("JJCK.OtherAccountName", "dataStatus_DasId");
            AddForeignKey("JJCK.OtherAccountName", "dataStatus_DasId", "JJCK.DataStatus", "DasId");
        }
    }
}
