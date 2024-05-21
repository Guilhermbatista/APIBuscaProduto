using Microsoft.EntityFrameworkCore;
using MinhaAPI.Context;
using MinhaAPI.Models;
using MinhaAPI.Pagination;
using MinhaAPI.Repository.interfaces;
using X.PagedList;

namespace MinhaAPI.Repository;

public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
{

    public CategoriaRepository(AppDbContext context): base(context)
    {
    }

    public async Task<IPagedList<Categoria>> GetCategoriasAsync(CategoriaParameters categoriasParams)
    {
        var categorias = await GetAllAsync();

        var categoria = categorias.OrderBy(p => p.CategoriaId).AsQueryable();

        //var categoriasOrdenados = PagedList<Categoria>.ToRagedList(categoria, categoriasParams.PageNumber, categoriasParams.PageSize);
        var categoriasOrdenados = await categoria.ToPagedListAsync(categoriasParams.PageNumber, categoriasParams.PageSize);
        return categoriasOrdenados;
    }

    public async Task<IPagedList<Categoria>> GetCategoriasFiltroNomeAsync(CategoriaFiltroNome categoriasParams)
    {
        var categorias = await GetAllAsync();

        if (!string.IsNullOrEmpty(categoriasParams.Nome))
        {
            categorias = categorias.Where(c=> c.Nome.Contains(categoriasParams.Nome));
        }
        //var categoriaFiltradas = IPagedList<Categoria>.ToRagedList(categorias.AsQueryable(), categoriasParams.PageNumber, categoriasParams.PageSize);
        var categoriaFiltradas = await categorias.ToPagedListAsync(categoriasParams.PageNumber, categoriasParams.PageSize);
        return categoriaFiltradas;
    }
}
