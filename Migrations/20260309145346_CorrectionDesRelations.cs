using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpressVoitures.Migrations
{
    /// <inheritdoc />
    public partial class CorrectionDesRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Finitions_Modeles_ModeleId",
                table: "Finitions");

            migrationBuilder.DropForeignKey(
                name: "FK_Modeles_Marques_IdMarque",
                table: "Modeles");

            migrationBuilder.DropForeignKey(
                name: "FK_Reparations_Voitures_VoitureId",
                table: "Reparations");

            migrationBuilder.DropForeignKey(
                name: "FK_Ventes_Voitures_IdVoiture",
                table: "Ventes");

            migrationBuilder.DropForeignKey(
                name: "FK_Voitures_Finitions_FinitionId",
                table: "Voitures");

            migrationBuilder.DropForeignKey(
                name: "FK_Voitures_Marques_MarqueId",
                table: "Voitures");

            migrationBuilder.DropIndex(
                name: "IX_Voitures_FinitionId",
                table: "Voitures");

            migrationBuilder.DropIndex(
                name: "IX_Voitures_MarqueId",
                table: "Voitures");

            migrationBuilder.DropIndex(
                name: "IX_Reparations_VoitureId",
                table: "Reparations");

            migrationBuilder.DropIndex(
                name: "IX_Finitions_ModeleId",
                table: "Finitions");

            migrationBuilder.DropColumn(
                name: "FinitionId",
                table: "Voitures");

            migrationBuilder.DropColumn(
                name: "MarqueId",
                table: "Voitures");

            migrationBuilder.DropColumn(
                name: "VoitureId",
                table: "Reparations");

            migrationBuilder.DropColumn(
                name: "ModeleId",
                table: "Finitions");

            migrationBuilder.CreateIndex(
                name: "IX_Voitures_IdFinition",
                table: "Voitures",
                column: "IdFinition");

            migrationBuilder.CreateIndex(
                name: "IX_Voitures_IdMarque",
                table: "Voitures",
                column: "IdMarque");

            migrationBuilder.CreateIndex(
                name: "IX_Reparations_IdVoiture",
                table: "Reparations",
                column: "IdVoiture");

            migrationBuilder.CreateIndex(
                name: "IX_Finitions_IdModele",
                table: "Finitions",
                column: "IdModele");

            migrationBuilder.AddForeignKey(
                name: "FK_Finitions_Modeles_IdModele",
                table: "Finitions",
                column: "IdModele",
                principalTable: "Modeles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Modeles_Marques_IdMarque",
                table: "Modeles",
                column: "IdMarque",
                principalTable: "Marques",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reparations_Voitures_IdVoiture",
                table: "Reparations",
                column: "IdVoiture",
                principalTable: "Voitures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ventes_Voitures_IdVoiture",
                table: "Ventes",
                column: "IdVoiture",
                principalTable: "Voitures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Voitures_Finitions_IdFinition",
                table: "Voitures",
                column: "IdFinition",
                principalTable: "Finitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Voitures_Marques_IdMarque",
                table: "Voitures",
                column: "IdMarque",
                principalTable: "Marques",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Finitions_Modeles_IdModele",
                table: "Finitions");

            migrationBuilder.DropForeignKey(
                name: "FK_Modeles_Marques_IdMarque",
                table: "Modeles");

            migrationBuilder.DropForeignKey(
                name: "FK_Reparations_Voitures_IdVoiture",
                table: "Reparations");

            migrationBuilder.DropForeignKey(
                name: "FK_Ventes_Voitures_IdVoiture",
                table: "Ventes");

            migrationBuilder.DropForeignKey(
                name: "FK_Voitures_Finitions_IdFinition",
                table: "Voitures");

            migrationBuilder.DropForeignKey(
                name: "FK_Voitures_Marques_IdMarque",
                table: "Voitures");

            migrationBuilder.DropIndex(
                name: "IX_Voitures_IdFinition",
                table: "Voitures");

            migrationBuilder.DropIndex(
                name: "IX_Voitures_IdMarque",
                table: "Voitures");

            migrationBuilder.DropIndex(
                name: "IX_Reparations_IdVoiture",
                table: "Reparations");

            migrationBuilder.DropIndex(
                name: "IX_Finitions_IdModele",
                table: "Finitions");

            migrationBuilder.AddColumn<int>(
                name: "FinitionId",
                table: "Voitures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MarqueId",
                table: "Voitures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VoitureId",
                table: "Reparations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ModeleId",
                table: "Finitions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Voitures_FinitionId",
                table: "Voitures",
                column: "FinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_Voitures_MarqueId",
                table: "Voitures",
                column: "MarqueId");

            migrationBuilder.CreateIndex(
                name: "IX_Reparations_VoitureId",
                table: "Reparations",
                column: "VoitureId");

            migrationBuilder.CreateIndex(
                name: "IX_Finitions_ModeleId",
                table: "Finitions",
                column: "ModeleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Finitions_Modeles_ModeleId",
                table: "Finitions",
                column: "ModeleId",
                principalTable: "Modeles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Modeles_Marques_IdMarque",
                table: "Modeles",
                column: "IdMarque",
                principalTable: "Marques",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reparations_Voitures_VoitureId",
                table: "Reparations",
                column: "VoitureId",
                principalTable: "Voitures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ventes_Voitures_IdVoiture",
                table: "Ventes",
                column: "IdVoiture",
                principalTable: "Voitures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Voitures_Finitions_FinitionId",
                table: "Voitures",
                column: "FinitionId",
                principalTable: "Finitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Voitures_Marques_MarqueId",
                table: "Voitures",
                column: "MarqueId",
                principalTable: "Marques",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
