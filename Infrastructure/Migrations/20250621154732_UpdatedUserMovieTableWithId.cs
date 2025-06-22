using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilmZone.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedUserMovieTableWithId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserMovie",
                table: "UserMovie");

            migrationBuilder.DropIndex(
                name: "IX_UserMovie_UserId",
                table: "UserMovie");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UserMovie",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserMovie",
                table: "UserMovie",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserMovie_MovieId",
                table: "UserMovie",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMovie_UserId_MovieId",
                table: "UserMovie",
                columns: new[] { "UserId", "MovieId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserMovie",
                table: "UserMovie");

            migrationBuilder.DropIndex(
                name: "IX_UserMovie_MovieId",
                table: "UserMovie");

            migrationBuilder.DropIndex(
                name: "IX_UserMovie_UserId_MovieId",
                table: "UserMovie");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserMovie");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserMovie",
                table: "UserMovie",
                columns: new[] { "MovieId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_UserMovie_UserId",
                table: "UserMovie",
                column: "UserId");
        }
    }
}
