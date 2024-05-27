using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using MinhaAPI.Context;
using MinhaAPI.DTOs;
using MinhaAPI.DTOs.Mappings;
using MinhaAPI.Models;
using MinhaAPI.Pagination;
using MinhaAPI.Repository.interfaces;
using Newtonsoft.Json;
using X.PagedList;

namespace MinhaAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[EnableRateLimiting("fixedwindow")]
public class CategoriasController : ControllerBase
{
    private readonly IUnitOfWork _uof;

    public CategoriasController(IUnitOfWork uof)
    {
        _uof = uof;
    }
    //[Authorize]
    [HttpGet]
    [DisableRateLimiting]
    public async Task<ActionResult<IEnumerable<CategoriaDTO>>> Get()
    {
        var categorias = await _uof.CategoriaRepository.GetAllAsync();
        
        if(categorias is null)
        
            return NotFound("nao existem categorias... ");
        
       var categoriasDTO = categorias.ToCategoriaDTOList();

        return Ok(categoriasDTO);

    }

    [HttpGet("pagination")]
    public async Task<ActionResult<IEnumerable<CategoriaDTO>>> Get([FromQuery] CategoriaParameters categoriasParameters)
    {
        var categorias = await _uof.CategoriaRepository.GetCategoriasAsync(categoriasParameters);
        return ObterCategorias(categorias);
    }

    private ActionResult<IEnumerable<CategoriaDTO>> ObterCategorias(IPagedList<Categoria> categorias)
    {
        var metadata = new
        {
            categorias.Count,
            categorias.PageSize,
            categorias.PageCount,
            categorias.TotalItemCount,
            categorias.HasNextPage,
            categorias.HasPreviousPage

        };

        Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));


        var categoriasDTO = categorias.ToCategoriaDTOList();

        return Ok(categoriasDTO);
    }


    [HttpGet("filter/preco/pagination")]
    public async Task<ActionResult<IEnumerable<CategoriaDTO>>> GetCategoriaFiltradas([FromQuery] CategoriaFiltroNome categoriasFiltro)
    {
        var categorias = await _uof.CategoriaRepository.GetCategoriasFiltroNomeAsync(categoriasFiltro);
        return ObterCategorias(categorias);
    }


    [HttpGet("{id:int}", Name = "ObterCategoria")]
    public async Task<ActionResult<CategoriaDTO>> get(int id)
    {
        var categoria = await _uof.CategoriaRepository.GetAsync(c => c.CategoriaId == id);

        if (categoria is null)
        {
            return NotFound();
        }

        var categoriaDTO = categoria.ToCategoriaDTO();

        return Ok(categoriaDTO);
    }

    [HttpPost]
    public async Task<ActionResult<CategoriaDTO>> Post(CategoriaDTO categoriaDTO)
    {
        if(categoriaDTO is null)
        {
            return BadRequest();
        }

        var categoria = categoriaDTO.ToCategoria();

        var categoriaCriada = _uof.CategoriaRepository.Create(categoria);

        await _uof.CommitAsync();

        var NovaCategoriaDTO = categoriaCriada.ToCategoriaDTO();

        return new CreatedAtRouteResult("ObterCategoria", new {id = NovaCategoriaDTO.CategoriaId}, NovaCategoriaDTO);
    }


    [HttpPut("{id:int}")]
    public async Task<ActionResult<CategoriaDTO>> Put(int id, CategoriaDTO categoriaDTO)
    {
        if (id != categoriaDTO.CategoriaId)
        {
            return BadRequest();
        }

        var categoria = categoriaDTO.ToCategoria();

        var categoriaAtualizada = _uof.CategoriaRepository.Update(categoria);
        await _uof.CommitAsync();

        var CategoriaAtualizadaDTO = categoriaAtualizada.ToCategoriaDTO();

        return Ok(CategoriaAtualizadaDTO);
    }


    [HttpDelete("{id:int}")]
    [Authorize(Policy ="AdminOnly")]
    public async  Task<ActionResult<CategoriaDTO>> Delete(int id)
    {
        var categoria = await _uof.CategoriaRepository.GetAsync(c => c.CategoriaId == id);
        if(categoria is null)
        {
            return NotFound("Categoria nao localizada");
        }
       
       var categoriaExcluida = _uof.CategoriaRepository.Delete(categoria);
        await _uof.CommitAsync();

        var CategoriaExcluidaDTO = categoriaExcluida.ToCategoriaDTO();

        return Ok(CategoriaExcluidaDTO);
    }
}
