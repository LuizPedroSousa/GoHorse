namespace GoHorse.Application.Models.Headless;

public class GoTo
{
  public string url { get; set; }
  public int navigationTimeout { get; set; }
  public int? timeout { get; set; }
}
