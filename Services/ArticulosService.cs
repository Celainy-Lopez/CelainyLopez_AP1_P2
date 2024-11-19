using CelainyLopez_AP1_P2.DAL;
using CelainyLopez_AP1_P2.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CelainyLopez_AP1_P2.Services;

public class ArticulosService(IDbContextFactory<Context> DbFactory)
{
    public async Task<Productos> Buscar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        return await contexto.Productos
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.ArticuloId == id);
    }

    public async Task<List<Productos>> Listar(Expression<Func<Productos, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Productos
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }
}
