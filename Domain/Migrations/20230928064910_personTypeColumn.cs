using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    public partial class personTypeColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stars_Persons_PersonId",
                table: "Stars");

            migrationBuilder.DropForeignKey(
                name: "FK_Talents_Persons_PersonId",
                table: "Talents");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                table: "Talents",
                newName: "personId");

            migrationBuilder.RenameIndex(
                name: "IX_Talents_PersonId",
                table: "Talents",
                newName: "IX_Talents_personId");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                table: "Stars",
                newName: "personId");

            migrationBuilder.RenameIndex(
                name: "IX_Stars_PersonId",
                table: "Stars",
                newName: "IX_Stars_personId");

            migrationBuilder.AlterColumn<Guid>(
                name: "personId",
                table: "Talents",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "personId",
                table: "Stars",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PersonType",
                table: "Persons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Stars_Persons_personId",
                table: "Stars",
                column: "personId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Talents_Persons_personId",
                table: "Talents",
                column: "personId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stars_Persons_personId",
                table: "Stars");

            migrationBuilder.DropForeignKey(
                name: "FK_Talents_Persons_personId",
                table: "Talents");

            migrationBuilder.DropColumn(
                name: "PersonType",
                table: "Persons");

            migrationBuilder.RenameColumn(
                name: "personId",
                table: "Talents",
                newName: "PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_Talents_personId",
                table: "Talents",
                newName: "IX_Talents_PersonId");

            migrationBuilder.RenameColumn(
                name: "personId",
                table: "Stars",
                newName: "PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_Stars_personId",
                table: "Stars",
                newName: "IX_Stars_PersonId");

            migrationBuilder.AlterColumn<Guid>(
                name: "PersonId",
                table: "Talents",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "PersonId",
                table: "Stars",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Stars_Persons_PersonId",
                table: "Stars",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Talents_Persons_PersonId",
                table: "Talents",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id");
        }
    }
}
