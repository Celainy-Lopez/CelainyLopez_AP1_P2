using CelainyLopez_AP1_P2.DAL;
using CelainyLopez_AP1_P2.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CelainyLopez_AP1_P2.Services;

public class RegistrosService(IDbContextFactory<Context> DbFactory)
{
    private async Task /*<bool>*/ Existe(int id)
    {
        await using var contexto = await DbFactory
            .CreateDbContextAsync();
    }

    private async Task/*<bool>*/ Insertar(Registros registros)
    {
        await using var contexto = await DbFactory
            .CreateDbContextAsync();
    }

    private async Task/*<bool>*/ Modificar(Registros registros)
    {
        await using var contexto = await DbFactory
            .CreateDbContextAsync();

    }

    public async Task/*<bool>*/ Eliminar(int id)
    {
        await using var contexto = await DbFactory
            .CreateDbContextAsync();
    }

    public async Task <List<Registros>> Listar(Expression<Func<Registros, bool>> criterio)
    {
        await using var contexto = await DbFactory
            .CreateDbContextAsync();
        return null;

    }

    public async Task/*<Registros>*/ Buscar(int id)
    {
        await using var contexto = await DbFactory
            .CreateDbContextAsync();
    }

    public async Task/*<Registros>*/ BuscarDetalles(int id)
    {
        await using var contexto = await DbFactory
            .CreateDbContextAsync();
    }

    public async Task/*<bool>*/ Guardar(Registros registros)
    {
        await using var contexto = await DbFactory
            .CreateDbContextAsync();
    }
    /*
    private async Task AfectarPrestamos(CobrosDetalle[] detalle, TipoOperacion tipoOperacion)
    {
        await using var contexto = await DbFactory
            .CreateDbContextAsync();
    }
    */
}
