using Microsoft.EntityFrameworkCore;
using MinhaAPI.Context;
using MinhaAPI.Models;
using MinhaAPI.Pagination;
using MinhaAPI.Repository.interfaces;

namespace MinhaAPI.Repository;

public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
{

    public CategoriaRepository(AppDbContext context): base(context)
    {
    }

    public PagedList<Categoria> GetCategorias(CategoriaParameters categoriasParams)
    {
        var categorias = GetAll().OrderBy(p => p.CategoriaId).AsQueryable();
        var categoriasOrdenados = PagedList<Categoria>.ToRagedList(categorias, categoriasParams.PageNumber, categoriasParams.PageSize);
        return categoriasOrdenados;
    }

    public PagedList<Categoria> GetCategoriasFiltroNome(CategoriaFiltroNome categoriasParams)
    {
        var categorias = GetAll().AsQueryable();

        if (!string.IsNullOrEmpty(categoriasParams.Nome))
        {
            categorias = categorias.Where(c=> c.Nome.Contains(categoriasParams.Nome));
        }
        var categoriaFiltradas = PagedList<Categoria>.ToRagedList(categorias, categoriasParams.PageNumber, categoriasParams.PageSize);
        return categoriaFiltradas;
    }
}
