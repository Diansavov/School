using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestIgnatov.Models;
using TestIgnatov.Models.ViewModels;

namespace TestIgnatov.Controllers;

public class UsersController : Controller
{
    private readonly UserManager<Users> _userManager;
    private readonly SignInManager<Users> _signInManager;
    public UsersController(UserManager<Users> userManager, SignInManager<Users> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
    public IActionResult Register()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Register(UserRegister userRegister)
    {
        if (ModelState.IsValid)
        {
            Users user = new Users()
            {
                UserName = userRegister.UserName,
                Email = userRegister.Email,
                DateOfBirth = userRegister.DateOfBirth,
            };
            var result = await _userManager.CreateAsync(user, userRegister.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
                await _signInManager.SignInAsync(user, isPersistent: false);

                return RedirectToAction("Index", "Home");
            }
        }
        return View(userRegister);
    }



    public IActionResult LogIn()
    {
        return View();
    }
}
