using System.ComponentModel.DataAnnotations;
namespace BackendAPI.Models
{
    public class Tarefa
    {
        [Key]
        public int Id {get; set;}
        
        [Required]
        public string NomeUsuarioCriador {get; set;} =  string.Empty;
        public string Titulo {get; set;} = string.Empty;
        public string Descricao {get; set;} = string.Empty;
        public bool Concluida {get; set;}
        public DateTime DataFinal {get; set;}
        public DateTime? DataConclusao {get; set;} = null;
        public int ElevationLevel {get; set;} = 2;

        [Required]
        public Boolean Excluida {get; set;}
    }
}