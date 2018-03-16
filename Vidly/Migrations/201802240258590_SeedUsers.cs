namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'2ffbe885-7a23-4b16-a26f-fb0dcba23afa', N'admin@vidly.com', 0, N'APloQAa3X8J3Rh6lffIQHNadNo2R+J77JaJ67jBneJ5cFm11WVj6odMkIYHn8ZFZwg==', N'3cf53ef4-8c34-44ae-b6a3-296c63564cd8', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'ac56128e-f58b-4889-abc9-42465edb1237', N'guest@vidly.com', 0, N'AL+l/7mljSvnTcCyArsz6JGU0OdGrjQYpW2ZZuubcdCmCoNEASTSFK8D1Pb5namMyQ==', N'6c3f0c87-b621-460f-b4ef-2cb105f303da', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')
                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'7488912a-496a-416a-bafb-0bac74488f30', N'CanManageMovies')
                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'2ffbe885-7a23-4b16-a26f-fb0dcba23afa', N'7488912a-496a-416a-bafb-0bac74488f30')
            ");

        }
        
        public override void Down()
        {
        }
    }
}
