using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CSharpExamBledar.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CSharpExamBledar.Controllers;
public class SessionCheckAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        // Find the session, but remember it may be null so we need int?
        int? UserId = context.HttpContext.Session.GetInt32("UserId");
        // Check to see if we got back null
        if(UserId == null)
        {
            // Redirect to the Index page if there was nothing in session
            // "Home" here is referring to "HomeController", you can use any controller that is appropriate here
            context.Result = new RedirectToActionResult("Auth", "Home", null);
        }
    }
}

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
     private MyContext _context; 

    public HomeController(ILogger<HomeController> logger, MyContext context)    
    {        
        _logger = logger;
        _context = context;    
    } 


    [HttpGet("Auth")]
    public IActionResult Auth()
    {
        
        return View();
    }

    [HttpPost("Register")]
    public IActionResult Register(User RegisterForm)
    {
            
        if (ModelState.IsValid)
        {   
            PasswordHasher<User> Hasher = new PasswordHasher<User>();        
            RegisterForm.Password = Hasher.HashPassword(RegisterForm, RegisterForm.Password); 
            _context.Add(RegisterForm);
            _context.SaveChanges();
            HttpContext.Session.SetInt32("UserId", RegisterForm.UserId);
            return RedirectToAction("Index");
        }
        return View("Auth");

    }

    [HttpPost("Login")]
    public IActionResult Login(Login LoginForm)
    {
            
        if (ModelState.IsValid)
        {   
            User UserDB = _context.Users.FirstOrDefault(e=> e.Email == LoginForm.LoginEmail);
            if(UserDB == null)        
            {                       
                ModelState.AddModelError("LoginEmail", "Invalid Email");            
                return View("Auth");        
            } 
        
            PasswordHasher<Login> hasher = new PasswordHasher<Login>(); 
            var result = hasher.VerifyHashedPassword(LoginForm, UserDB.Password, LoginForm.LoginPassword);
            if(result == 0)        
            {            
                ModelState.AddModelError("LoginPassword", "Invalid Password");            
                return View("Auth");  
            }
            HttpContext.Session.SetInt32("UserId", UserDB.UserId);
            return RedirectToAction("Index");
            
        }
        return View("Auth");

    }
    
    [HttpGet("Logout")]
    public IActionResult Logout(){
        HttpContext.Session.Clear();
        return RedirectToAction("Auth");
    }

    [SessionCheck]
    public IActionResult Index()
    {
        ViewBag.userId = HttpContext.Session.GetInt32("UserId");
        List<Project> projects = _context.Projects.Include(e => e.Creator).Include(e => e.ListSupport).ToList();
        ViewBag.projects = projects;
    
        List<Project> fundedProjects = projects.Where(p => p.ListSupport.Sum(support => support.SupportAmount) >= p.Goal).ToList();
        ViewBag.totalFundedProjects = fundedProjects.Count;

        double overallTotalFunded = projects.Sum(p => p.ListSupport.Sum(support => support.SupportAmount));
        ViewBag.overallTotalFunded = overallTotalFunded;

    
        
        return View();
    }

    [SessionCheck]
    [HttpGet("Projects/new")]
    public IActionResult StartProject()
    {
        return View();
    }

    [SessionCheck]
    [HttpPost]
    public IActionResult CreateProject(Project projectForm) {
        int? userId = HttpContext.Session.GetInt32("UserId");
        if(ModelState.IsValid) {
            projectForm.UserId = userId;

            _context.Add(projectForm);
            _context.SaveChanges();

            return RedirectToAction("ProjectDetails", new { itemid = projectForm.ProjectId });

        }
        return View("StartProject");
    }

    [SessionCheck]
    [HttpGet]
    public IActionResult DeleteProject(int itemid)
    {
        Project delete = _context.Projects.Include(e => e.Creator).Include(e => e.ListSupport).FirstOrDefault(e => e.ProjectId == itemid);
        _context.Remove(delete);
        _context.SaveChanges(); 
        return RedirectToAction("Index");
    }

    [SessionCheck]
    [HttpGet("projects/{itemid}")]
    public IActionResult ProjectDetails(int itemid)
    {
        ViewBag.userId = HttpContext.Session.GetInt32("UserId");
        
        Project project = _context.Projects.Include(e => e.Creator).Include(e => e.ListSupport).FirstOrDefault(e => e.ProjectId == itemid);
        ViewBag.projects = project;

        double totalFunded = project.ListSupport.Sum(support => support.SupportAmount);
        ViewBag.totalFunded = totalFunded;

        int numberOfSupporters = project.ListSupport.Count;
        ViewBag.numberOfSupporters = numberOfSupporters;

        int daysRemaining = (int)(project.EndDate.Date - DateTime.Now.Date).TotalDays;
        ViewBag.daysRemaining = daysRemaining;

        bool hasSupported = project.ListSupport.Any(support => support.UserId == ViewBag.userId);
        ViewBag.hasSupported = hasSupported;

       
        return View();
    }

    [SessionCheck]
    [HttpPost]
    public IActionResult SupportProject(Support supportForm, int itemid)
    {
        if (ModelState.IsValid)
        {
            supportForm.UserId = HttpContext.Session.GetInt32("UserId");
            supportForm.ProjectId = itemid;
            _context.Add(supportForm);
            _context.SaveChanges();
            return RedirectToAction("ProjectDetails", new{itemid = supportForm.ProjectId});
        }
        
        int? Userid = HttpContext.Session.GetInt32("UserId");
        ViewBag.userId = Userid;

        Project project = _context.Projects.Include(e => e.Creator).Include(e => e.ListSupport).FirstOrDefault(e => e.ProjectId == itemid);
        ViewBag.projects = project;

        double totalFunded = project.ListSupport.Sum(support => support.SupportAmount);
        ViewBag.totalFunded = totalFunded;

        int numberOfSupporters = project.ListSupport.Count;
        ViewBag.numberOfSupporters = numberOfSupporters;

        int daysRemaining = (int)(project.EndDate.Date - DateTime.Now.Date).TotalDays;
        ViewBag.daysRemaining = daysRemaining;

        bool hasSupported = project.ListSupport.Any(support => support.UserId == Userid);
        ViewBag.hasSupported = hasSupported;
        return View("ProjectDetails");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
