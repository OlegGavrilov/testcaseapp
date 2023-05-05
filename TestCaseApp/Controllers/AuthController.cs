using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestCaseApp.Dto;
using TestCaseApp.Mappers;
using TestCaseApp.Services;

namespace TestCaseApp.Controllers;

public record RegistrationRequest([Required] string UserName, [Required] string Password);

public record AuthRequest(string Username, string Password);

public record AuthResponse(TestCaseAppUserDto User, string Token);

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<TestCaseAppUser> _userManager;
    private readonly UsersContext _context;
    private readonly TokenService _tokenService;

    public AuthController(UserManager<TestCaseAppUser> userManager, UsersContext context, TokenService tokenService)
    {
        _userManager = userManager;
        _context = context;
        _tokenService = tokenService;
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> RegisterAsync(RegistrationRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _userManager.CreateAsync(
            new() { UserName = request.UserName },
            request.Password
        );

        if (result.Succeeded)
        {
            var userInDb = _context.Users.AsNoTracking().FirstOrDefault(u => u.UserName == request.UserName);
            if (userInDb is null)
                return BadRequest();

            var accessToken = _tokenService.CreateToken(userInDb);

            return Ok(new AuthResponse(userInDb.ToDto(), accessToken));
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(error.Code, error.Description);
        }

        return BadRequest(ModelState);
    }

    [HttpPost]
    [Route("login")]
    public async Task<ActionResult<AuthResponse>> AuthenticateAsync([FromBody] AuthRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var managedUser = await _userManager.FindByNameAsync(request.Username);
        if (managedUser == null)
        {
            return BadRequest();
        }

        var isPasswordValid = await _userManager.CheckPasswordAsync(managedUser, request.Password);
        if (!isPasswordValid)
        {
            return BadRequest();
        }

        var userInDb = _context.Users.AsNoTracking().FirstOrDefault(u => u.UserName == request.Username);
        if (userInDb is null)
            return Unauthorized();

        var accessToken = _tokenService.CreateToken(userInDb);

        return Ok(new AuthResponse(userInDb.ToDto(), accessToken));
    }
}