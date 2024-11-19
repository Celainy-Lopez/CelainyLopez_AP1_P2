using CelainyLopez_AP1_P2.DAL;
using Microsoft.EntityFrameworkCore;
using CelainyLopez_AP1_P2.Models;
using System.Linq.Expressions;

namespace CelainyLopez_AP1_P2.Services;

public class CombosService(IDbContextFactory<Context> DbFactory)
{
    public async Task<bool> Existe(int id)
    {
        await using var contexto = await DbFactory
            .CreateDbContextAsync();
        return await contexto.Combos
            .AnyAsync(a => a.ComboId == id);
    }

    private async Task<bool> Insertar(Combos combos)
    {
        await using var contexto = await DbFactory
            .CreateDbContextAsync();
        await AfectarArticulo(combos.CombosDetalles.ToArray(), true);
        contexto.Combos
            .Add(combos);
        return await contexto
            .SaveChangesAsync() > 0;
    }
    private async Task AfectarArticulo(CombosDetalle[] detalle, bool resta = true)
    {
        await using var contexto = await DbFactory
            .CreateDbContextAsync();
        foreach (var item in detalle)
        {
            var articulo = await contexto.Productos.SingleAsync(p => p.ArticuloId == item.ArticuloId);
            if (resta)
            {
                articulo.Existencia -= item.Cantidad;
            }
            else
            {
                articulo.Existencia += item.Cantidad;
            }
            await contexto.SaveChangesAsync();
        }
    }

    private async Task<bool> Modificar(Combos combos)
    {
        await using var contexto = await DbFactory
            .CreateDbContextAsync();

        var comboOriginal = await contexto.Combos
            .Include(t => t.CombosDetalles)
            .FirstOrDefaultAsync(t => t.ComboId == combos.ComboId);

        if (comboOriginal == null)
            return false;

        await AfectarArticulo(comboOriginal.CombosDetalles.ToArray(), false);

        foreach (var detalleOriginal in comboOriginal.CombosDetalles)
        {
            if (!combos.CombosDetalles.Any(d => d.DetalleId == detalleOriginal.DetalleId))
            {
                contexto.CombosDetalle.Remove(detalleOriginal);
            }
        }

        await AfectarArticulo(comboOriginal.CombosDetalles.ToArray(), true);
        contexto.Entry(comboOriginal).CurrentValues.SetValues(combos);

        foreach (var detalle in combos.CombosDetalles)
        {
            var detalleExistente = comboOriginal.CombosDetalles
                .FirstOrDefault(d => d.DetalleId == detalle.DetalleId);

            if (detalleExistente != null)
            {
                contexto.Entry(detalleExistente).CurrentValues.SetValues(detalle);
            }
            else
            {
                comboOriginal.CombosDetalles.Add(detalle);
            }
        }

        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await DbFactory
            .CreateDbContextAsync();
        var combo = await contexto.Combos
            .Include(t => t.CombosDetalles)
            .ThenInclude(td => td.Articulos)
            .FirstOrDefaultAsync(t => t.ComboId == id);

        if (combo == null)
            return false;


        await AfectarArticulo(combo.CombosDetalles.ToArray(), resta: false);
        contexto.CombosDetalle.RemoveRange(combo.CombosDetalles);
        contexto.Combos.Remove(combo);

        var cantidad = await contexto.SaveChangesAsync();
        return cantidad > 0;
    }

    public async Task<List<Combos>> Listar(Expression<Func<Combos, bool>> criterio)
    {
        await using var contexto = await DbFactory
            .CreateDbContextAsync();
        return await contexto.Combos
            .Include(t => t.CombosDetalles)
            .ThenInclude(t => t.Articulos)
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();

    }

    public async Task<Combos> Buscar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Combos
            .Include(t => t.CombosDetalles)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.ComboId == id);
    }

    public async Task<Combos> BuscarDetalles(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Combos
            .Include(t => t.CombosDetalles)
            .ThenInclude(td => td.Articulos)
            .FirstOrDefaultAsync(p => p.ComboId == id);
    }

    public async Task<bool> Guardar(Combos combos)
    {
        if (!await Existe(combos.ComboId))
            return await Insertar(combos);
        else
            return await Modificar(combos);
    }
}