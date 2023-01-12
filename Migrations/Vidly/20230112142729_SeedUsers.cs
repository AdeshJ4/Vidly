using Microsoft.EntityFrameworkCore.Migrations;
using System.Security;

#nullable disable

namespace Vidly.Migrations.Vidly
{
    /// <inheritdoc />
    public partial class SeedUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO[dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES(N'6133930a-936d-4d1d-a6f2-09fdfbd7328d', N'admin@vidly.com', N'ADMIN@VIDLY.COM', N'admin@vidly.com', N'ADMIN@VIDLY.COM', 1, N'AQAAAAEAACcQAAAAEJFD4jGUGbwDDnZpKifgvc2HjhUjQbE9J8CSHagCQAK1uI/IXjBuLwbeGC/l1aTrTg==', N'W4L2XJAEBVL62XKXWVTWINRJJTOCYK64', N'bbffd8fd-aa48-4414-8e9c-eda17181a34d', N'9527370288', 0, 0, NULL, 1, 0)");
            migrationBuilder.Sql("INSERT INTO[dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES(N'86d77efa-bb1f-43f6-8817-9f1779adce30', N'guest@vidly.com', N'GUEST@VIDLY.COM', N'guest@vidly.com', N'GUEST@VIDLY.COM', 1, N'AQAAAAEAACcQAAAAEOkFJzszFCG/dudg2inKp8j/KBUPhJQTlJDbyTfpYhN2a4kwUGH3ln9XH4m1QWUOtg==', N'TPGBJHIR5S4JHP27TNYMGL22BWBVQBMU', N'6d839929-1aa1-4f33-b6a2-cfbe8862e729', N'7721031355', 0, 0, NULL, 1, 0)");
            migrationBuilder.Sql("INSERT INTO [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'673aff65-adc1-480f-9474-9f2a62886fca', N'CanManageMovie', N'CANMANAGEMOVIE', N'6517374d-ae25-4ba4-89d6-e6974acf80b6')");
            migrationBuilder.Sql("INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'6133930a-936d-4d1d-a6f2-09fdfbd7328d', N'673aff65-adc1-480f-9474-9f2a62886fca')\r\n");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
        }
    }
}
