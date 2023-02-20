using GoHorse.Core.Domain.Modules.Videos;
using GoHorse.Core.Domain.Shared;

namespace GoHorse.Core.Application.Services.Youtube.Responses;

public class ScrapYoutubeVideosResponse : Either<BaseException, List<Video>>
{
  public ScrapYoutubeVideosResponse(BaseException left) : base(left)
  {
  }

  public ScrapYoutubeVideosResponse(List<Video> right) : base(right)
  {
  }

  public static implicit operator ScrapYoutubeVideosResponse(List<Video> result) => new ScrapYoutubeVideosResponse(result);
}