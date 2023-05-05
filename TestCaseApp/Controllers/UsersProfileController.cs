using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestCaseApp.Dto;
using TestCaseApp.Mappers;

namespace TestCaseApp.Controllers;

public record UpdateUserProfileRequest (string Name, string Surname, [Required]string Phone, [Required]string Email);

[ApiController]
[Authorize]
[Route("[controller]")]
public class UsersProfileController : Controller
{
    private readonly UsersContext _context;

    public UsersProfileController(UsersContext context)
    {
        _context = context;
    }
    
    [HttpGet(Name = "GetUserProfile")]
    public async Task<ActionResult<TestCaseAppUserDto>> Get()
    {
        var userName = User.FindFirst(ClaimTypes.Name)?.Value;
        
        var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.UserName == userName);
        
        return Ok(user.ToDto());
    }
    
    [HttpPost(Name = "UpdateUserProfile")]
    public async Task<ActionResult<TestCaseAppUserDto>> PostAsync([FromBody]UpdateUserProfileRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var userName = User.FindFirst(ClaimTypes.Name)?.Value;
        
        var user = _context.Users.FirstOrDefault(u => u.UserName == userName);

        user.Name = request.Name;
        user.Surname = request.Surname;
        user.Phone = request.Phone;
        user.Email = request.Email;
        
        await _context.SaveChangesAsync();
        
        return Ok(user);
    }
}
