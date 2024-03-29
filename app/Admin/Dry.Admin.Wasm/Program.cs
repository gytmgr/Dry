using Dry.Admin.Application.RESTFul.Client;
using Dry.Admin.Wasm.Common;
using Dry.Dependency;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Dry.Admin.Wasm
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddDependency(true);

            builder.Services.AddAntDesign();

            builder.Services.AddSingleton<LoginUser<string>>();
            var host = builder.Build();
            var clientRequestConfigurer = host.Services.GetService<ClientRequestConfigurer>();
            clientRequestConfigurer.ApiUrl = "http://localhost:61073";
            await host.RunAsync();
        }
    }
}