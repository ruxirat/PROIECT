using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ratiu_Ruxandra_Proiect.Migrations
{
    /// <inheritdoc />
    public partial class LinkServiceToSpecialization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SpecializationId",
                table: "Service",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Service_SpecializationId",
                table: "Service",
                column: "SpecializationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Service_Specialization_SpecializationId",
                table: "Service",
                column: "SpecializationId",
                principalTable: "Specialization",
                principalColumn: "SpecializationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Service_Specialization_SpecializationId",
                table: "Service");

            migrationBuilder.DropIndex(
                name: "IX_Service_SpecializationId",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "SpecializationId",
                table: "Service");
        }
    }
}
