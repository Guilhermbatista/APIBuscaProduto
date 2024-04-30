using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinhaAPI.Context;
using MinhaAPI.Models;
using MinhaAPI.Repository.interfaces;

namespace MinhaAPI.Controllers;

[Route("[controller]")]// /produto
[ApiController]
public class ProdutosController : ControllerBase
{

    private readonly IUnitOfWork _uof;
    private readonly IProdutosRepository _produtosRepository;

    public ProdutosController(IUnitOfWork uof, IProdutosRepository produtosRepository)
    {
        _uof = uof;
        _produtosRepository = produtosRepository;
    }
    [HttpGet("produtos/{id}")]
   public ActionResult <IEnumerable<Produto>> GetProdutosCategoria(int id)
    {
        var produto = _produtosRepository.GetProdutosPorCategoria(id);

        if(produto is null)
            return NotFound();

        return Ok(produto);
    }

    [HttpGet]
    public ActionResult<IEnumerable<Produto>> Get()
    {
        var produto = _uof.ProdutoRepository.GetAll().ToList();
        if(produto is null)
        {
            return NotFound();
        }
        
        return Ok(produto);

    }
    [HttpGet("{id:int:min(1)}", Name="ObterProduto")] // /produtos/id
    public  ActionResult<Produto> Get(int id)
    {
        var produto = _uof.ProdutoRepository.Get(c => c.ProdutoId == id);

        if (produto is null)
        {
            return NotFound("Produto não encontrado...");
        }

        return Ok(produto);

    }
    [HttpPost]  
    public ActionResult Post (Produto produto)
    {
        if (produto is null)
        {
            return BadRequest();
        }

        var NewProduto = _uof.ProdutoRepository.Create(produto);
        _uof.Commit();
        return new CreatedAtRouteResult("ObterProduto", new {id = NewProduto.ProdutoId }, NewProduto);
    }

    [HttpPut("{id:int}")]// /produtos/id
    public ActionResult Put(int id, Produto produto) 
    {
        if( id != produto.ProdutoId )
        {
            return BadRequest();
        }

        var produtoAtualizado = _uof.ProdutoRepository.Update(produto);
        _uof.Commit();
        return Ok(produtoAtualizado);
        
    }
    [HttpDelete("{id:int}")]// /produtos/id
    public ActionResult Delete(int id)
    {
       var produto = _uof.ProdutoRepository.Get(c =>c.ProdutoId == id);
        if (produto is null)
            return NotFound();

       
        return Ok(_uof.ProdutoRepository.Delete(produto));
        _uof.Commit();

    }
}
