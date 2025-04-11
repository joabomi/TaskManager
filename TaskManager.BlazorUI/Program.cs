using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TaskManager.BlazorUI;
using TaskManager.BlazorUI.Contracts;
using TaskManager.BlazorUI.Services;
using TaskManager.BlazorUI.Services.Base;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });


builder.Services.AddHttpClient<IClient, Client>(Client => Client.BaseAddress = new Uri("http://localhost:5041"));

builder.Services.AddScoped<IWorkTaskService, WorkTaskService>();
builder.Services.AddScoped<IWorkTaskStatusTypeService, WorkTaskStatusTypeService>();
builder.Services.AddScoped<IWorkTaskPriorityTypeService, WorkTaskPriorityTypeService>();

await builder.Build().RunAsync();
