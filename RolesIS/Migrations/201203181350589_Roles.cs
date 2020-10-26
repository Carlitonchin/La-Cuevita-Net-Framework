namespace RolesIS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Roles : DbMigration
    {
        public override void Up()
        {
            Sql(@"INSERT INTO[dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES(N'0e89ec6a-9348-4b0b-9123-e1fb9cc2f2c6', N'admin@cuevita.com', 0, N'AA+LSOEggtJ7NNvTzdQ2duZf0Re8jAoeviGdgNNtgv8NeSfUdiI3oTX07b4ttF+Nnw==', N'76bb7b6f-3e6b-42c2-9df9-60f3a4b88344', NULL, 0, 0, NULL, 1, 0, N'admin@cuevita.com')
            INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'e38f5120-3eb4-469a-92f3-706f6a47406c', N'Admin')
            INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'338c09b3-21e9-4a3a-a71a-4768fdc7a394', N'Comprador')
            INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'3c02b9d4-e386-4e47-bab4-d00b9f7e86cd', N'Vendedor')
            INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'0e89ec6a-9348-4b0b-9123-e1fb9cc2f2c6', N'e38f5120-3eb4-469a-92f3-706f6a47406c')

");

        }

    public override void Down()
        {
        }
    }
}
