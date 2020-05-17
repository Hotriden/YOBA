using Microsoft.EntityFrameworkCore.Migrations;

namespace YOBA_LibraryData.DAL.Migrations
{
    public partial class Added_user : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "WareHouses",
                unicode: false,
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Expences",
                unicode: false,
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    LastName = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Email = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Password = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    ConfirmPassword = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "WareHouses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Expences",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 50);
        }
    }
}
