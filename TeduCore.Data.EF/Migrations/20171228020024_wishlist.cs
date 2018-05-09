using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TeduCore.Data.EF.Migrations
{
    public partial class wishlist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AlterColumn<string>(
            //    name: "FunctionId",
            //    table: "Permissions",
            //    maxLength: 128,
            //    nullable: false,
            //    oldClrType: typeof(string),
            //    oldMaxLength: 128);

            //migrationBuilder.AlterColumn<string>(
            //    name: "AnnouncementId",
            //    table: "AnnouncementUsers",
            //    maxLength: 128,
            //    nullable: false,
            //    oldClrType: typeof(string),
            //    oldMaxLength: 128);

            //migrationBuilder.AlterColumn<string>(
            //    name: "PageId",
            //    table: "AdvertistmentPositions",
            //    maxLength: 20,
            //    nullable: true,
            //    oldClrType: typeof(string),
            //    oldMaxLength: 20,
            //    oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ProductWishlists",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductWishlists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductWishlists_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductWishlists_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductWishlists_ProductId",
                table: "ProductWishlists",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductWishlists_UserId",
                table: "ProductWishlists",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductWishlists");

            migrationBuilder.AlterColumn<string>(
                name: "FunctionId",
                table: "Permissions",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "AnnouncementId",
                table: "AnnouncementUsers",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "PageId",
                table: "AdvertistmentPositions",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true);
        }
    }
}
