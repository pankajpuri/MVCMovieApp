using FluentMigrator;

namespace MVCMovieApp.Migrations
{
    [Migration(2025022701)]
    public class M2025022701_AlteringTableProductSoldToAddDateSold : Migration
    {
        public override void Up()
        {
            var sql = @"
                        IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS where TABLE_NAME = 'ProductSold' AND COLUMN_NAME = 'DateSold')
                BEGIN
	                ALTER TABLE ProductSold
	                ADD DateSold DATETIME NOT NULL CONSTRAINT [DF_ProductSold_DateSold]
                END
                        ";
            Execute.Sql(sql);
        }
        public override void Down()
        {
            throw new NotImplementedException();
        }

        
    }
}
