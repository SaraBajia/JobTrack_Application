using Microsoft.AspNetCore.Mvc;using Microsoft.EntityFrameworkCore;using JobTrack.Api.Data;using JobTrack.Api.Models;using Microsoft.IdentityModel.Tokens;using System.IdentityModel.Tokens.Jwt;using System.Security.Claims;using System.Text;
namespace JobTrack.Api.Controllers;
public record RegisterRequest(string Name,string Email,string Password);
public record LoginRequest(string Email,string Password);
[ApiController][Route("api/auth")]
public class AuthController:ControllerBase{
 readonly JobTrackDbContext db;readonly IConfiguration c;public AuthController(JobTrackDbContext d,IConfiguration config){db=d;c=config;}
 [HttpPost("register")] public async Task<IActionResult> Register(RegisterRequest r){
  var email=r.Email.Trim().ToLowerInvariant(); if(string.IsNullOrWhiteSpace(r.Name)||string.IsNullOrWhiteSpace(email)||r.Password.Length<6)return BadRequest(new{message="Name, email and a password of at least 6 characters are required."});
  if(await db.Users.AnyAsync(x=>x.Email==email))return Conflict(new{message="An account with this email already exists."});
  var u=new User{Name=r.Name.Trim(),Email=email,PasswordHash=BCrypt.Net.BCrypt.HashPassword(r.Password)};db.Users.Add(u);await db.SaveChangesAsync();return Ok(CreateResponse(u));
 }
 [HttpPost("login")] public async Task<IActionResult> Login(LoginRequest r){var email=r.Email.Trim().ToLowerInvariant();var u=await db.Users.FirstOrDefaultAsync(x=>x.Email==email);if(u==null||!BCrypt.Net.BCrypt.Verify(r.Password,u.PasswordHash))return Unauthorized(new{message="Invalid email or password."});return Ok(CreateResponse(u));}
 object CreateResponse(User u){var claims=new[]{new Claim(ClaimTypes.NameIdentifier,u.Id.ToString()),new Claim(ClaimTypes.Name,u.Name),new Claim(ClaimTypes.Email,u.Email)};var key=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(c["Jwt:Key"]!));var creds=new SigningCredentials(key,SecurityAlgorithms.HmacSha256);var token=new JwtSecurityToken(c["Jwt:Issuer"],c["Jwt:Audience"],claims,expires:DateTime.UtcNow.AddDays(7),signingCredentials:creds);return new{token=new JwtSecurityTokenHandler().WriteToken(token),user=new{id=u.Id,u.Name,u.Email}};}
}
