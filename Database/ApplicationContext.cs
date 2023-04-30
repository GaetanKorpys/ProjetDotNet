using Microsoft.EntityFrameworkCore;
using ProjetDotNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Windows.Media;

namespace ProjetDotNet.Database
{
    public class ApplicationContext : DbContext
    {
        private readonly string _connectionString;

        public DbSet<Investigation> Investigations { get; set; }
        public DbSet<Investigator> Investigators { get; set; }
        public DbSet<Complainant> Complainants { get; set; }
        public DbSet<Suspect> Suspects { get; set; }
        public DbSet<Visit> Visits { get; set; }
        public DbSet<ProofPicture> ProofPictures { get; set; }

        public ApplicationContext()
        {
            _connectionString = Configuration.Configuration.connectionString;
            Database.EnsureCreated();
            //Database.EnsureDeleted();
            this.Database.SetCommandTimeout(300); // Définir le délai d'attente à 300 secondes (5 minutes)

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*
            modelBuilder.Entity<Investigation>(e => {
                //e.HasOne(x => x.Suspect).WithOne(x => x.Investigation).HasForeignKey<Investigation>(x => x.SuspectId).IsRequired(false);
                //e.HasOne(x => x.Complainant).WithOne(x => x.Investigation).HasForeignKey<Investigation>(x => x.ComplainantId).IsRequired(false);
            });
            */
        }
    }
}
