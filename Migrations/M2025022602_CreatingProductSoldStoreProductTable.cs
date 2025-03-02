using FluentMigrator;

namespace MVCMovieApp.Migrations
{
    [Migration(2025022602)]
    public class M2025022602_CreatingProductSoldStoreProductTable : Migration
    {
        public override void Up()
        {
            var sql = @"
                        If(NOT EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Store'))
                          Begin
                            CREATE TABLE [dbo].[Store](
	                        [Id] [int] IDENTITY(1,1) NOT NULL,
	                        [Name] [nvarchar](max) NULL,
	                        [Address] [nvarchar](max) NULL,
                         CONSTRAINT [PK_Store] PRIMARY KEY CLUSTERED 
                        (
	                        [Id] ASC
                        )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
                        ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
                          END

                       If(NOT EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Product'))
                          Begin
                             CREATE TABLE [dbo].[Product](
	                            [Id] [int] IDENTITY(1,1) NOT NULL,
	                            [Name] [nvarchar](max) NULL,
	                            [Price] [decimal](18, 2) NOT NULL,
                             CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
                            (
	                            [Id] ASC
                            )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
                            ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
                         END


                       If (NOT EXISTS(select 1 from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'ProductSold'))
                         Begin
	                        
                            CREATE TABLE [dbo].[ProductSold](
	                            [Id] [int] IDENTITY(1,1) NOT NULL,
	                            [CustomerId] [int] NULL,
	                            [ProductId] [int] NULL,
	                            [StoreId] [int] NULL,
                                [DateSold] [datetime] NULL,
                             CONSTRAINT [PK_dbo.ProductSold] PRIMARY KEY CLUSTERED 
                            (
	                            [Id] ASC
                            )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
                            ) ON [PRIMARY]

                            ALTER TABLE [dbo].[ProductSold]  WITH CHECK ADD  CONSTRAINT [FK_ProductSold_Customer] FOREIGN KEY([CustomerId])
                            REFERENCES [dbo].[Customer] ([Id])

                            ALTER TABLE [dbo].[ProductSold] CHECK CONSTRAINT [FK_ProductSold_Customer]

                            ALTER TABLE [dbo].[ProductSold]  WITH CHECK ADD  CONSTRAINT [FK_ProductSold_Product] FOREIGN KEY([ProductId])
                            REFERENCES [dbo].[Product] ([Id])

                            ALTER TABLE [dbo].[ProductSold] CHECK CONSTRAINT [FK_ProductSold_Product]

                            ALTER TABLE [dbo].[ProductSold]  WITH CHECK ADD  CONSTRAINT [FK_ProductSold_Store] FOREIGN KEY([StoreId])
                            REFERENCES [dbo].[Store] ([Id])

                            ALTER TABLE [dbo].[ProductSold] CHECK CONSTRAINT [FK_ProductSold_Store]
                          End
                        ";
            Execute.Sql(sql);
        }

        public override void Down()
        {
            throw new NotImplementedException();
        }

    }
}
