namespace JJCKManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changercum : DbMigration
    {
        public override void Up()
        {
            AddColumn("JJCKIot.TempFunction", "Updatadate", c => c.DateTime(nullable: false));
            DropColumn("JJCKIot.TempFunction", "update");
        }
        
        public override void Down()
        {
            AddColumn("JJCKIot.TempFunction", "update", c => c.DateTime(nullable: false));
            DropColumn("JJCKIot.TempFunction", "Updatadate");
        }
    }
}
