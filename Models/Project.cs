#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CSharpExamBledar.Models;
public class Project
{    
    [Key]
    public int ProjectId { get; set; }

    [Required(ErrorMessage = "Title is required")]
    public string Title { get; set; }    
    
    [Required(ErrorMessage = "Goal is required")]
    [Range(1, double.MaxValue, ErrorMessage = "Goal must be greater than 0")]
    public double Goal { get; set; } 

    [Required(ErrorMessage = "End Date is required")]
    [DataType(DataType.Date)]
    [Display(Name = "End Date")]
    [FutureDate]
    public DateTime EndDate { get; set; }

    public string Description { get; set; }

    public int? UserId { get; set; }
    public User? Creator { get; set; }

    public List<Support>? ListSupport { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}

public class FutureDateAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is DateTime dateValue)
        {
            if (dateValue.Date < DateTime.Now.Date)
            {
                return new ValidationResult("The date must be in the future.");
            }
        }

        return ValidationResult.Success;
    }
}