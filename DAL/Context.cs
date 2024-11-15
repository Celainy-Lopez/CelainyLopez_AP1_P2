using CelainyLopez_AP1_P2.Models;
using Microsoft.EntityFrameworkCore;

namespace CelainyLopez_AP1_P2.DAL;

public class Context(DbContextOptions<Context> options) : DbContext(options)
{
    public DbSet<Registros> Registros { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
