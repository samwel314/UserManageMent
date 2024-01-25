using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace User__Management.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddrolesToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("insert into AspNetUserRoles (UserId , RoleId)\r\nselect 'd7086026-6ac1-4ba4-b419-2fe92e0ba55d' ,   Id  from   AspNetRoles"); 
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from  AspNetUserRoles where UserId = 'd7086026-6ac1-4ba4-b419-2fe92e0ba55d'");
        }
    }
}
