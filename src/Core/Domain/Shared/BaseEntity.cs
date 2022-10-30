namespace GoHorse.Domain.Shared;

public class BaseEntity
{
  public BaseEntity()
  {
    if (this.id is null)
    {
      this.id = Guid.NewGuid().ToString();
    }
  }

  public string id { get; set; }
  public DateTime createdAt { get; set; }


}
