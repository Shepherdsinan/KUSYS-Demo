using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using EntityLayer.Concrete;
using KUSYS_Demo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KUSYS_Demo.Controllers;
[AllowAnonymous]
public class LoginController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IStudentService _studentService;

    public LoginController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IStudentService studentManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _studentService = studentManager;
    }

    [HttpGet]
    public IActionResult SignUp()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> SignUp(UserRegisterViewModel p)
    {
        // validate UserRegisterViewModel
        if (!ModelState.IsValid)
        {
            return View(p);
        }
        
        
        var student = _studentService.TGetById(p.StudentId);
        if (student == null)
        {
            ModelState.AddModelError("StudentId", "Öğrenci bulunamadı.");
            return View(p);
        }
        AppUser appUser = new AppUser()
        {
            Email = p.Mail,
            UserName = p.StudentId.ToString(),
            StudentId = p.StudentId
        };
        if (p.Password == p.ConfirmPassword)
        {
            var result = await _userManager.CreateAsync(appUser, p.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(appUser, "User");
                return RedirectToAction("SignIn");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
        }

        return View(p);
    }
    [HttpGet]
    public IActionResult SignIn()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> SignIn(UserSignInViewModel p)
    {
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(p.username, p.password, false, true);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Course");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Kullanıcı adı veya şifre hatalı.");
            }
        }
        return View();
    }

    public async Task<IActionResult> SignOut()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("SignIn", "Login");
    }
    public IActionResult AccessDenied()
    {
        return View();
    }
}
