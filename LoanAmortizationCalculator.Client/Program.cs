using LoanAmortizationCalculator.Client;
using LoanAmortizationCalculator.Client.Services;
using LoanAmortizationCalculator.Core.Interfaces;
using LoanAmortizationCalculator.Core.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// 1. Register MudBlazor Services
builder.Services.AddMudServices();

// 2. Register Core Services (Dependency Injection)
// We use AddScoped because Blazor WASM is a single-page app running in the browser.
// The instance lives as long as the tab is open (mostly).
builder.Services.AddScoped<ILoanCalculator, LoanCalculator>();
// Register the State Service as Scoped (one instance per user session)
builder.Services.AddScoped<LoanStateService>();

await builder.Build().RunAsync();