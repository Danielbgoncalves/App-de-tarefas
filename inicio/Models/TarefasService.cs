//using System.Collections.Generic;
//using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

/*
    Tem por finalidade gerar uma cópia atualizada e local do banco de dados, 
    cada interação com as tarefas será feita em `Tarefas`. Assim só é 
    necessário coneção com a base de dados quando os dados mudam, até lá,
    `Tarefas` é uma opção mais rápida e barata. 
     
    Notar também que tarefas não é atualizado usando métodos de listas, 
    embora fosse ser mais rápido e eficiente em casos de acessos 
    simultaneos a base de dados fazer sempre um get para a base de dados 
    mais atual garante um sistema amis robusto e confiavel.
*/

namespace TaskApp.Models
{
    class TarefasService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _baseUrl = "http://localhost:5267/api/tarefas";
        public List<Tarefa> Tarefas {get; private set;} = new List<Tarefa>();
        public TarefasService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // Método para fazer o Get
        public async Task BuscaListaTarefas() // Task é o void de funções asincronas do C#
        {
            Console.WriteLine("Inicioou a busca");
            var httpClient = _httpClientFactory.CreateClient();
            Console.WriteLine("Fez o http");
            var response = await httpClient.GetStringAsync(_baseUrl); // faz o get em sí
            Console.WriteLine("Recebeu a resposta");
            Tarefas = JsonConvert.DeserializeObject<List<Tarefa>>(response) ?? new List<Tarefa>();
            Console.WriteLine("Tarefas carregado");
        }

        // Método para fazer o Post
        public async void AddTarefa(Tarefa tarefaNova)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var json = JsonConvert.SerializeObject(tarefaNova);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            await httpClient.PostAsync(_baseUrl, content);
            Console.WriteLine("Criou a tarefa");
        
            var response = await httpClient.GetStringAsync(_baseUrl);
            Tarefas = JsonConvert.DeserializeObject<List<Tarefa>>(response) ?? new List<Tarefa>();
            Console.WriteLine("Criou a tarefa em Tarefa");
        }

        // Método para deletar
        public async void RemoveTarefa(int id)
        {
            var httpClient = _httpClientFactory.CreateClient();
            await httpClient.DeleteAsync($"{_baseUrl}/{id}");

            var response = await httpClient.GetStringAsync(_baseUrl);
            Tarefas = JsonConvert.DeserializeObject<List<Tarefa>>(response) ?? new List<Tarefa>();
        }

        // Método para atualizar
        public async void AtualizarTarefa(Tarefa tarefaAtualizada)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var json = JsonConvert.SerializeObject(tarefaAtualizada);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application,json");
            await httpClient.PutAsync($"{_baseUrl}/{tarefaAtualizada.Id}", content);

            var response = await httpClient.GetStringAsync(_baseUrl);
            Tarefas = JsonConvert.DeserializeObject<List<Tarefa>>(response) ?? new List<Tarefa>();
        }

        public Boolean IsEmpty()
        {
            return Tarefas.Count <= 0;
        }

        public (int Pendentes, int Concluidas, int Atrasadas) ContarTarefas()
        {
            int Pendentes = Tarefas.Count(t => !t.Concluida);
            int ConcluidasNoUltimos7Dias = Tarefas.Count(t => t.Concluida &&  t.DataConclusao != null );
            int Atrasadas = Tarefas.Count(t => t.DataFinal < DateTime.Now && !t.Concluida);
            return (Pendentes, ConcluidasNoUltimos7Dias, Atrasadas);
        }

        public List<Tarefa> ObterProximasTarefas(int quantidade)
        {
            var NovaLista = Tarefas
                .Where(t => !t.Concluida)
                .OrderBy(t => t.DataFinal)
                .Take(quantidade)
                .ToList();

            return NovaLista;
        }
        
    }
}