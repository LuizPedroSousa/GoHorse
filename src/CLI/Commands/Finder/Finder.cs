using System.ComponentModel;
using McMaster.Extensions.CommandLineUtils;
using System.ComponentModel.DataAnnotations;
using GoHorse.CLI.Commands.Shared;
using GoHorse.Application.Contracts.Providers;
using GoHorse.Application.Models.Headless;
using Spectre.Console;
using GoHorse.Application.Models.Headless.Selectors;
using GoHorse.Domain.Modules.Videos;
using Newtonsoft.Json;

namespace CLI.Commands.Finder;

[Command("fdr", Description = "Scraping for video links")]
public class Finder : BaseCommand
{
  private readonly HeadlessProvider _headlessProvider;
  public Finder(HeadlessProvider headlessProvider)
  {
    _headlessProvider = headlessProvider;
  }

  [Option("-p|--provider")]
  [RegularExpression("ytb|tiktok", ErrorMessage = "Provider must be equal to 'ytb' or 'tiktok'")]
  public string provider { get; set; } = "ytb";

  [Option("-nh|--no-headless")]
  public bool headless { get; set; } = true;

  [Required(AllowEmptyStrings = false, ErrorMessage = "Subject must be provided for fdr ⚠️")]
  [Argument(0, Description = "List of subjects to find")]
  public string[] subjects { get; set; }

  private List<Video> videos = new List<Video>();

  protected async override Task<int> OnExecute(CommandLineApplication app)
  {

    await ScrapYoutube();

    return await base.OnExecute(app);
  }


  private async Task ScrapYoutube()
  {
    await AnsiConsole.Status().StartAsync($"Initializing headless", async (context) =>
    {
      await _headlessProvider.open(async () =>
      {
        foreach (string subject in subjects)
        {
          var durationQuery = "sp=EgIYAQ%253D%253D"; // 4m
          var url = $"https://youtube.com/results?search_query={subject}&{durationQuery}";

          context.Status($"[blue bold] Opening[/] [underline]{subject}[/]");
          context.Spinner(Spinner.Known.Circle);

          await _headlessProvider.GoTo(new GoTo
          {
            url = url,
          });

          AnsiConsole.MarkupLineInterpolated($":check_mark_button: [green]{subject} has been openned[/]");

          context.Status($"[blue bold]Scraping videos[/] [underline link]{subject}[/]");
          string youtubeContainer = "div#contents > ytd-video-renderer.ytd-item-section-renderer";
          var videos = await _headlessProvider.GetElementsByEvaluateFunction<Video>(new GetElementsByEvaluateFunction
          {
            target = youtubeContainer,
            scriptParams = new object[] { youtubeContainer, subject },
            script = @"(container, subject) => {
                  const cards = document.querySelectorAll(container);

                  const videos = []; 

                  cards.forEach((card, index) => {
                    const title = card.querySelector('div:first-of-type > div:last-of-type > div#meta > div#title-wrapper h3 a#video-title yt-formatted-string').textContent;
                    const url = card.querySelector('div:first-of-type > div:last-of-type > div#meta > div#title-wrapper h3 a#video-title').getAttribute('href');
                    const views = card.querySelector('div:first-of-type > div:last-of-type > div#meta > ytd-video-meta-block > div:first-of-type div#metadata-line span:first-of-type').textContent;
                    videos.push({
                      title,
                      url: `https://youtube.com${url}`,
                      subject,
                      views: Number(views.replace(/\D/g, '')),
                    })
                  });

                  return Array.from(videos);
                }"
          });
          this.videos.AddRange(videos);

          AnsiConsole.MarkupLineInterpolated($":check_mark_button: [green] videos for {subject} has been succed[/]");
        }
        context.Refresh();
      }, new Open
      {
        headless = headless
      });

      var table = new Table().Centered().AddColumns(
        "ID",
        "Title",
        "Subject",
        "Views",
        "URL"
        ).Expand();

      table.Border(TableBorder.Rounded);
      table.Title("Videos");


      videos = this.videos.OrderBy(video => video.views).ToList();


      this.videos.ForEach((video) => table.AddRow(video.id, "-", video.subject, video.views.ToString(), video.url));
      AnsiConsole.Write(table);

      var chart = new BreakdownChart()
           .ShowTags().FullSize();

      for (int i = 0; i < subjects.Count(); i++)
      {
        var subject = subjects[i];

        chart.AddItem(subject, videos.Where(video => video.subject == subject).Count(), Color.FromInt32(i));
      }
      AnsiConsole.Write(
        chart
      );
    });
  }
}
