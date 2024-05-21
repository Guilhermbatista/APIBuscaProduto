using MinhaAPI.Models;
using MinhaAPI.Pagination;
using X.PagedList;

namespace MinhaAPI.Repository.interfaces;

public interface ICategoriaRepository : IRepository<Categoria>
{
    Task<IPagedList<Categoria>> GetCategoriasAsync(CategoriaParameters categoriasParams);
    Task<IPagedList<Categoria>> GetCategoriasFiltroNomeAsync(CategoriaFiltroNome categoriasParams);

}
