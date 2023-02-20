using GoHorse.Core.Domain.Shared;

namespace GoHorse.Core.Domain.Modules.Videos;

public enum VideoStatus
{
  trending,
  common
}

public enum VideoType
{
  shorted,
  normal
}

public class Video : BaseEntity
{
  public string title { get; set; }
  public int views { get; set; }
  public int likes { get; set; }
  public int dislikes { get; set; }
  public string url { get; set; }
  public string subject { get; set; }
  public VideoStatus status { get; set; }
  public VideoType type { get; set; }

  public Video(string title, int views, int likes, int dislikes, string url, string subject, VideoStatus status, VideoType type)
  {
    this.title = title;
    this.views = views;
    this.likes = likes;
    this.dislikes = dislikes;
    this.url = url;
    this.subject = subject;
    this.status = status;
    this.type = type;
  }
}