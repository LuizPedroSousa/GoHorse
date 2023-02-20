using System.ComponentModel.DataAnnotations;
using McMaster.Extensions.CommandLineUtils;
using MediatR;
using Newtonsoft.Json;
using Spectre.Console;

namespace CLI.Commands.Finder;

using GoHorse.CLI.Commands.Shared;
using GoHorse.Core.Application.Services.Youtube.Requests.Queries;

[Command("fdr", Description = "Scraping for video links")]
public class FinderCommand : BaseCommand
{
  private readonly IMediator _mediator;
  public FinderCommand(IMediator mediator)
  {
    this._mediator = mediator;
  }

  [Option("-p|--provider")]
  [RegularExpression("ytb|tiktok", ErrorMessage = "Provider must be equal to 'ytb' or 'tiktok'")]
  public string provider { get; set; } = "ytb";

  [Option("-nh|--no-headless")]
  public bool headless { get; set; }

  [Required(AllowEmptyStrings = false, ErrorMessage = "Subject must be provided for fdr ⚠️")]
  [Argument(0, Description = "List of subjects to find")]
  public string[] subjects { get; set; }

  protected async override Task<int> OnExecute(CommandLineApplication app)
  {
    await AnsiConsole.Status().StartAsync($"Initializing headless", async (context) =>
    {

      var process_callback = GetVideoProcessCallback(context);

      var videosOrError = await this._mediator.Send(
        provider switch
        {
          _ =>
            new ScrapYoutubeVideosQuery(
            subjects,
            headless: !headless,
            process_callback: process_callback
            )
        }
        );
      context.Refresh();

      var table = new Table().Centered().AddColumns(
        "ID",
        "Title",
        "Subject",
        "Views",
        "URL"
        ).Expand();

      table.Border(TableBorder.Rounded);
      table.Title("Videos");


      videosOrError.right.OrderBy(video => video.views).ToList();

      videosOrError.right.ForEach((video) => table.AddRow(video.id, "-", video.subject, video.views.ToString(), video.url));
      AnsiConsole.Write(table);

      var chart = new BreakdownChart()
           .ShowTags().FullSize();

      for (int i = 0; i < subjects.Count(); i++)
      {
        var subject = subjects[i];

        chart.AddItem(subject, videosOrError.right.Where(video => video.subject == subject).Count(), Color.FromInt32(i));
      }
      AnsiConsole.Write(
        chart
      );
    });

    return await base.OnExecute(app);
  }

  private static ProcessCallback GetVideoProcessCallback(StatusContext context)
  {
    return new ProcessCallback
    {
      start = (string subject) =>
      {
        context.Status($"[blue bold] Opening[/] [underline]{subject}[/]");
        context.Spinner(Spinner.Known.Circle);
      },
      middle = (string subject) =>
      {
        AnsiConsole.MarkupLineInterpolated($":check_mark_button: [green]{subject} has been openned[/]");

        context.Status($"[blue bold]Scraping videos[/] [underline link]{subject}[/]");
      },
      end = (string subject) =>
      {
        AnsiConsole.MarkupLineInterpolated($":check_mark_button: [green] videos for {subject} has been succed[/]");
      }
    };
  }
}
