using CelainyLopez_AP1_P2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace CelainyLopez_AP1_P2.DAL;

public class Context(DbContextOptions<Context> options) : DbContext(options)
{
    public DbSet<Combos> Combos { get; set; }
    public DbSet<Productos> Productos { get; set; }

    public DbSet<CombosDetalle> CombosDetalle { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Productos>().HasData(new List<Productos>()
        {
            new Productos() {ArticuloId = 1,Descripcion = "Memoria RAM", Costo = 2000, Precio = 5000, Existencia = 15},
            new Productos() {ArticuloId = 2,Descripcion = "Disco duro", Costo = 250, Precio = 500, Existencia = 25},
            new Productos() {ArticuloId = 3,Descripcion = "Teclado", Costo = 500, Precio = 750, Existencia = 50},
            new Productos() {ArticuloId = 4,Descripcion = "Tarjeta gráfica", Costo = 3000, Precio = 4000, Existencia = 50},
            new Productos() {ArticuloId = 5,Descripcion = "Procesador", Costo = 1000, Precio = 2000, Existencia = 6}
        });
    }
}
