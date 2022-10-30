using GoHorse.CLI.Commands;
using GoHorse.Infrastructure;
using Microsoft.Extensions.Hosting;

var builder = new HostBuilder().ConfigureServices((hostContext, services) =>
{
  services.ConfigureInfrastructureServices();
});

return await builder.RunCommandLineApplicationAsync<GoHorseCommand>(args);