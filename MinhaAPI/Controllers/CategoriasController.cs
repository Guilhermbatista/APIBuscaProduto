using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinhaAPI.Context;
using MinhaAPI.DTOs;
using MinhaAPI.DTOs.Mappings;
using MinhaAPI.Models;
using MinhaAPI.Pagination;
using MinhaAPI.Repository.interfaces;
using Newtonsoft.Json;

namespace MinhaAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriasController : ControllerBase
{
    private readonly IUnitOfWork _uof;

    public CategoriasController(IUnitOfWork uof)
    {
        _uof = uof;
    }
    [HttpGet]
    public ActionResult<IEnumerable<CategoriaDTO>> Get()
    {
        var categorias = _uof.CategoriaRepository.GetAll();
        
        if(categorias is null)
        
            return NotFound("nao existem categorias... ");
        
       var categoriasDTO = categorias.ToCategoriaDTOList();

        return Ok(categoriasDTO);

    }

    [HttpGet("pagination")]
    public ActionResult<IEnumerable<CategoriaDTO>> Get([FromQuery] CategoriaParameters categoriasParameters)
    {
        var categorias = _uof.CategoriaRepository.GetCategorias(categoriasParameters);
        return ObterCategorias(categorias);
    }

    private ActionResult<IEnumerable<CategoriaDTO>> ObterCategorias(PagedList<Categoria> categorias)
    {
        var metadata = new
        {
            categorias.TotalCount,
            categorias.PageSize,
            categorias.CurrentPage,
            categorias.TotalPage,
            categorias.HasNext,
            categorias.HasPrevius

        };

        Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));


        var categoriasDTO = categorias.ToCategoriaDTOList();

        return Ok(categoriasDTO);
    }

    [HttpGet("filter/preco/pagination")]
    public ActionResult<IEnumerable<CategoriaDTO>> GetCategoriaFiltradas([FromQuery] CategoriaFiltroNome categoriasFiltro)
    {
        var categorias = _uof.CategoriaRepository.GetCategoriasFiltroNome(categoriasFiltro);
        return ObterCategorias(categorias);
    }


    [HttpGet("{id:int}", Name = "ObterCategoria")]
    public ActionResult<CategoriaDTO> get(int id)
    {
        var categoria = _uof.CategoriaRepository.Get(c => c.CategoriaId == id);

        if (categoria is null)
        {
            return NotFound();
        }

        var categoriaDTO = categoria.ToCategoriaDTO();

        return Ok(categoriaDTO);
    }

    [HttpPost]
    public ActionResult<CategoriaDTO> Post(CategoriaDTO categoriaDTO)
    {
        if(categoriaDTO is null)
        {
            return BadRequest();
        }

        var categoria = categoriaDTO.ToCategoria();

        var categoriaCriada = _uof.CategoriaRepository.Create(categoria);
        _uof.Commit();

        var NovaCategoriaDTO = categoriaCriada.ToCategoriaDTO();

        return new CreatedAtRouteResult("ObterCategoria", new {id = NovaCategoriaDTO.CategoriaId}, NovaCategoriaDTO);
    }


    [HttpPut("{id:int}")]
    public ActionResult<CategoriaDTO> Put(int id, CategoriaDTO categoriaDTO)
    {
        if (id != categoriaDTO.CategoriaId)
        {
            return BadRequest();
        }

        var categoria = categoriaDTO.ToCategoria();

        var categoriaAtualizada = _uof.CategoriaRepository.Update(categoria);
        _uof.Commit();

        var CategoriaAtualizadaDTO = categoriaAtualizada.ToCategoriaDTO();

        return Ok(CategoriaAtualizadaDTO);
    }


    [HttpDelete("{id:int}")]
    public ActionResult<CategoriaDTO> Delete(int id)
    {
        var categoria = _uof.CategoriaRepository.Get(c => c.CategoriaId == id);
        if(categoria is null)
        {
            return NotFound("Categoria nao localizada");
        }
       
       var categoriaExcluida = _uof.CategoriaRepository.Delete(categoria);
        _uof.Commit();

        var CategoriaExcluidaDTO = categoriaExcluida.ToCategoriaDTO();

        return Ok(CategoriaExcluidaDTO);
    }
}
