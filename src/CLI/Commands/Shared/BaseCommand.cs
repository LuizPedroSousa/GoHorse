using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;

namespace GoHorse.CLI.Commands.Shared;

public class BaseCommand
{
  protected virtual async Task<int> OnExecute(CommandLineApplication app)
  {
    if (app.Arguments.Count == 0 && app.Options.Count == 2)
    {
      app.ShowHelp();
    }

    return await Task.FromResult(0);
  }
}
