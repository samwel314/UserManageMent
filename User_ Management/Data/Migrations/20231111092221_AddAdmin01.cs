using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace User__Management.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAdmin01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql
                ("INSERT INTO [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [ImageProfile], [LastName]) VALUES (N'd7086026-6ac1-4ba4-b419-2fe92e0ba55d', N'admin', N'ADMIN', N'admin@test.com', N'ADMIN@TEST.COM', 0, N'AQAAAAIAAYagAAAAEBotl6TpjDEK/SZuU/6uc/RArsfo8wtkd+h+WuPNl/sdxNBrWHUuCXn1mBMCJuCxZg==', N'U56KPPVHPYIF7OZMWTAGCYISZFCBYTM5', N'cf35e904-a760-4eab-ac0b-7614895621ab', NULL, 0, 0, NULL, 1, 0, N'HelloAdmin', NULL, N'testAdmin')"); 
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from AspNetUsers where Id = 'd7086026-6ac1-4ba4-b419-2fe92e0ba55d'");
        }
    }
}
