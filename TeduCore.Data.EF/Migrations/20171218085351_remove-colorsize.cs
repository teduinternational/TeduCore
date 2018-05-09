using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TeduCore.Data.EF.Migrations
{
    public partial class removecolorsize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillDetails_Colors_ColorId",
                table: "BillDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_BillDetails_Sizes_SizeId",
                table: "BillDetails");

            migrationBuilder.DropIndex(
                name: "IX_BillDetails_ColorId",
                table: "BillDetails");

            migrationBuilder.DropIndex(
                name: "IX_BillDetails_SizeId",
                table: "BillDetails");

            migrationBuilder.DropColumn(
                name: "ColorId",
                table: "BillDetails");

            migrationBuilder.DropColumn(
                name: "SizeId",
                table: "BillDetails");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FunctionId",
                table: "Permissions",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AddColumn<int>(
                name: "ColorId",
                table: "BillDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SizeId",
                table: "BillDetails",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.CreateIndex(
                name: "IX_BillDetails_ColorId",
                table: "BillDetails",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_BillDetails_SizeId",
                table: "BillDetails",
                column: "SizeId");

            migrationBuilder.AddForeignKey(
                name: "FK_BillDetails_Colors_ColorId",
                table: "BillDetails",
                column: "ColorId",
                principalTable: "Colors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BillDetails_Sizes_SizeId",
                table: "BillDetails",
                column: "SizeId",
                principalTable: "Sizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
