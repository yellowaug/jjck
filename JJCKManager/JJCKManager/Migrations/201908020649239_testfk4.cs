namespace JJCKManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testfk4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("JJCK.VmHostAccount", "DaId", c => c.Int());
            AddColumn("JJCK.WebManagerAccount", "DaId", c => c.Int());
            CreateIndex("JJCK.VmHostAccount", "DaId");
            CreateIndex("JJCK.WebManagerAccount", "DaId");
            AddForeignKey("JJCK.VmHostAccount", "DaId", "JJCK.Datastatus", "DaId");
            AddForeignKey("JJCK.WebManagerAccount", "DaId", "JJCK.Datastatus", "DaId");
        }
        
        public override void Down()
        {
            DropForeignKey("JJCK.WebManagerAccount", "DaId", "JJCK.Datastatus");
            DropForeignKey("JJCK.VmHostAccount", "DaId", "JJCK.Datastatus");
            DropIndex("JJCK.WebManagerAccount", new[] { "DaId" });
            DropIndex("JJCK.VmHostAccount", new[] { "DaId" });
            DropColumn("JJCK.WebManagerAccount", "DaId");
            DropColumn("JJCK.VmHostAccount", "DaId");
        }
    }
}
