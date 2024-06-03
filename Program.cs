using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using VehicleInfoApp;
using VehicleInfoApp.Services;
using VehicleInformationApp;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp =>
{
    var httpClient = new HttpClient
    {
        BaseAddress = new Uri("https://beta.check-mot.service.gov.uk/")
    };

    // Include the API key in the request headers
    httpClient.DefaultRequestHeaders.Add("x-api-key", "fZi8YcjrZN1cGkQeZP7Uaa4rTxua8HovaswPuIno");

    return httpClient;
});

builder.Services.AddScoped<VehicleService>();

await builder.Build().RunAsync();
