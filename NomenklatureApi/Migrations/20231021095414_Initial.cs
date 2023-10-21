using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace NomenklatureApi.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Nomenklatures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nomenklatures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Links",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ChildIdId = table.Column<int>(type: "integer", nullable: false),
                    ParentIdId = table.Column<int>(type: "integer", nullable: false),
                    Count = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Links_Nomenklatures_ChildIdId",
                        column: x => x.ChildIdId,
                        principalTable: "Nomenklatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Links_Nomenklatures_ParentIdId",
                        column: x => x.ParentIdId,
                        principalTable: "Nomenklatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductMetaDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NomenklatureId = table.Column<int>(type: "integer", nullable: false),
                    MetaDataName = table.Column<string>(type: "text", nullable: false),
                    MetaDataValue = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductMetaDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductMetaDatas_Nomenklatures_NomenklatureId",
                        column: x => x.NomenklatureId,
                        principalTable: "Nomenklatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Links_ChildIdId",
                table: "Links",
                column: "ChildIdId");

            migrationBuilder.CreateIndex(
                name: "IX_Links_ParentIdId",
                table: "Links",
                column: "ParentIdId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductMetaDatas_NomenklatureId",
                table: "ProductMetaDatas",
                column: "NomenklatureId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Links");

            migrationBuilder.DropTable(
                name: "ProductMetaDatas");

            migrationBuilder.DropTable(
                name: "Nomenklatures");
        }
    }
}
