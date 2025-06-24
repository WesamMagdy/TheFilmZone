using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilmZone.Migrations
{
    /// <inheritdoc />
    public partial class UseSetterAndGetter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movie_Category_CategoryId",
                table: "Movie");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieStreamingService_Movie_MovieId",
                table: "MovieStreamingService");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieStreamingService_StreamingService_StreamingServiceId",
                table: "MovieStreamingService");

            migrationBuilder.DropForeignKey(
                name: "FK_UserMovie_AspNetUsers_UserId",
                table: "UserMovie");

            migrationBuilder.DropForeignKey(
                name: "FK_UserMovie_Movie_MovieId",
                table: "UserMovie");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserMovie",
                table: "UserMovie");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StreamingService",
                table: "StreamingService");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieStreamingService",
                table: "MovieStreamingService");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Movie",
                table: "Movie");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.RenameTable(
                name: "UserMovie",
                newName: "UserMovies");

            migrationBuilder.RenameTable(
                name: "StreamingService",
                newName: "StreamingServices");

            migrationBuilder.RenameTable(
                name: "MovieStreamingService",
                newName: "MovieStreamingServices");

            migrationBuilder.RenameTable(
                name: "Movie",
                newName: "Movies");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "Categories");

            migrationBuilder.RenameIndex(
                name: "IX_UserMovie_UserId_MovieId",
                table: "UserMovies",
                newName: "IX_UserMovies_UserId_MovieId");

            migrationBuilder.RenameIndex(
                name: "IX_UserMovie_MovieId",
                table: "UserMovies",
                newName: "IX_UserMovies_MovieId");

            migrationBuilder.RenameIndex(
                name: "IX_MovieStreamingService_StreamingServiceId",
                table: "MovieStreamingServices",
                newName: "IX_MovieStreamingServices_StreamingServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Movie_CategoryId",
                table: "Movies",
                newName: "IX_Movies_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserMovies",
                table: "UserMovies",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StreamingServices",
                table: "StreamingServices",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieStreamingServices",
                table: "MovieStreamingServices",
                columns: new[] { "MovieId", "StreamingServiceId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Movies",
                table: "Movies",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Categories_CategoryId",
                table: "Movies",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieStreamingServices_Movies_MovieId",
                table: "MovieStreamingServices",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieStreamingServices_StreamingServices_StreamingServiceId",
                table: "MovieStreamingServices",
                column: "StreamingServiceId",
                principalTable: "StreamingServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserMovies_AspNetUsers_UserId",
                table: "UserMovies",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserMovies_Movies_MovieId",
                table: "UserMovies",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Categories_CategoryId",
                table: "Movies");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieStreamingServices_Movies_MovieId",
                table: "MovieStreamingServices");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieStreamingServices_StreamingServices_StreamingServiceId",
                table: "MovieStreamingServices");

            migrationBuilder.DropForeignKey(
                name: "FK_UserMovies_AspNetUsers_UserId",
                table: "UserMovies");

            migrationBuilder.DropForeignKey(
                name: "FK_UserMovies_Movies_MovieId",
                table: "UserMovies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserMovies",
                table: "UserMovies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StreamingServices",
                table: "StreamingServices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieStreamingServices",
                table: "MovieStreamingServices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Movies",
                table: "Movies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "UserMovies",
                newName: "UserMovie");

            migrationBuilder.RenameTable(
                name: "StreamingServices",
                newName: "StreamingService");

            migrationBuilder.RenameTable(
                name: "MovieStreamingServices",
                newName: "MovieStreamingService");

            migrationBuilder.RenameTable(
                name: "Movies",
                newName: "Movie");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Category");

            migrationBuilder.RenameIndex(
                name: "IX_UserMovies_UserId_MovieId",
                table: "UserMovie",
                newName: "IX_UserMovie_UserId_MovieId");

            migrationBuilder.RenameIndex(
                name: "IX_UserMovies_MovieId",
                table: "UserMovie",
                newName: "IX_UserMovie_MovieId");

            migrationBuilder.RenameIndex(
                name: "IX_MovieStreamingServices_StreamingServiceId",
                table: "MovieStreamingService",
                newName: "IX_MovieStreamingService_StreamingServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Movies_CategoryId",
                table: "Movie",
                newName: "IX_Movie_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserMovie",
                table: "UserMovie",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StreamingService",
                table: "StreamingService",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieStreamingService",
                table: "MovieStreamingService",
                columns: new[] { "MovieId", "StreamingServiceId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Movie",
                table: "Movie",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Movie_Category_CategoryId",
                table: "Movie",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieStreamingService_Movie_MovieId",
                table: "MovieStreamingService",
                column: "MovieId",
                principalTable: "Movie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieStreamingService_StreamingService_StreamingServiceId",
                table: "MovieStreamingService",
                column: "StreamingServiceId",
                principalTable: "StreamingService",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserMovie_AspNetUsers_UserId",
                table: "UserMovie",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserMovie_Movie_MovieId",
                table: "UserMovie",
                column: "MovieId",
                principalTable: "Movie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
