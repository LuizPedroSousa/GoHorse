using GoHorse.Core.Application.Contracts.Providers;
using GoHorse.Infrastructure.Providers.Headless;
using Microsoft.Extensions.DependencyInjection;

namespace GoHorse.Infrastructure;

public static class InfrastructureRegistrationServices
{
  public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services)
  {
    services.AddScoped<HeadlessProvider, PuppeteerHeadlessProvider>();
    return services;
  }
}