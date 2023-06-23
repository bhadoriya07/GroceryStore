using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class mappingUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartProduct_Carts_CartId",
                table: "CartProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_CartProduct_Products_ProductId",
                table: "CartProduct");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "CartProduct",
                newName: "ProductsId");

            migrationBuilder.RenameColumn(
                name: "CartId",
                table: "CartProduct",
                newName: "CartsId");

            migrationBuilder.RenameIndex(
                name: "IX_CartProduct_ProductId",
                table: "CartProduct",
                newName: "IX_CartProduct_ProductsId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartProduct_Carts_CartsId",
                table: "CartProduct",
                column: "CartsId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CartProduct_Products_ProductsId",
                table: "CartProduct",
                column: "ProductsId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartProduct_Carts_CartsId",
                table: "CartProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_CartProduct_Products_ProductsId",
                table: "CartProduct");

            migrationBuilder.RenameColumn(
                name: "ProductsId",
                table: "CartProduct",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "CartsId",
                table: "CartProduct",
                newName: "CartId");

            migrationBuilder.RenameIndex(
                name: "IX_CartProduct_ProductsId",
                table: "CartProduct",
                newName: "IX_CartProduct_ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartProduct_Carts_CartId",
                table: "CartProduct",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CartProduct_Products_ProductId",
                table: "CartProduct",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
