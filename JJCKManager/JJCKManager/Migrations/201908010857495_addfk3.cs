namespace JJCKManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addfk3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("JJCK.OtherAccountName", "datastatus");
        }
        
        public override void Down()
        {
            AddColumn("JJCK.OtherAccountName", "datastatus", c => c.Int(nullable: false));
        }
    }
}
