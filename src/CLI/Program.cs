using GoHorse.CLI.Commands;
using GoHorse.Core.Application;
using GoHorse.Infrastructure;
using Microsoft.Extensions.Hosting;

var builder = new HostBuilder().ConfigureServices((hostContext, services) =>
{
  services.ConfigureApplicationServices();
  services.ConfigureInfrastructureServices();
});

return await builder.RunCommandLineApplicationAsync<GoHorseCommand>(args);