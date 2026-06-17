using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OfiPro.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveOldProjectRelationFromProposal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Proposals_Projects_ProjectId",
                table: "Proposals");

            migrationBuilder.DropIndex(
                name: "IX_Proposals_ProjectId",
                table: "Proposals");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Proposals");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProjectId",
                table: "Proposals",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Proposals_ProjectId",
                table: "Proposals",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Proposals_Projects_ProjectId",
                table: "Proposals",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
