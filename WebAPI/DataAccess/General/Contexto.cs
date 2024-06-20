using Domain.Modelos;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options) { }
        public DbSet<Encargado> Encargados { get; set; }
        public DbSet<Articulo> Articulos { get; set; }
        public DbSet<StockArticulo> StockArticulos { get; set; }
        public DbSet<TipoMovimiento> TiposMovimiento { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Encargado>().HasKey(u => u.EncargadoId);
            modelBuilder.Entity<Encargado>().HasIndex(u => u.Email).IsUnique();

            modelBuilder.Entity<Articulo>().HasKey(a => a.ArticuloId);
            modelBuilder.Entity<Articulo>().HasIndex(a => a.Codigo).IsUnique();
            modelBuilder.Entity<Articulo>().HasIndex(a => a.Nombre).IsUnique();

            modelBuilder.Entity<StockArticulo>().HasKey(s => s.MovimientoId);
            modelBuilder.Entity<StockArticulo>()
                .HasOne(s => s.Articulo)
                .WithMany()
                .HasForeignKey(s => s.ArticuloId);
            modelBuilder.Entity<StockArticulo>()
                .HasOne(s => s.TipoMovimiento)
                .WithMany()
                .HasForeignKey(s => s.TipoMovimientoId);

            modelBuilder.Entity<TipoMovimiento>()
            .HasKey(tm => tm.TipoMovimientoId);

            modelBuilder.Entity<TipoMovimiento>()
                .HasIndex(tm => tm.Nombre)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}