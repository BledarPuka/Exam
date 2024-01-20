#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CSharpExamBledar.Models;
public class User
{    
    [Key]    
    public int UserId { get; set; }
    
    [Required]
    [MinLength(2, ErrorMessage = "First Name must be at least 2 characters")]
    public string FirstName { get; set; }
    
    [Required]
    [MinLength(2, ErrorMessage = "Last Name must be at least 2 characters")]        
    public string LastName { get; set; }     
    [Required(ErrorMessage = "The Email is required")]
    [EmailAddress]
    [UniqueEmail]
    public string Email { get; set; }    
    
    [Required(ErrorMessage = "The Password is required")]
    [DataType(DataType.Password)]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
    public string Password { get; set; } 

    public List<Project>? Projects {get;set;}
    public List<Support>? Supports {get;set;}
    
    public DateTime CreatedAt {get;set;} = DateTime.Now;   
    public DateTime UpdatedAt {get;set;} = DateTime.Now;

    [Required(ErrorMessage = "The Password is required")]
    [NotMapped]
    [Compare("Password", ErrorMessage = "The Confirm Password must match Password")]
    [DataType(DataType.Password)]
    public string PasswordConfirm { get; set; }
}