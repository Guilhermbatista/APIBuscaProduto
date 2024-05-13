using MinhaAPI.Models;
using MinhaAPI.Pagination;

namespace MinhaAPI.Repository.interfaces;

public interface ICategoriaRepository : IRepository<Categoria>
{
    PagedList<Categoria> GetCategorias(CategoriaParameters categoriasParams);
    PagedList<Categoria> GetCategoriasFiltroNome(CategoriaFiltroNome categoriasParams);

}
