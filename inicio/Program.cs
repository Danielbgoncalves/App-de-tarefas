using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Globalization;
using inicio;
using MudBlazor.Services;
using TaskApp.Models;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configuração do HttpClient para o backend
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:5267/") });

// Adicionando MudBlazor Services
builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = MudBlazor.Defaults.Classes.Position.BottomRight;
});

// Adicionando o serviço de Tarefas
builder.Services.AddSingleton<TarefasService>();

// Registre o IHttpClientFactory para ser usado no frontend
builder.Services.AddHttpClient();

// Configuração de Cultura (localização)
var culture = new CultureInfo("pt-BR");
CultureInfo.DefaultThreadCurrentCulture = culture;
CultureInfo.DefaultThreadCurrentUICulture = culture;

// Construção da aplicação
await builder.Build().RunAsync();
