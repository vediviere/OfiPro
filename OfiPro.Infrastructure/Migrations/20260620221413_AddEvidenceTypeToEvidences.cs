using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OfiPro.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEvidenceTypeToEvidences : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EvidenceType",
                table: "Evidences",
                type: "int",
                nullable: false,
                defaultValue: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EvidenceType",
                table: "Evidences");
        }
    }
}
