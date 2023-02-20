using MediatR;

namespace GoHorse.Core.Application.Services.Youtube.Handlers.Queries;

using GoHorse.Core.Application.Contracts.Providers;
using GoHorse.Core.Application.Models.Headless;
using GoHorse.Core.Application.Models.Headless.Selectors;
using GoHorse.Core.Application.Services.Youtube.Requests.Queries;
using GoHorse.Core.Application.Services.Youtube.Responses;
using GoHorse.Core.Domain.Modules.Videos;

public class ScrapYoutubeVideosQueryHandler : IRequestHandler<ScrapYoutubeVideosQuery, ScrapYoutubeVideosResponse>
{
  private readonly HeadlessProvider _headlessProvider;
  public ScrapYoutubeVideosQueryHandler(HeadlessProvider headlessProvider)
  {
    this._headlessProvider = headlessProvider;
  }

  public async Task<ScrapYoutubeVideosResponse> Handle(ScrapYoutubeVideosQuery request, CancellationToken cancellationToken)
  {
    List<Video> videos = new List<Video>();
    await this._headlessProvider.open(async () =>
    {
      foreach (string subject in request.subjects)
      {
        var durationQuery = "sp=EgIYAQ%253D%253D"; // 4m
        var url = $"https://youtube.com/results?search_query={subject}&{durationQuery}";

        if (request.process_callback.start is not null)
          request.process_callback.start(subject);

        await _headlessProvider.GoTo(new GoTo
        {
          url = url,
        });

        if (request.process_callback.middle is not null)
          request.process_callback.middle(subject);


        string youtubeContainer = "div#contents > ytd-video-renderer.ytd-item-section-renderer";
        var evaluatedVideos = await _headlessProvider.GetElementsByEvaluateFunction<Video>(new GetElementsByEvaluateFunction
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

        videos.AddRange(evaluatedVideos);

        if (request.process_callback.end is not null)
          request.process_callback.end(subject);
      }
    }, new Open
    {
      headless = request.headless
    });



    return videos;
  }
}