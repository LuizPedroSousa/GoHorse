namespace GoHorse.Core.Domain.Shared;

public class BaseException
{
  public string message { get; set; }

  public BaseException(string message)
  {
    this.message = message;
  }
}