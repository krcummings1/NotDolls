﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NotDolls.Migrations
{
    public partial class AddedCollections : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_InventoryImage_InventoryId",
                table: "InventoryImage",
                column: "InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_GeekId",
                table: "Inventory",
                column: "GeekId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_Geek_GeekId",
                table: "Inventory",
                column: "GeekId",
                principalTable: "Geek",
                principalColumn: "GeekId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryImage_Inventory_InventoryId",
                table: "InventoryImage",
                column: "InventoryId",
                principalTable: "Inventory",
                principalColumn: "InventoryId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_Geek_GeekId",
                table: "Inventory");

            migrationBuilder.DropForeignKey(
                name: "FK_InventoryImage_Inventory_InventoryId",
                table: "InventoryImage");

            migrationBuilder.DropIndex(
                name: "IX_InventoryImage_InventoryId",
                table: "InventoryImage");

            migrationBuilder.DropIndex(
                name: "IX_Inventory_GeekId",
                table: "Inventory");
        }
    }
}
