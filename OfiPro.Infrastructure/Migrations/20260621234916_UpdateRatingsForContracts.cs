using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OfiPro.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRatingsForContracts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Projects_ProjectId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Users_EvaluatedUserId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Users_EvaluatorUserId",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_EvaluatedUserId",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "Communication",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "CostBenefit",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "Professionalism",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "Punctuality",
                table: "Ratings");

            migrationBuilder.RenameColumn(
                name: "Quality",
                table: "Ratings",
                newName: "Score");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "Ratings",
                newName: "RaterUserId");

            migrationBuilder.RenameColumn(
                name: "EvaluatorUserId",
                table: "Ratings",
                newName: "RatedUserId");

            migrationBuilder.RenameColumn(
                name: "EvaluatedUserId",
                table: "Ratings",
                newName: "ContractId");

            migrationBuilder.RenameIndex(
                name: "IX_Ratings_ProjectId",
                table: "Ratings",
                newName: "IX_Ratings_RaterUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Ratings_EvaluatorUserId",
                table: "Ratings",
                newName: "IX_Ratings_RatedUserId");

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "Ratings",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1500)",
                oldMaxLength: 1500);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Ratings",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_ContractId_RaterUserId_RatedUserId",
                table: "Ratings",
                columns: new[] { "ContractId", "RaterUserId", "RatedUserId" },
                unique: true,
                filter: "[DeletedAt] IS NULL");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Ratings_Score_Range",
                table: "Ratings",
                sql: "[Score] >= 1 AND [Score] <= 5");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Contracts_ContractId",
                table: "Ratings",
                column: "ContractId",
                principalTable: "Contracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Users_RatedUserId",
                table: "Ratings",
                column: "RatedUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Users_RaterUserId",
                table: "Ratings",
                column: "RaterUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Contracts_ContractId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Users_RatedUserId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Users_RaterUserId",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_ContractId_RaterUserId_RatedUserId",
                table: "Ratings");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Ratings_Score_Range",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Ratings");

            migrationBuilder.RenameColumn(
                name: "Score",
                table: "Ratings",
                newName: "Quality");

            migrationBuilder.RenameColumn(
                name: "RaterUserId",
                table: "Ratings",
                newName: "ProjectId");

            migrationBuilder.RenameColumn(
                name: "RatedUserId",
                table: "Ratings",
                newName: "EvaluatorUserId");

            migrationBuilder.RenameColumn(
                name: "ContractId",
                table: "Ratings",
                newName: "EvaluatedUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Ratings_RaterUserId",
                table: "Ratings",
                newName: "IX_Ratings_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Ratings_RatedUserId",
                table: "Ratings",
                newName: "IX_Ratings_EvaluatorUserId");

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "Ratings",
                type: "nvarchar(1500)",
                maxLength: 1500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AddColumn<int>(
                name: "Communication",
                table: "Ratings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CostBenefit",
                table: "Ratings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Professionalism",
                table: "Ratings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Punctuality",
                table: "Ratings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_EvaluatedUserId",
                table: "Ratings",
                column: "EvaluatedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Projects_ProjectId",
                table: "Ratings",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Users_EvaluatedUserId",
                table: "Ratings",
                column: "EvaluatedUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Users_EvaluatorUserId",
                table: "Ratings",
                column: "EvaluatorUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
