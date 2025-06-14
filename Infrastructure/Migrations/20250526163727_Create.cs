using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FilmZone.Migrations
{
    /// <inheritdoc />
    public partial class Create : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movie",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cover = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movie", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movie_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StreamingService",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MovieId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StreamingService", x => x.id);
                    table.ForeignKey(
                        name: "FK_StreamingService_Movie_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movie",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MovieStreamingService",
                columns: table => new
                {
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    StreamingServiceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieStreamingService", x => new { x.MovieId, x.StreamingServiceId });
                    table.ForeignKey(
                        name: "FK_MovieStreamingService_Movie_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieStreamingService_StreamingService_StreamingServiceId",
                        column: x => x.StreamingServiceId,
                        principalTable: "StreamingService",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Action" },
                    { 2, "Comedy" },
                    { 3, "Horror" },
                    { 4, "Drama" },
                    { 5, "Fantasy" },
                    { 6, "Science Fiction" },
                    { 7, "Romance" },
                    { 8, "Thriller" },
                    { 9, "Documentary" },
                    { 10, "Animation" }
                });

            migrationBuilder.InsertData(
                table: "StreamingService",
                columns: new[] { "id", "Logo", "MovieId", "Name" },
                values: new object[,]
                {
                    { 1, "NetflixLogo", null, "Netflix" },
                    { 2, "AmazonPrimeLogo", null, "Amazon Prime" },
                    { 3, "DisneyPlusLogo", null, "Disney+" },
                    { 4, "HuluLogo", null, "Hulu" },
                    { 5, "HBOMaxLogo", null, "HBO Max" },
                    { 6, "AppleTVPlusLogo", null, "Apple TV+" },
                    { 7, "WatchItLogo", null, "WatchIt" },
                    { 8, "ShahidLogo", null, "Shahid" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movie_CategoryId",
                table: "Movie",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieStreamingService_StreamingServiceId",
                table: "MovieStreamingService",
                column: "StreamingServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_StreamingService_MovieId",
                table: "StreamingService",
                column: "MovieId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieStreamingService");

            migrationBuilder.DropTable(
                name: "StreamingService");

            migrationBuilder.DropTable(
                name: "Movie");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
