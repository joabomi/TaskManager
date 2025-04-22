using System.Reflection;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TaskManager.BlazorUI;
using TaskManager.BlazorUI.Contracts;
using TaskManager.BlazorUI.Services;
using TaskManager.BlazorUI.Services.Base;
using TaskManager.BlazorUI.Providers;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });


builder.Services.AddHttpClient<IClient, Client>(Client => Client.BaseAddress = new Uri("https://localhost:7269"));

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();

builder.Services.AddScoped<IWorkTaskService, WorkTaskService>();
builder.Services.AddScoped<IWorkTaskStatusTypeService, WorkTaskStatusTypeService>();
builder.Services.AddScoped<IWorkTaskPriorityTypeService, WorkTaskPriorityTypeService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

await builder.Build().RunAsync();
