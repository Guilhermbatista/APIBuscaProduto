using Microsoft.EntityFrameworkCore;
using MinhaAPI.Context;
using MinhaAPI.Models;
using MinhaAPI.Repository.interfaces;

namespace MinhaAPI.Repository;

public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
{

    public CategoriaRepository(AppDbContext context): base(context)
    {
    }
   
}
