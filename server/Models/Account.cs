namespace keepr.Models;

public class Creator
{
  public string Name { get; set; }
  public string Id { get; set; }
  public string Picture { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
}
public class Account : Creator
{
  public string Email { get; set; }
}

