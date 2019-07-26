using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Practica01.models
{
    class SqliteContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data source=Db/CensoAgua.db");
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Todo: codigo para representar la tabla de claves de localizacion
            modelBuilder.Entity<Clavedelocalizacion>(clave => {
                clave.Property(c => c.Id).HasColumnName("id");
                clave.Property(c => c.Subsistema).HasColumnName("subsistema");
                clave.Property(c => c.Sector).HasColumnName("sector");
                clave.Property(c => c.Manzana).HasColumnName("manzana");
                clave.Property(c => c.Lote).HasColumnName("lote");
                clave.Property(c => c.Fraccion).HasColumnName("fraccion");
                clave.Property(c => c.Toma).HasColumnName("toma");
                clave.Property(c => c.Original).HasColumnName("original");
                clave.Property(c => c.Correcta).HasColumnName("Correcta");
                clave.ToTable("claves_de_localizacion");
            });
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Clavedelocalizacion> Clavedelocalizacions{ get; set; }

    }
}
