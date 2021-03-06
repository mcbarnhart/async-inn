﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace AsyncInn.Migrations
{
    public partial class removedHotelRoomNavPropertyFromHotel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Amenity_Room_RoomID",
                table: "Amenity");

            migrationBuilder.DropIndex(
                name: "IX_Amenity_RoomID",
                table: "Amenity");

            migrationBuilder.DropColumn(
                name: "RoomID",
                table: "Amenity");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoomID",
                table: "Amenity",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Amenity_RoomID",
                table: "Amenity",
                column: "RoomID");

            migrationBuilder.AddForeignKey(
                name: "FK_Amenity_Room_RoomID",
                table: "Amenity",
                column: "RoomID",
                principalTable: "Room",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
