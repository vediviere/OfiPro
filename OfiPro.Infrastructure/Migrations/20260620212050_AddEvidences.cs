using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OfiPro.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEvidences : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evidences_Projects_ProjectId",
                table: "Evidences");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Evidences");

            migrationBuilder.RenameColumn(
                name: "Url",
                table: "Evidences",
                newName: "FileUrl");

            migrationBuilder.RenameColumn(
                name: "UploadedAt",
                table: "Evidences",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "Evidences",
                newName: "ContractId");

            migrationBuilder.RenameIndex(
                name: "IX_Evidences_ProjectId",
                table: "Evidences",
                newName: "IX_Evidences_ContractId");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Evidences",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Evidences",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FileType",
                table: "Evidences",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Evidences",
                type: "nvarchar(120)",
                maxLength: 120,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Evidences_Contracts_ContractId",
                table: "Evidences",
                column: "ContractId",
                principalTable: "Contracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evidences_Contracts_ContractId",
                table: "Evidences");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Evidences");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Evidences");

            migrationBuilder.DropColumn(
                name: "FileType",
                table: "Evidences");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Evidences");

            migrationBuilder.RenameColumn(
                name: "FileUrl",
                table: "Evidences",
                newName: "Url");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Evidences",
                newName: "UploadedAt");

            migrationBuilder.RenameColumn(
                name: "ContractId",
                table: "Evidences",
                newName: "ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Evidences_ContractId",
                table: "Evidences",
                newName: "IX_Evidences_ProjectId");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Evidences",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Evidences_Projects_ProjectId",
                table: "Evidences",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
