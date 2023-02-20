using System.Reflection;
using CLI.Commands.Finder;
using GoHorse.CLI.Commands.Shared;
using McMaster.Extensions.CommandLineUtils;

namespace GoHorse.CLI.Commands;

[Command("gohorse")]
[Subcommand(typeof(FinderCommand))]
public class GoHorseCommand : BaseCommand
{

  [Option("-v", Description = "Show current version of GoHorse")]
  public bool version { get; set; } = false;

  private static string GetVersion() => typeof(GoHorseCommand).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;

  protected async override Task<int> OnExecute(CommandLineApplication app)
  {

    if (version)
    {
      Console.WriteLine(GetVersion());
    }


    return await base.OnExecute(app);
  }

}
