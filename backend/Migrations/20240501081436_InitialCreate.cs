using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PokemonItems",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PokeID = table.Column<int>(type: "INTEGER", nullable: false),
                    HP = table.Column<int>(type: "INTEGER", nullable: false),
                    Atk = table.Column<int>(type: "INTEGER", nullable: false),
                    Def = table.Column<int>(type: "INTEGER", nullable: false),
                    SpAtk = table.Column<int>(type: "INTEGER", nullable: false),
                    SpDef = table.Column<int>(type: "INTEGER", nullable: false),
                    Spd = table.Column<int>(type: "INTEGER", nullable: false),
                    Move1 = table.Column<int>(type: "INTEGER", nullable: true),
                    Move2 = table.Column<int>(type: "INTEGER", nullable: true),
                    Move3 = table.Column<int>(type: "INTEGER", nullable: true),
                    Move4 = table.Column<int>(type: "INTEGER", nullable: true),
                    Ability = table.Column<int>(type: "INTEGER", nullable: false),
                    Level = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonItems", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PokemonItems");
        }
    }
}
