using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinhaAPI.Context;
using MinhaAPI.DTOs;
using MinhaAPI.Models;
using MinhaAPI.Pagination;
using MinhaAPI.Repository.interfaces;
using Newtonsoft.Json;

namespace MinhaAPI.Controllers;

[Route("[controller]")]// /produto
[ApiController]
public class ProdutosController : ControllerBase
{

    private readonly IUnitOfWork _uof;
    private readonly IMapper _mapper;

    public ProdutosController(IUnitOfWork uof, IProdutosRepository produtosRepository, IMapper mapper)
    {
        _uof = uof;
        _mapper = mapper;
    }
    [HttpGet("produtos/{id}")]
   public ActionResult <IEnumerable<ProdutoDTO>> GetProdutosCategoria(int id)
    {
        var produto = _uof.ProdutoRepository.GetProdutosPorCategoria(id);

        if(produto is null)
            return NotFound();

        //var destino = _mappaer.Map<destino>(origem)
        var produtosDTO = _mapper.Map<IEnumerable<ProdutoDTO>>(produto);

        return Ok(produtosDTO);
    }



    [HttpGet("pagination")]
    public ActionResult<IEnumerable<ProdutoDTO>>Get([FromQuery] ProdutosParameters produtosParameters)
    {
        var produtos = _uof.ProdutoRepository.GetProdutos(produtosParameters);

        return ObterProdutos(produtos);
    }

    private ActionResult<IEnumerable<ProdutoDTO>> ObterProdutos(PagedList<Produto> produtos)
    {
        var metadata = new
        {
            produtos.TotalCount,
            produtos.PageSize,
            produtos.CurrentPage,
            produtos.TotalPage,
            produtos.HasNext,
            produtos.HasPrevius

        };

        Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));


        var produtosDTO = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);

        return Ok(produtosDTO);
    }

    [HttpGet("filter/preco/pagination")]
    public ActionResult<IEnumerable<ProdutoDTO>> GetProdutosFilterPreco([FromQuery] ProdutosFiltroPreco produtosFilterParameters)
    {
        var produtos = _uof.ProdutoRepository.GetProdutosFiltroPreco(produtosFilterParameters);

        return ObterProdutos(produtos);
    }



    [HttpGet]
    public ActionResult<IEnumerable<ProdutoDTO>> Get()
    {
        var produto = _uof.ProdutoRepository.GetAll().ToList();
        if(produto is null)
        {
            return NotFound();
        }
        var produtosDTO = _mapper.Map<IEnumerable<ProdutoDTO>>(produto);

        return Ok(produtosDTO);

    }


    [HttpGet("{id:int:min(1)}", Name="ObterProduto")] // /produtos/id
    public  ActionResult<ProdutoDTO> Get(int id)
    {
        var produto = _uof.ProdutoRepository.Get(c => c.ProdutoId == id);

        if (produto is null)
        {
            return NotFound("Produto não encontrado...");
        }
        var produtoDTO = _mapper.Map<ProdutoDTO>(produto);

        return Ok(produtoDTO);

    }


    [HttpPost]  
    public ActionResult<ProdutoDTO> Post (ProdutoDTO produtoDTO)
    {
        if (produtoDTO is null)
        {
            return BadRequest();
        }
        var produto = _mapper.Map<Produto>(produtoDTO);

        var NewProduto = _uof.ProdutoRepository.Create(produto);
        _uof.Commit();

        var novoProdutoDTO = _mapper.Map<ProdutoDTO>(NewProduto);


        return new CreatedAtRouteResult("ObterProduto", new {id = novoProdutoDTO.ProdutoId }, novoProdutoDTO);
    }

    [HttpPatch("{id}/UpdatePartial")]
    public ActionResult<ProdutoDTOUpdateResponse> Patch(int id, JsonPatchDocument<ProdutoDTOUpdateRequest> patchProdutoDTO) 
    { 
        if(patchProdutoDTO is null || id <= 0)
            return BadRequest();

        var produto = _uof.ProdutoRepository.Get(c => c.ProdutoId == id);

        if (produto is null)
            return NotFound();

        var produtoUpdateRequest  = _mapper.Map<ProdutoDTOUpdateRequest>(produto);

        patchProdutoDTO.ApplyTo(produtoUpdateRequest, ModelState);

        if (!ModelState.IsValid || TryValidateModel(produtoUpdateRequest))
            return BadRequest(ModelState);

        _mapper.Map(produtoUpdateRequest, produto);

        _uof.ProdutoRepository.Update(produto);
        _uof.Commit();

        return Ok(_mapper.Map<ProdutoDTOUpdateResponse>(produto));

    }


    [HttpPut("{id:int}")]// /produtos/id
    public ActionResult<ProdutoDTO> Put(int id, ProdutoDTO produtoDTO) 
    {
        if( id != produtoDTO.ProdutoId )
            return BadRequest();

        var produto = _mapper.Map<Produto>(produtoDTO);

        var produtoAtualizado = _uof.ProdutoRepository.Update(produto);
        _uof.Commit();

        var produtoAtualizadoDTO = _mapper.Map<ProdutoDTO>(produtoAtualizado);

        return Ok(produtoAtualizadoDTO);
        
    }


    [HttpDelete("{id:int}")]// /produtos/id
    public ActionResult<ProdutoDTO> Delete(int id)
    {
       var produto = _uof.ProdutoRepository.Get(c =>c.ProdutoId == id);
        if (produto is null)
            return NotFound();

        var produtoDeletado = _uof.ProdutoRepository.Delete(produto);
        _uof.Commit();

        var produtoDeletadoDTO = _mapper.Map<ProdutoDTO>(produtoDeletado);

        return Ok(produtoDeletadoDTO);


    }
}
