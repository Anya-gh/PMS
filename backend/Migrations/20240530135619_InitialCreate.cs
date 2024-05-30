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
                    NatureDetails_Name = table.Column<string>(type: "TEXT", nullable: false),
                    NatureDetails_Atk = table.Column<float>(type: "REAL", nullable: true),
                    NatureDetails_Def = table.Column<float>(type: "REAL", nullable: true),
                    NatureDetails_SpAtk = table.Column<float>(type: "REAL", nullable: true),
                    NatureDetails_SpDef = table.Column<float>(type: "REAL", nullable: true),
                    NatureDetails_Spd = table.Column<float>(type: "REAL", nullable: true),
                    Move1_Name = table.Column<string>(type: "TEXT", nullable: true),
                    Move1_Power = table.Column<int>(type: "INTEGER", nullable: true),
                    Move1_PP = table.Column<int>(type: "INTEGER", nullable: true),
                    Move1_Accuracy = table.Column<int>(type: "INTEGER", nullable: true),
                    Move1_Description = table.Column<string>(type: "TEXT", nullable: true),
                    Move1_Type = table.Column<string>(type: "TEXT", nullable: true),
                    Move2_Name = table.Column<string>(type: "TEXT", nullable: true),
                    Move2_Power = table.Column<int>(type: "INTEGER", nullable: true),
                    Move2_PP = table.Column<int>(type: "INTEGER", nullable: true),
                    Move2_Accuracy = table.Column<int>(type: "INTEGER", nullable: true),
                    Move2_Description = table.Column<string>(type: "TEXT", nullable: true),
                    Move2_Type = table.Column<string>(type: "TEXT", nullable: true),
                    Move3_Name = table.Column<string>(type: "TEXT", nullable: true),
                    Move3_Power = table.Column<int>(type: "INTEGER", nullable: true),
                    Move3_PP = table.Column<int>(type: "INTEGER", nullable: true),
                    Move3_Accuracy = table.Column<int>(type: "INTEGER", nullable: true),
                    Move3_Description = table.Column<string>(type: "TEXT", nullable: true),
                    Move3_Type = table.Column<string>(type: "TEXT", nullable: true),
                    Move4_Name = table.Column<string>(type: "TEXT", nullable: true),
                    Move4_Power = table.Column<int>(type: "INTEGER", nullable: true),
                    Move4_PP = table.Column<int>(type: "INTEGER", nullable: true),
                    Move4_Accuracy = table.Column<int>(type: "INTEGER", nullable: true),
                    Move4_Description = table.Column<string>(type: "TEXT", nullable: true),
                    Move4_Type = table.Column<string>(type: "TEXT", nullable: true),
                    Ability_Name = table.Column<string>(type: "TEXT", nullable: false),
                    Ability_Description = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
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
