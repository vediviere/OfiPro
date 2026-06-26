using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OfiPro.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefactorInvitationsForContractorInvites : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Invitations_ProjectId",
                table: "Invitations");

            migrationBuilder.DropColumn(
                name: "InvitedName",
                table: "Invitations");

            migrationBuilder.DropColumn(
                name: "InvitedPhone",
                table: "Invitations");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Invitations",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "InvitedContractorUserId",
                table: "Invitations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "Invitations",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "RespondedAt",
                table: "Invitations",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_InvitedContractorUserId",
                table: "Invitations",
                column: "InvitedContractorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_ProjectId_InvitedContractorUserId_Status",
                table: "Invitations",
                columns: new[] { "ProjectId", "InvitedContractorUserId", "Status" });

            migrationBuilder.AddForeignKey(
                name: "FK_Invitations_Users_InvitedContractorUserId",
                table: "Invitations",
                column: "InvitedContractorUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invitations_Users_InvitedContractorUserId",
                table: "Invitations");

            migrationBuilder.DropIndex(
                name: "IX_Invitations_InvitedContractorUserId",
                table: "Invitations");

            migrationBuilder.DropIndex(
                name: "IX_Invitations_ProjectId_InvitedContractorUserId_Status",
                table: "Invitations");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Invitations");

            migrationBuilder.DropColumn(
                name: "InvitedContractorUserId",
                table: "Invitations");

            migrationBuilder.DropColumn(
                name: "Message",
                table: "Invitations");

            migrationBuilder.DropColumn(
                name: "RespondedAt",
                table: "Invitations");

            migrationBuilder.AddColumn<string>(
                name: "InvitedName",
                table: "Invitations",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "InvitedPhone",
                table: "Invitations",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_ProjectId",
                table: "Invitations",
                column: "ProjectId");
        }
    }
}
