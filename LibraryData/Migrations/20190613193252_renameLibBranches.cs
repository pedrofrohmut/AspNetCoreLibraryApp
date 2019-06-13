using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryData.Migrations
{
    public partial class renameLibBranches : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BranchHours_Librarybranches_LibraryBranchId",
                table: "BranchHours");

            migrationBuilder.DropForeignKey(
                name: "FK_LibraryAssets_Librarybranches_LocationId",
                table: "LibraryAssets");

            migrationBuilder.DropForeignKey(
                name: "FK_Patrons_Librarybranches_HomeLibrarybranchId",
                table: "Patrons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Librarybranches",
                table: "Librarybranches");

            migrationBuilder.RenameTable(
                name: "Librarybranches",
                newName: "LibraryBranches");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LibraryBranches",
                table: "LibraryBranches",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BranchHours_LibraryBranches_LibraryBranchId",
                table: "BranchHours",
                column: "LibraryBranchId",
                principalTable: "LibraryBranches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LibraryAssets_LibraryBranches_LocationId",
                table: "LibraryAssets",
                column: "LocationId",
                principalTable: "LibraryBranches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Patrons_LibraryBranches_HomeLibrarybranchId",
                table: "Patrons",
                column: "HomeLibrarybranchId",
                principalTable: "LibraryBranches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BranchHours_LibraryBranches_LibraryBranchId",
                table: "BranchHours");

            migrationBuilder.DropForeignKey(
                name: "FK_LibraryAssets_LibraryBranches_LocationId",
                table: "LibraryAssets");

            migrationBuilder.DropForeignKey(
                name: "FK_Patrons_LibraryBranches_HomeLibrarybranchId",
                table: "Patrons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LibraryBranches",
                table: "LibraryBranches");

            migrationBuilder.RenameTable(
                name: "LibraryBranches",
                newName: "Librarybranches");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Librarybranches",
                table: "Librarybranches",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BranchHours_Librarybranches_LibraryBranchId",
                table: "BranchHours",
                column: "LibraryBranchId",
                principalTable: "Librarybranches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LibraryAssets_Librarybranches_LocationId",
                table: "LibraryAssets",
                column: "LocationId",
                principalTable: "Librarybranches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Patrons_Librarybranches_HomeLibrarybranchId",
                table: "Patrons",
                column: "HomeLibrarybranchId",
                principalTable: "Librarybranches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
