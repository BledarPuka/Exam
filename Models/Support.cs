#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CSharpExamBledar.Models;

public class Support {
    [Key]
    public int SupportId { get; set; }

    public int? UserId { get; set; }
    public User? Supporter { get; set; }
    public int? ProjectId { get; set; }
    public Project? Project { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Support must be greater than 0")]
    public double SupportAmount { get; set; }

    public double? PercentageFunded => Project?.Goal == 0 ? 0 : Math.Round((double)SupportAmount / (Project?.Goal ?? 1) * 100, 2);

    public DateTime CreatedAt { get; set; } = DateTime.Now;        
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

}