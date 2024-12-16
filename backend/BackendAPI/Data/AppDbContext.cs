using Microsoft.EntityFrameworkCore;
using BackendAPI.Models;

namespace BackendAPI.Data
{
    public class AppDbContext : DbContext // herda DbContext que torna ela um contexto entendido pela Entity para criar o CRUD
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) // o super
        {  
        }

        public DbSet<Tarefa> Tarefas { get; set; } // Corrigido para "public" e adicionado "DbSet" corretamente

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
          optionsBuilder.UseSqlite("Data Source=tarefas.db");
        }
    }
}
