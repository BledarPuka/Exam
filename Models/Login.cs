#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CSharpExamBledar.Models;
public class Login
{    
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress]
    public string LoginEmail { get; set; }    
    
    [Required(ErrorMessage = "The Password is required")]
    [DataType(DataType.Password)]
    public string LoginPassword { get; set; } 
}