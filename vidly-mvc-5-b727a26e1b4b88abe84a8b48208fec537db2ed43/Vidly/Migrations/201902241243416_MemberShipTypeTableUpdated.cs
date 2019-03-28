namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MemberShipTypeTableUpdated : DbMigration
    {
        public override void Up()
        {
            Sql("Update MemberShipTypes set Name = 'Pay as u Go' where Id = 1");
            Sql("Update MemberShipTypes set Name = 'Monthly' where Id = 2");
            Sql("Update MemberShipTypes set Name = 'Yearly' where Id = 3");
            Sql("Update MemberShipTypes set Name = 'Premium' where Id = 4");

        }

        public override void Down()
        {
        }
    }
}
