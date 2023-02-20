using MediatR;

namespace GoHorse.Core.Application.Services.Youtube.Requests.Queries;

using GoHorse.Core.Application.Services.Youtube.Responses;

public class ProcessCallback
{
  public Action<string> start { get; set; }
  public Action<string> middle { get; set; }
  public Action<string> end { get; set; }
}

public class ScrapYoutubeVideosQuery : IRequest<ScrapYoutubeVideosResponse>
{
  public string[] subjects { get; private set; }
  public string duration { get; private set; }

  public ProcessCallback process_callback { get; private set; }

  public bool headless { get; private set; }

  public ScrapYoutubeVideosQuery(string[] subjects, ProcessCallback process_callback, bool headless)
  {
    this.subjects = subjects;
    this.process_callback = process_callback;
    this.headless = headless;
  }
}