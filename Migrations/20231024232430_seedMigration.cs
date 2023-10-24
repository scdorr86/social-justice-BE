using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace social_justice_BE.Migrations
{
    public partial class seedMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meetups_Organizations_OrganizationId",
                table: "Meetups");

            migrationBuilder.DropForeignKey(
                name: "FK_Members_Organizations_OrganizationId",
                table: "Members");

            migrationBuilder.AlterColumn<string>(
                name: "Uid",
                table: "Members",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "OrganizationId",
                table: "Members",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OrganizationId",
                table: "Meetups",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Organizations",
                columns: new[] { "Id", "Created_at", "Mission", "Name" },
                values: new object[] { 1, new DateTime(2023, 10, 24, 18, 24, 29, 842, DateTimeKind.Local).AddTicks(9223), "mission 1", "OrgSeed1" });

            migrationBuilder.InsertData(
                table: "Meetups",
                columns: new[] { "Id", "DateCreated", "Description", "ImageUrl", "Location", "MeetTime", "OrganizationId", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 10, 24, 18, 24, 29, 842, DateTimeKind.Local).AddTicks(9448), "meetup 1 desc", "https://assets.hvmag.com/2023/09/pumpkin-world-AdobeStock_626050040.jpg", "location 1", new DateTime(2023, 10, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "meetup 1" },
                    { 2, new DateTime(2023, 10, 24, 18, 24, 29, 842, DateTimeKind.Local).AddTicks(9456), "meetup 2 desc", "https://assets.hvmag.com/2023/09/pumpkin-world-AdobeStock_626050040.jpg", "location 2", new DateTime(2023, 10, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "meetup 2" }
                });

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "Id", "Email", "FirstName", "ImageUrl", "LastName", "MemberSince", "OrganizationId", "Phone", "Uid" },
                values: new object[,]
                {
                    { 1, "user1@gmail.com", "Test", "https://i0.wp.com/theverybesttop10.com/wp-content/uploads/2015/03/Top-10-Dogs-With-Funny-Things-In-Their-Mouth-8-510x700.jpg?resize=600%2C824", "user 1", new DateTime(2023, 10, 24, 18, 24, 29, 842, DateTimeKind.Local).AddTicks(9476), 1, "123-456-7890", "uid1" },
                    { 2, "user2@gmail.com", "Test", "https://joyrideharness.com/cdn/shop/articles/AdobeStock_274099078.jpg?v=1620400547", "user 2", new DateTime(2023, 10, 24, 18, 24, 29, 842, DateTimeKind.Local).AddTicks(9481), 1, "123-456-7890", "uid2" },
                    { 3, "user3@gmail.com", "Test", "https://hips.hearstapps.com/hmg-prod/images/dog-puns-1581708208.jpg", "user 3", new DateTime(2023, 10, 24, 18, 24, 29, 842, DateTimeKind.Local).AddTicks(9485), 1, "123-456-7890", "uid3" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Meetups_Organizations_OrganizationId",
                table: "Meetups",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Organizations_OrganizationId",
                table: "Members",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meetups_Organizations_OrganizationId",
                table: "Meetups");

            migrationBuilder.DropForeignKey(
                name: "FK_Members_Organizations_OrganizationId",
                table: "Members");

            migrationBuilder.DeleteData(
                table: "Meetups",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Meetups",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AlterColumn<int>(
                name: "Uid",
                table: "Members",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "OrganizationId",
                table: "Members",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "OrganizationId",
                table: "Meetups",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Meetups_Organizations_OrganizationId",
                table: "Meetups",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Organizations_OrganizationId",
                table: "Members",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id");
        }
    }
}
