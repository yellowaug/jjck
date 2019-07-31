namespace JJCKManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changetableforeigenKey1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("JJCK.OtherAccountName", "accountUser_Uid", "JJCK.Account");
            DropIndex("JJCK.OtherAccountName", new[] { "accountUser_Uid" });
            DropColumn("JJCK.OtherAccountName", "Creater");
            RenameColumn(table: "JJCK.OtherAccountName", name: "accountUser_Uid", newName: "Creater");
            AlterColumn("JJCK.OtherAccountName", "Creater", c => c.Int(nullable: false));
            AlterColumn("JJCK.OtherAccountName", "Creater", c => c.Int(nullable: false));
            CreateIndex("JJCK.OtherAccountName", "Creater");
            AddForeignKey("JJCK.OtherAccountName", "Creater", "JJCK.Account", "Uid", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("JJCK.OtherAccountName", "Creater", "JJCK.Account");
            DropIndex("JJCK.OtherAccountName", new[] { "Creater" });
            AlterColumn("JJCK.OtherAccountName", "Creater", c => c.Int());
            AlterColumn("JJCK.OtherAccountName", "Creater", c => c.String());
            RenameColumn(table: "JJCK.OtherAccountName", name: "Creater", newName: "accountUser_Uid");
            AddColumn("JJCK.OtherAccountName", "Creater", c => c.String());
            CreateIndex("JJCK.OtherAccountName", "accountUser_Uid");
            AddForeignKey("JJCK.OtherAccountName", "accountUser_Uid", "JJCK.Account", "Uid");
        }
    }
}
