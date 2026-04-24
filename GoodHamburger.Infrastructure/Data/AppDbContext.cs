using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace GoodHamburger.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Pedido> Pedidos => Set<Pedido>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(p => p.Id);

            entity.OwnsOne(p => p.Lanche, nav =>
            {
                nav.Property(m => m.Nome).HasColumnName("LancheNome");
                nav.Property(m => m.Preco).HasColumnName("LanchePreco");
                nav.Property(m => m.Tipo).HasColumnName("LancheTipo");
            });

            entity.OwnsOne(p => p.BatataFrita, nav =>
            {
                nav.Property(m => m.Nome).HasColumnName("BatataFritaNome");
                nav.Property(m => m.Preco).HasColumnName("BatataFritaPreco");
                nav.Property(m => m.Tipo).HasColumnName("BatataFritaTipo");
            });

            entity.OwnsOne(p => p.Refrigerante, nav =>
            {
                nav.Property(m => m.Nome).HasColumnName("RefrigeranteNome");
                nav.Property(m => m.Preco).HasColumnName("RefrigerantePreco");
                nav.Property(m => m.Tipo).HasColumnName("RefrigeranteTipo");
            });
        });
    }
}