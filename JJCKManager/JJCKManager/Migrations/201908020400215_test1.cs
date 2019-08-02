namespace JJCKManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test1 : DbMigration
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
                "JJCK.Datastatus",
                c => new
                    {
                        DaId = c.Int(nullable: false, identity: true),
                        StatusDesc = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.DaId);
            
            CreateTable(
                "JJCK.VmHostAccount",
                c => new
                    {
                        VmhostId = c.Int(nullable: false, identity: true),
                        VMhostName = c.String(nullable: false),
                        VMLoginIp = c.String(nullable: false),
                        VMLoginPassWord = c.String(nullable: false),
                        VMCreateTime = c.DateTime(nullable: false),
                        CreateUser = c.Int(nullable: false),
                        VmAccountDesc = c.String(),
                    })
                .PrimaryKey(t => t.VmhostId)
                .ForeignKey("JJCK.Account", t => t.CreateUser, cascadeDelete: true)
                .Index(t => t.CreateUser);
            
            CreateTable(
                "JJCKIot.TempFunction",
                c => new
                    {
                        DevID = c.Int(nullable: false, identity: true),
                        Temperature = c.String(),
                        humidity = c.String(),
                        IotDevIP = c.String(),
                        FuncName = c.String(),
                        Updatadate = c.DateTime(nullable: false),
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
                    })
                .PrimaryKey(t => t.WaID)
                .ForeignKey("JJCK.Account", t => t.CreateUser, cascadeDelete: true)
                .Index(t => t.CreateUser);
            
            CreateTable(
                "JJCK.OtherAccountName",
                c => new
                    {
                        OAccID = c.Int(nullable: false, identity: true),
                        OtherAccountName = c.String(nullable: false, maxLength: 20),
                        PassWord = c.String(nullable: false),
                        AccountDesc = c.String(),
                        Creater = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OAccID)
                .ForeignKey("JJCK.Account", t => t.Creater, cascadeDelete: true)
                .Index(t => t.Creater);
            
        }
        
        public override void Down()
        {
            DropForeignKey("JJCK.OtherAccountName", "Creater", "JJCK.Account");
            DropForeignKey("JJCK.WebManagerAccount", "CreateUser", "JJCK.Account");
            DropForeignKey("JJCK.VmHostAccount", "CreateUser", "JJCK.Account");
            DropIndex("JJCK.OtherAccountName", new[] { "Creater" });
            DropIndex("JJCK.WebManagerAccount", new[] { "CreateUser" });
            DropIndex("JJCK.VmHostAccount", new[] { "CreateUser" });
            DropTable("JJCK.OtherAccountName");
            DropTable("JJCK.WebManagerAccount");
            DropTable("JJCKIot.TempFunction");
            DropTable("JJCK.VmHostAccount");
            DropTable("JJCK.Datastatus");
            DropTable("JJCK.Account");
        }
    }
}
