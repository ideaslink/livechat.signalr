using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace livechat.signalr.Migrations
{
    /// <inheritdoc />
    public partial class updategroupuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GroupAlias",
                table: "Groups",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "Groups",
                type: "TEXT",
                maxLength: 255,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GroupAlias",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "Icon",
                table: "Groups");
        }
    }
}
