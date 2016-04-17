namespace TestApplication.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.customer",
                c => new
                    {
                        customer_id = c.Long(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        email = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.customer_id);
            
            CreateTable(
                "dbo.order",
                c => new
                    {
                        order_id = c.Long(nullable: false, identity: true),
                        customer_id = c.Long(nullable: false),
                        created = c.DateTime(nullable: false),
                        price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.order_id)
                .ForeignKey("dbo.customer", t => t.customer_id, cascadeDelete: true)
                .Index(t => t.customer_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.order", "customer_id", "dbo.customer");
            DropIndex("dbo.order", new[] { "customer_id" });
            DropTable("dbo.order");
            DropTable("dbo.customer");
        }
    }
}
