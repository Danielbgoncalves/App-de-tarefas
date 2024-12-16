namespace TaskApp.Models
{
    public class Tarefa
    {
        public int Id {get; set;}
        public string NomeUsuarioCriador {get; set;} =  string.Empty;
        public string Titulo {get; set;} = string.Empty;
        public string Descricao {get; set;} = string.Empty;
        public bool Concluida {get; set;}
        public DateTime DataFinal {get; set;}

        public DateTime? DataConclusao {get; set;} = null;
        public int ElevationLevel {get; set;} = 2;

        public Boolean Excluida {get; set;}
        public Tarefa()
        {

        }

        public void MarcarComoConcluida()
        {
            Concluida = true;
            DataConclusao = DateTime.Now;
        }
        public void ExcluirTarefa()
        {
            Concluida = true;
            Excluida = true;
        }

        public void ElevationChange( int level)
        {
            ElevationLevel = level;
        }

        public void Restaurar()
        {
            Concluida = false;
            Excluida = false;
        }    

        
    }
}