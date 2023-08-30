using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Zaka.Core.WEB;
using Zaka.Core.WEB.Models;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddSingleton<Number>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7078/") });

await builder.Build().RunAsync();
