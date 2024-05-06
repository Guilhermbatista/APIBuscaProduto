using MinhaAPI.Models;
using MinhaAPI.Pagination;

namespace MinhaAPI.Repository.interfaces;

public interface IProdutosRepository : IRepository<Produto>
{
    IEnumerable<Produto> GetProdutos(ProdutosParameters produtosParams);
    IEnumerable<Produto> GetProdutosPorCategoria(int id);
}
