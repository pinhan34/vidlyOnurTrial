namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MemberShipTypeTableUpdatedWithNewValues : DbMigration
    {
        public override void Up()
        {
            Sql("Update MemberShipTypes set Name = 'Yearly' where Id = 3");
            Sql("Update MemberShipTypes set Name = 'Premium' where Id = 4");
        }
        
        public override void Down()
        {
        }
    }
}
