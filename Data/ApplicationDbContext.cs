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

            // Relation Marque-Modele (1-N)
            modelBuilder.Entity<Marque>()
                .HasMany(m => m.Modeles)
                .WithOne(mo => mo.Marque)
                .HasForeignKey(mo => mo.IdMarque)
                .OnDelete(DeleteBehavior.Restrict);

            // Relation Modele-Voiture (1-N)
            modelBuilder.Entity<Modele>()
                .HasMany(mo => mo.Voitures)
                .WithOne(v => v.Modele)
                .HasForeignKey(v => v.IdModele)
                .OnDelete(DeleteBehavior.Restrict);

            // Relation Voiture-Vente (1-0..1)
            modelBuilder.Entity<Voiture>()
                .HasOne(v => v.Vente)
                .WithOne(ve => ve.Voiture)
                .HasForeignKey<Vente>(ve => ve.IdVoiture)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
