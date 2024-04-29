﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinhaAPI.Context;
using MinhaAPI.Models;
using MinhaAPI.Repository.interfaces;

namespace MinhaAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriasController : ControllerBase
{
    private readonly IRepository<Categoria> _repository;

    public CategoriasController(IRepository<Categoria> repository)
    {
        _repository = repository;
    }

   

    [HttpGet]
    public ActionResult<IEnumerable<Categoria>> Get()
    {
        var categorias = _repository.GetAll();

        return Ok(categorias);

    }

    [HttpGet("{id:int}", Name = "ObterCategoria")]
    public ActionResult<Categoria> get(int id)
    {


        var categoria = _repository.Get(c => c.CategoriaId == id);

        if(categoria is null)
        {
            return NotFound();
        }
        return Ok(categoria);
    }
    [HttpPost]
    public ActionResult Post(Categoria categoria)
    {
        if(categoria is null)
        {
            return BadRequest();
        }

        var categoriaCriada = _repository.Create(categoria);

        return new CreatedAtRouteResult("ObterCategoria", new {id = categoriaCriada.CategoriaId}, categoriaCriada);
    }
    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Categoria categoria)
    {
        if (id != categoria.CategoriaId)
        {
            return BadRequest();
        }       
         _repository.Update(categoria);

        return Ok(categoria);
    }
    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var categoria = _repository.Get(c => c.CategoriaId == id);
        if(categoria is null)
        {
            return NotFound("Categoria nao localizada");
        }
       
       var categoriaExcluida = _repository.Delete(categoria); 
        return Ok(categoriaExcluida);
    }
}
