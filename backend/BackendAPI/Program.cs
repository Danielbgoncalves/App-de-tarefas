using Microsoft.EntityFrameworkCore;
using BackendAPI.Models;
using BackendAPI.Data;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Adiciona o serviço DbContext e a string de conexão com o SQLite
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite("Data Source=tarefas.db"));

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
            {
                policy.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });

        builder.Services.AddControllers();

        builder.Services.AddHttpClient();

        var app = builder.Build();

        app.UseCors("AllowAll");

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}



// Adiciona os serviços padrão

