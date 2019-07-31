namespace JJCKManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changetableforeigenKey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("JJCK.VmHostAccount", "acc_Uid", "JJCK.Account");
            DropForeignKey("JJCK.WebManagerAccount", "AccountUser_Uid", "JJCK.Account");
            DropIndex("JJCK.VmHostAccount", new[] { "acc_Uid" });
            DropIndex("JJCK.WebManagerAccount", new[] { "AccountUser_Uid" });
            DropColumn("JJCK.VmHostAccount", "CreateUser");
            RenameColumn(table: "JJCK.VmHostAccount", name: "acc_Uid", newName: "CreateUser");
            RenameColumn(table: "JJCK.WebManagerAccount", name: "AccountUser_Uid", newName: "CreateUser");
            AlterColumn("JJCK.VmHostAccount", "CreateUser", c => c.Int(nullable: false));
            AlterColumn("JJCK.VmHostAccount", "CreateUser", c => c.Int(nullable: false));
            AlterColumn("JJCK.WebManagerAccount", "CreateUser", c => c.Int(nullable: false));
            CreateIndex("JJCK.VmHostAccount", "CreateUser");
            CreateIndex("JJCK.WebManagerAccount", "CreateUser");
            AddForeignKey("JJCK.VmHostAccount", "CreateUser", "JJCK.Account", "Uid", cascadeDelete: true);
            AddForeignKey("JJCK.WebManagerAccount", "CreateUser", "JJCK.Account", "Uid", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("JJCK.WebManagerAccount", "CreateUser", "JJCK.Account");
            DropForeignKey("JJCK.VmHostAccount", "CreateUser", "JJCK.Account");
            DropIndex("JJCK.WebManagerAccount", new[] { "CreateUser" });
            DropIndex("JJCK.VmHostAccount", new[] { "CreateUser" });
            AlterColumn("JJCK.WebManagerAccount", "CreateUser", c => c.Int());
            AlterColumn("JJCK.VmHostAccount", "CreateUser", c => c.Int());
            AlterColumn("JJCK.VmHostAccount", "CreateUser", c => c.String());
            RenameColumn(table: "JJCK.WebManagerAccount", name: "CreateUser", newName: "AccountUser_Uid");
            RenameColumn(table: "JJCK.VmHostAccount", name: "CreateUser", newName: "acc_Uid");
            AddColumn("JJCK.VmHostAccount", "CreateUser", c => c.String());
            CreateIndex("JJCK.WebManagerAccount", "AccountUser_Uid");
            CreateIndex("JJCK.VmHostAccount", "acc_Uid");
            AddForeignKey("JJCK.WebManagerAccount", "AccountUser_Uid", "JJCK.Account", "Uid");
            AddForeignKey("JJCK.VmHostAccount", "acc_Uid", "JJCK.Account", "Uid");
        }
    }
}
