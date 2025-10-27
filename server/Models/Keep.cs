using System.ComponentModel.DataAnnotations;

namespace keepr.Models;

public class Keep
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    [Url] public string Img { get; set; }
    public int Views { get; set; }
    public string CreatorId { get; set; }
    public Creator Creator { get; set; }
    public VaultKeep Kept { get; set; }

    public int VaultKeepId { get; set; }

}