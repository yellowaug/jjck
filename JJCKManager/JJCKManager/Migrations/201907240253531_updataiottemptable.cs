namespace JJCKManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updataiottemptable : DbMigration
    {
        public override void Up()
        {
            AddColumn("JJCKIot.TempFunction", "Temperature", c => c.String());
            AddColumn("JJCKIot.TempFunction", "humidity", c => c.String());
            AddColumn("JJCKIot.TempFunction", "IotDevIP", c => c.String());
            AddColumn("JJCKIot.TempFunction", "FuncName", c => c.String());
            AddColumn("JJCKIot.TempFunction", "update", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("JJCKIot.TempFunction", "update");
            DropColumn("JJCKIot.TempFunction", "FuncName");
            DropColumn("JJCKIot.TempFunction", "IotDevIP");
            DropColumn("JJCKIot.TempFunction", "humidity");
            DropColumn("JJCKIot.TempFunction", "Temperature");
        }
    }
}
