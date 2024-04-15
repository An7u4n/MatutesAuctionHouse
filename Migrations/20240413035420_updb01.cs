using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MatutesAuctionHouse.Migrations
{
    /// <inheritdoc />
    public partial class updb01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "user_id",
                table: "Item",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Item_user_id",
                table: "Item",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Auction_item_id",
                table: "Auction",
                column: "item_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Auction_Item_item_id",
                table: "Auction",
                column: "item_id",
                principalTable: "Item",
                principalColumn: "item_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Item_User_user_id",
                table: "Item",
                column: "user_id",
                principalTable: "User",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auction_Item_item_id",
                table: "Auction");

            migrationBuilder.DropForeignKey(
                name: "FK_Item_User_user_id",
                table: "Item");

            migrationBuilder.DropIndex(
                name: "IX_Item_user_id",
                table: "Item");

            migrationBuilder.DropIndex(
                name: "IX_Auction_item_id",
                table: "Auction");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "Item");
        }
    }
}
