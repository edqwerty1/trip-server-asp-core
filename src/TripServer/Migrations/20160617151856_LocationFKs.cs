using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TripServer.Migrations
{
    public partial class LocationFKs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Locations_LocationId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Locations_LocationId1",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_LocationId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_LocationId1",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LocationId1",
                table: "Users");

            migrationBuilder.AddColumn<Guid>(
                name: "DownVoteLocationId",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpVoteLocationId",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_DownVoteLocationId",
                table: "Users",
                column: "DownVoteLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UpVoteLocationId",
                table: "Users",
                column: "UpVoteLocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Locations_DownVoteLocationId",
                table: "Users",
                column: "DownVoteLocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Locations_UpVoteLocationId",
                table: "Users",
                column: "UpVoteLocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Locations_DownVoteLocationId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Locations_UpVoteLocationId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_DownVoteLocationId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_UpVoteLocationId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DownVoteLocationId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UpVoteLocationId",
                table: "Users");

            migrationBuilder.AddColumn<Guid>(
                name: "LocationId",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LocationId1",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_LocationId",
                table: "Users",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_LocationId1",
                table: "Users",
                column: "LocationId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Locations_LocationId",
                table: "Users",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Locations_LocationId1",
                table: "Users",
                column: "LocationId1",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
