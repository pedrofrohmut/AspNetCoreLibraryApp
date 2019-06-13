using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryData.Migrations
{
    public partial class LibraryAssets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckoutHistories_LibraryAsset_LibraryAssetId",
                table: "CheckoutHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_Checkouts_LibraryAsset_LibraryAssetId",
                table: "Checkouts");

            migrationBuilder.DropForeignKey(
                name: "FK_Holds_LibraryAsset_LibraryAssetId",
                table: "Holds");

            migrationBuilder.DropTable(
                name: "LibraryAsset");

            migrationBuilder.CreateTable(
                name: "LibraryAssets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Year = table.Column<int>(nullable: false),
                    StatusId = table.Column<int>(nullable: false),
                    Cost = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    ImageUrl = table.Column<string>(nullable: true),
                    NumberOfCopies = table.Column<int>(nullable: false),
                    LocationId = table.Column<int>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    ISBN = table.Column<string>(nullable: true),
                    Author = table.Column<string>(nullable: true),
                    DeweyIndex = table.Column<string>(nullable: true),
                    Director = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibraryAssets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LibraryAssets_Librarybranches_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Librarybranches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LibraryAssets_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LibraryAssets_LocationId",
                table: "LibraryAssets",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_LibraryAssets_StatusId",
                table: "LibraryAssets",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_CheckoutHistories_LibraryAssets_LibraryAssetId",
                table: "CheckoutHistories",
                column: "LibraryAssetId",
                principalTable: "LibraryAssets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Checkouts_LibraryAssets_LibraryAssetId",
                table: "Checkouts",
                column: "LibraryAssetId",
                principalTable: "LibraryAssets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Holds_LibraryAssets_LibraryAssetId",
                table: "Holds",
                column: "LibraryAssetId",
                principalTable: "LibraryAssets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckoutHistories_LibraryAssets_LibraryAssetId",
                table: "CheckoutHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_Checkouts_LibraryAssets_LibraryAssetId",
                table: "Checkouts");

            migrationBuilder.DropForeignKey(
                name: "FK_Holds_LibraryAssets_LibraryAssetId",
                table: "Holds");

            migrationBuilder.DropTable(
                name: "LibraryAssets");

            migrationBuilder.CreateTable(
                name: "LibraryAsset",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Cost = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    ImageUrl = table.Column<string>(nullable: true),
                    LocationId = table.Column<int>(nullable: true),
                    NumberOfCopies = table.Column<int>(nullable: false),
                    StatusId = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false),
                    Author = table.Column<string>(nullable: true),
                    DeweyIndex = table.Column<string>(nullable: true),
                    ISBN = table.Column<string>(nullable: true),
                    Director = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibraryAsset", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LibraryAsset_Librarybranches_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Librarybranches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LibraryAsset_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LibraryAsset_LocationId",
                table: "LibraryAsset",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_LibraryAsset_StatusId",
                table: "LibraryAsset",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_CheckoutHistories_LibraryAsset_LibraryAssetId",
                table: "CheckoutHistories",
                column: "LibraryAssetId",
                principalTable: "LibraryAsset",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Checkouts_LibraryAsset_LibraryAssetId",
                table: "Checkouts",
                column: "LibraryAssetId",
                principalTable: "LibraryAsset",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Holds_LibraryAsset_LibraryAssetId",
                table: "Holds",
                column: "LibraryAssetId",
                principalTable: "LibraryAsset",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
