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

    public LoginController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpGet]
    public IActionResult SignUp()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> SignUp(UserRegisterViewModel p)
    {

        AppUser appUser = new AppUser()
        {
            Email = p.Mail,
            UserName = p.Username,
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
                return RedirectToAction("GetListStudentandCourses", "Student");
            }
            else
            {
                return RedirectToAction("SignIn", "Login");
            }
        }
        return View();
    }

    public async Task<IActionResult> SignOut()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("SignIn", "Login");
    }
}
