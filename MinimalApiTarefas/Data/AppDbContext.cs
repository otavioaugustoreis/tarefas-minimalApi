using Microsoft.EntityFrameworkCore;
using MinimalApiTarefas.Entities;

namespace MinimalApiTarefas.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) 
            : base(options)
        {
        }
        public DbSet<TarefaEntity> Tarefas => Set<TarefaEntity>();
    }
}
