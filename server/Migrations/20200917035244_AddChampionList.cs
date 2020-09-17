using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace server.Migrations
{
    public partial class AddChampionList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChampionLists",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChampionLists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChampionListChampion",
                columns: table => new
                {
                    ChampionId = table.Column<Guid>(nullable: false),
                    ChampionListId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChampionListChampion", x => new { x.ChampionId, x.ChampionListId });
                    table.ForeignKey(
                        name: "FK_ChampionListChampion_Champions_ChampionId",
                        column: x => x.ChampionId,
                        principalTable: "Champions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChampionListChampion_ChampionLists_ChampionListId",
                        column: x => x.ChampionListId,
                        principalTable: "ChampionLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChampionListChampion_ChampionListId",
                table: "ChampionListChampion",
                column: "ChampionListId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChampionListChampion");

            migrationBuilder.DropTable(
                name: "ChampionLists");
        }
    }
}
