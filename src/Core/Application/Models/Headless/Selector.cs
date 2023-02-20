namespace GoHorse.Core.Application.Models.Headless
{
  public class Selector
  {
    public string target { get; set; }
    public int? timeout { get; set; }
    public int maxSelectorTimeout { get; set; }
    public bool isIframe { get; set; }
    public bool logError { get; set; } = true;
    public ActionCheck? checker { get; set; }
  }
}