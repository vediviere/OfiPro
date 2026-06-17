using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OfiPro.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeProposalToProjectRequirement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Proposals_Projects_ProjectId",
                table: "Proposals");

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectRequirementId",
                table: "Proposals",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Proposals_ProjectRequirementId",
                table: "Proposals",
                column: "ProjectRequirementId");

            migrationBuilder.AddForeignKey(
                name: "FK_Proposals_ProjectRequirements_ProjectRequirementId",
                table: "Proposals",
                column: "ProjectRequirementId",
                principalTable: "ProjectRequirements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Proposals_Projects_ProjectId",
                table: "Proposals",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Proposals_ProjectRequirements_ProjectRequirementId",
                table: "Proposals");

            migrationBuilder.DropForeignKey(
                name: "FK_Proposals_Projects_ProjectId",
                table: "Proposals");

            migrationBuilder.DropIndex(
                name: "IX_Proposals_ProjectRequirementId",
                table: "Proposals");

            migrationBuilder.DropColumn(
                name: "ProjectRequirementId",
                table: "Proposals");

            migrationBuilder.AddForeignKey(
                name: "FK_Proposals_Projects_ProjectId",
                table: "Proposals",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
