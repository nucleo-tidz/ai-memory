using infrastructure.Agents;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;

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
    }
}
