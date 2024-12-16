using Microsoft.AspNetCore.Mvc;
using BackendAPI.Models;
using BackendAPI.Data;

[ApiController]
[Route("api/[controller]")]
public class TarefasController : ControllerBase
{
    private readonly AppDbContext _context; // usa '_' pq é privada, só convenção
    public TarefasController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetTarefas()
    {
        var tarefas = _context.Tarefas.ToList();
        return Ok(tarefas);
    }

    [HttpPost]
    public IActionResult PostTarefa(Tarefa novaTarefa)
    {
        if(novaTarefa == null) return BadRequest();

        _context.Tarefas.Add(novaTarefa);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetTarefas), new {id = novaTarefa.Id}, novaTarefa);
    }
}

