using ExpressVoitures.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExpressVoitures.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
    {
        public DbSet<Voiture> Voitures { get; set; }
        public DbSet<Marque> Marques { get; set; }
        public DbSet<Modele> Modeles { get; set; }
        public DbSet<Finition> Finitions { get; set; }
        public DbSet<Reparation> Reparations { get; set; }
        public DbSet<Vente> Ventes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relation Voiture-Vente (1-0..1)
            modelBuilder.Entity<Voiture>()
                .HasOne(v => v.Vente)
                .WithOne(ve => ve.Voiture)
                .HasForeignKey<Vente>(ve => ve.IdVoiture);

            // Désactiver les cascades sur toutes les relations 1-N
            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
