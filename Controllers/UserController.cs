using Microsoft.AspNetCore.Mvc;
using ECommerceAPI.Models;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using ECommerceAPI.Data;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    [HttpPost("register")]
    public ActionResult<User> Register(User user)
    {
        if (InMemoryData.Users.Any(u => u.Username == user.Username))
        {
            return Conflict("Username already exists.");
        }

        InMemoryData.Users.Add(user);

        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    }

    [HttpPost("login")]
    public ActionResult Login(User user)
    {
        var dbUser = InMemoryData.Users
            .SingleOrDefault(u => u.Username == user.Username && u.Password == user.Password);

        if (dbUser == null)
        {
            return Unauthorized();
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes("your_secret_key_here");
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", dbUser.Id.ToString()) }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        return Ok(new { Token = tokenString });
    }

    [HttpGet("{id}")]
    public ActionResult<User> GetUser(int id)
    {
        var user = InMemoryData.Users.FirstOrDefault(u => u.Id == id);

        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }
}
