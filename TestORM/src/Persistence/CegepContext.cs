using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestORMCodeFirst.Entities;

namespace TestORMCodeFirst.Persistence
{
    public class CegepContext : DbContext
    {
        public CegepContext(DbContextOptions<CegepContext> options) : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public virtual DbSet<Etudiant> Etudiants { get; set; }  
        public virtual DbSet<InscriptionCours> InscCours { get; set; }

        public virtual DbSet<Cours> Cours { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)  //Fluent API configuration has the highest precedence and will override conventions and data annotations. This is the most powerful method of configuration
        {

            //Etudiant
            builder.Entity<Etudiant>()
                        .HasIndex(e => e.EtudiantTuteurID)
                        .IsUnique();
            
            builder.Entity<Etudiant>() //Nécessaire pour implémenter la FK EtudiantTuteurID sur la table ETUDIANT sans le cascade delete par défaut
                        .HasOne(etud => etud.Tuteur)
                        .WithOne()           
                        .OnDelete(DeleteBehavior.Restrict);

            //InscriptionCours
            builder.Entity<InscriptionCours>()
                        .HasKey(inscription => new { inscription.EtudiantID, inscription.CodeCours, inscription.CodeSession });

            builder.Entity<InscriptionCours>()
                        .HasOne(inscription => inscription.Etudiant)
                        .WithMany(etud => etud.Cours)
                        .OnDelete(DeleteBehavior.Restrict);

            //Cours
            //Inscrire les dépendances
            builder.Entity<Cours>()
                        .HasKey(cours => new { cours.CodeCours });

            builder.Entity<Cours>()
                        .HasMany(cours => cours.Inscriptions)
                        .WithOne().
                        OnDelete(DeleteBehavior.Restrict);
       }

    }
}
