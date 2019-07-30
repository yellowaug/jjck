namespace JJCKManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "JJCK.Account",
                c => new
                    {
                        Uid = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false),
                        PassWord = c.String(nullable: false),
                        Createdate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Uid);
            
            CreateTable(
                "JJCK.VmHostAccount",
                c => new
                    {
                        VmhostId = c.Int(nullable: false, identity: true),
                        VMhostName = c.String(nullable: false),
                        VMLoginIp = c.String(nullable: false),
                        VMLoginPassWord = c.String(nullable: false),
                        VMCreateTime = c.DateTime(nullable: false),
                        CreateUser = c.String(),
                        VmAccountDesc = c.String(),
                        acc_Uid = c.Int(),
                    })
                .PrimaryKey(t => t.VmhostId)
                .ForeignKey("JJCK.Account", t => t.acc_Uid)
                .Index(t => t.acc_Uid);
            
            CreateTable(
                "JJCKIot.TempFunction",
                c => new
                    {
                        DevID = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.DevID);
            
            CreateTable(
                "JJCK.WebManagerAccount",
                c => new
                    {
                        WaID = c.Int(nullable: false, identity: true),
                        AccountName = c.String(nullable: false),
                        AccountPassWord = c.String(nullable: false),
                        WebUrlORIPaddress = c.String(nullable: false),
                        WebAccountDesc = c.String(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        CreateUser = c.Int(nullable: false),
                        AccountUser_Uid = c.Int(),
                    })
                .PrimaryKey(t => t.WaID)
                .ForeignKey("JJCK.Account", t => t.AccountUser_Uid)
                .Index(t => t.AccountUser_Uid);
            
            CreateTable(
                "JJCK.OtherAccountName",
                c => new
                    {
                        OAccID = c.Int(nullable: false, identity: true),
                        OtherAccountName = c.String(nullable: false, maxLength: 20),
                        PassWord = c.String(nullable: false),
                        AccountDesc = c.String(),
                        Creater = c.String(),
                        accountUser_Uid = c.Int(),
                    })
                .PrimaryKey(t => t.OAccID)
                .ForeignKey("JJCK.Account", t => t.accountUser_Uid)
                .Index(t => t.accountUser_Uid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("JJCK.OtherAccountName", "accountUser_Uid", "JJCK.Account");
            DropForeignKey("JJCK.WebManagerAccount", "AccountUser_Uid", "JJCK.Account");
            DropForeignKey("JJCK.VmHostAccount", "acc_Uid", "JJCK.Account");
            DropIndex("JJCK.OtherAccountName", new[] { "accountUser_Uid" });
            DropIndex("JJCK.WebManagerAccount", new[] { "AccountUser_Uid" });
            DropIndex("JJCK.VmHostAccount", new[] { "acc_Uid" });
            DropTable("JJCK.OtherAccountName");
            DropTable("JJCK.WebManagerAccount");
            DropTable("JJCKIot.TempFunction");
            DropTable("JJCK.VmHostAccount");
            DropTable("JJCK.Account");
        }
    }
}
