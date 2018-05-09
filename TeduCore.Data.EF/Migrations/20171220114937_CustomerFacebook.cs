using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TeduCore.Data.EF.Migrations
{
    public partial class CustomerFacebook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_AppUsers_CustomerId",
                table: "Bills");

            migrationBuilder.DropIndex(
                name: "IX_Bills_CustomerId",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Bills");

            //migrationBuilder.AlterColumn<string>(
            //    name: "FunctionId",
            //    table: "Permissions",
            //    maxLength: 128,
            //    nullable: false,
            //    oldClrType: typeof(string),
            //    oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "CustomerMessage",
                table: "Bills",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 256);

            migrationBuilder.AddColumn<string>(
                name: "CustomerFacebook",
                table: "Bills",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ShippingFee",
                table: "Bills",
                nullable: true);

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
            migrationBuilder.DropColumn(
                name: "CustomerFacebook",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "ShippingFee",
                table: "Bills");

            migrationBuilder.AlterColumn<string>(
                name: "FunctionId",
                table: "Permissions",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "CustomerMessage",
                table: "Bills",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerId",
                table: "Bills",
                maxLength: 450,
                nullable: true);

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
                name: "IX_Bills_CustomerId",
                table: "Bills",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_AppUsers_CustomerId",
                table: "Bills",
                column: "CustomerId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
