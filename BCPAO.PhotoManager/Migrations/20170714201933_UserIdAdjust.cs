using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BCPAO.PhotoManager.Migrations
{
    public partial class UserIdAdjust : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Photos",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "ImageData",
                table: "Photos",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Photos",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<byte[]>(
                name: "ImageData",
                table: "Photos",
                nullable: true,
                oldClrType: typeof(byte[]));
        }
    }
}
