using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TeduCore.Data.EF.Migrations
{
    public partial class productquantity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Products",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Products",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "ProductCategories",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CurrentIdentity",
                table: "ProductCategories",
                nullable: false,
                defaultValue: 0);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "ProductCategories");

            migrationBuilder.DropColumn(
                name: "CurrentIdentity",
                table: "ProductCategories");

       
        }
    }
}
