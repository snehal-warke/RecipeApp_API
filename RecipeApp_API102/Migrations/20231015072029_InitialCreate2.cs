using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeAppAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Ingredient",
                table: "Ingredient");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Recipes");

            migrationBuilder.RenameTable(
                name: "Ingredient",
                newName: "Ingredients");

            migrationBuilder.RenameColumn(
                name: "RecipeName",
                table: "Recipes",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Ingredients",
                table: "Recipes",
                newName: "ImagePath");

            migrationBuilder.AddColumn<int>(
                name: "RecipeId",
                table: "Ingredients",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ingredients",
                table: "Ingredients",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_RecipeId",
                table: "Ingredients",
                column: "RecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_Recipes_RecipeId",
                table: "Ingredients",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "RecipeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_Recipes_RecipeId",
                table: "Ingredients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ingredients",
                table: "Ingredients");

            migrationBuilder.DropIndex(
                name: "IX_Ingredients_RecipeId",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "RecipeId",
                table: "Ingredients");

            migrationBuilder.RenameTable(
                name: "Ingredients",
                newName: "Ingredient");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Recipes",
                newName: "RecipeName");

            migrationBuilder.RenameColumn(
                name: "ImagePath",
                table: "Recipes",
                newName: "Ingredients");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Recipes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ingredient",
                table: "Ingredient",
                column: "IngredientId");
        }
    }
}
