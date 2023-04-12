﻿using Microsoft.EntityFrameworkCore;
using ProjetDotNet.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Windows.Media;

namespace ProjetDotNet.Database
{
    public class ApplicationContext : DbContext
    {
        private readonly string _connectionString;

        public DbSet<Investigation> Enquetes { get; set; }
        public DbSet<Investigator> Enqueteurs { get; set; }
        public DbSet<Complainant> Complainants { get; set; }
        public DbSet<Suspect> Suspects { get; set; }

        public ApplicationContext(string connectionString)
        {
            _connectionString = connectionString;
            Database.EnsureCreated();
            //Database.EnsureDeleted();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
