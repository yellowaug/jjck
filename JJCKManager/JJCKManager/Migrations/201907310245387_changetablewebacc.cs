namespace JJCKManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changetablewebacc : DbMigration
    {
        public override void Up()
        {
            DropColumn("JJCK.WebManagerAccount", "CreateUser");
        }
        
        public override void Down()
        {
            AddColumn("JJCK.WebManagerAccount", "CreateUser", c => c.Int(nullable: false));
        }
    }
}
