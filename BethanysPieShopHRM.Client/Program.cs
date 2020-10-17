using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using BethanysPieShopHRM.App.Services;

namespace BethanysPieShopHRM.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<BethanysPieShopHRM.App.App>("app");

            builder.Services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
            });
            builder.Services.AddHttpClient<IEmplyeeDataService, EmployeeDataService>(
                client => client.BaseAddress = new Uri("https://localhost:44330"));
            builder.Services.AddHttpClient<ICountryDataService, CountryDataService>(
                client => client.BaseAddress = new Uri("https://localhost:44330"));
            builder.Services.AddHttpClient<IJobCategoryDataService, JobCategoryDataService>(
                client => client.BaseAddress = new Uri("https://localhost:44330"));

            await builder.Build().RunAsync();
        }
    }
}
