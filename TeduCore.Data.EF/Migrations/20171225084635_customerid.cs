using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TeduCore.Data.EF.Migrations
{
    public partial class customerid : Migration
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

            migrationBuilder.AddColumn<string>(
                name: "CustomerId",
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
                name: "CustomerId",
                table: "Bills");

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
