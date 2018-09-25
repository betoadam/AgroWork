using AgroWork.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgroWork.Dados
{
    public class AgroContext: DbContext
    {
        public AgroContext(DbContextOptions<AgroContext> options) : base(options)
        {
        }

        public DbSet<Inseminador> Inseminadors { get; set; }
        public DbSet<Produtor> Produtors { get; set; }
        public DbSet<Vaca> Vacas { get; set; }
        public DbSet<Inseminacao> Inseminacaos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Inseminador>().ToTable("Inseminador");
            modelBuilder.Entity<Produtor>().ToTable("Produtor");
            modelBuilder.Entity<Vaca>().ToTable("Vaca");
            modelBuilder.Entity<Inseminacao>().ToTable("Inseminacao");
        }
    }
}
