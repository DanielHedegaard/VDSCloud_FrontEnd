using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Web;
using MudBlazor.Services;
using Web.Services;
using Blazored.LocalStorage;
using Web.Models;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddBlazoredLocalStorageAsSingleton();
builder.Services.AddSingleton<IApiService, ApiService>();
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<Session>();

builder.Services.AddMudServices();

await builder.Build().RunAsync();