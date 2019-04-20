using Microsoft.EntityFrameworkCore.Migrations;

namespace angular_netcore.Migrations
{
    public partial class Add_Feature_model_Constraints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Features",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Features",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255);
        }
    }
}
