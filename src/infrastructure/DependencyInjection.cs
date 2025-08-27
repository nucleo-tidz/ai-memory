using infrastructure.Agents;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Memory;

namespace infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }

        public static IServiceCollection AddSemanticKernel(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddTransient<Kernel>(serviceProvider =>
            {
                IKernelBuilder kernelBuilder = Kernel.CreateBuilder();
                kernelBuilder.Services.AddAzureOpenAIChatCompletion("o4-mini",
                  configuration["o4-mini-url"],
                  configuration["o4-mini-key"],
                   "o4-mini",
                   "o4-mini");
                return kernelBuilder.Build();


            }).AddTransient<INucleotidzAgent,NucleotidzAgent>();
        }
        public static IServiceCollection AddMemory(this IServiceCollection services, IConfiguration configuration)
        {
            var baseUrl = configuration["Memory:BaseUrl"] ?? "https://api.mem0.ai";
            var token = configuration["Memory:Token"];
                        services.AddHttpClient("MemoryClient", client =>
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Token", token);
            });
            return services;
        }
    }
}
