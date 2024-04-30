namespace MinhaAPI.Repository.interfaces;

public interface IUnitOfWork 
{
    IProdutosRepository ProdutoRepository { get;}
    ICategoriaRepository CategoriaRepository { get;}
    void Commit();
}
