using BookmarksFront;
using BookmarksFront.Classes.Services;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<ICookie, Cookie>(); //SO added from stackOverflow check link:"Classes/Services/Cookie.cs"
builder.Services.AddScoped<IBookmarkService, BookmarkService>();
builder.Services.AddScoped<CheckKey, CheckKey>();
builder.Services.AddSingleton<TempData, TempData>();


// await builder.Build().RunAsync();

var host = builder.Build();
host.Services.GetRequiredService<TempData>();
await host.RunAsync();