using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieBooking.DataContext.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MovieBookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreferedSlot = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SeatsBooked = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieBookings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MovieData",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Genre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MovieDirector = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TheatreName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Duration = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieData", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MovieTiming",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShowTimings = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SeatsCountLeft = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieTiming", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieBookings");

            migrationBuilder.DropTable(
                name: "MovieData");

            migrationBuilder.DropTable(
                name: "MovieTiming");
        }
    }
}
