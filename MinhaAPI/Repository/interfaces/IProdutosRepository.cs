using MinhaAPI.Models;

namespace MinhaAPI.Repository.interfaces;

public interface IProdutosRepository : IRepository<Produto>
{
    IEnumerable<Produto> GetProdutosPorCategoria(int id);
}
