﻿using AppServer.Data;
using AppServer.Models.Domains;
using AppServer.Models.DTOs;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AppServer.Controllers
{
    [Route("user/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthController : Controller
    {
        private readonly AppServerDbContext dbContext;
        private readonly IConfiguration _configuration;
        public AuthController(AppServerDbContext dbContext, IConfiguration configuration)
        {
            this.dbContext = dbContext;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllUsers()
        {
            var users = await dbContext.Users.ToListAsync();

            return Ok(users);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<ActionResult> GetUser(Guid id)
        {
            var user = await dbContext.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(user);
            }
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult> Register([FromBody] UserDTO newUser)
        {
            var existingUser = await dbContext.Users.FirstOrDefaultAsync(u => u.Name == newUser.Name);
            if (existingUser != null)
            { 
                return BadRequest("User with this name already exists.");
            }
            var user = await dbContext.Users.FindAsync(newUser.Id);
            if (user != null)
            {
                return BadRequest("User with this name already exists.");
            }
            string hashedPassword = HashOriginPassword(newUser.Password);
            var userDTO = new User
            {
                Id = newUser.Id,
                Name = newUser.Name,
                Password = hashedPassword
            };

            dbContext.Users.Add(userDTO);
            await dbContext.SaveChangesAsync();

            string token = CreateToken(newUser);
            return Ok(token);
        }
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login([FromBody] UserDTO user)
        {
            var existedUser = await dbContext.Users.FindAsync(user.Id);
            if (existedUser == null)
            {
                return BadRequest("User doesn't exist");
            }
            string existedUserPassword = HashOriginPassword(user.Password);
            if (existedUser.Password != existedUserPassword)
            {
                return BadRequest("Incorrect password");
            }
            string token = CreateToken(user);
            return Ok(token);
        }

        [HttpPost("logout")]
        public async Task<ActionResult> Logout([FromBody] UserDTO newUser)
        {
            return Ok("Logged out successfully");
        }
        private string HashOriginPassword(string originalPassword)
        {
            string hashedPassword;
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(originalPassword));
                StringBuilder builder = new StringBuilder();

                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                hashedPassword = builder.ToString();
            }
            return hashedPassword;
        }
        private string CreateToken(UserDTO user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name)
            };
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
              _configuration["Jwt:Issuer"],
              claims: claims,
              expires: DateTime.Now.AddMinutes(180),
              signingCredentials: credentials);
            string jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}
