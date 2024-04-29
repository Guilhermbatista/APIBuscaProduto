using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MinhaAPI.Context;
using MinhaAPI.Models;
using MinhaAPI.Repository.interfaces;

namespace MinhaAPI.Repository;

public class ProdutoRepository : Repository<Produto>, IProdutosRepository
{ 
   

    public ProdutoRepository(AppDbContext context): base(context)
    {
    }

    public IEnumerable<Produto> GetProdutosPorCategoria(int id)
    {
        return GetAll().Where(c=> c.CategoriaId == id);
    }
}
