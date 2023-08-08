﻿using AutoMapper;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using webapi.Data;
using webapi.Identity;
using webapi.Models;
using webapi.Models.DTO;
using webapi.Models.Entities;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepo _repository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepo repository, IMapper mapper, IConfiguration configuration)
        {
            _repository = repository;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _repository.GetAllUsers();

            return Ok(_mapper.Map<IEnumerable<UserReadDto>>(users));
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> LoginUser(UserLoginDto userLoginDto)
        {
            var existingUser = await _repository.GetUserByEmail(userLoginDto.Email);

            if (existingUser == null)
            {
                return NotFound();
            }

            if (existingUser.Password != userLoginDto.Password)
            {
                return BadRequest("Wrong password");
            }

            var secretToken = _configuration.GetSection("JwtSettings:Key").ToString();
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretToken));

            var issuer = _configuration.GetSection("JwtSettings:Issuer").ToString();
            var audience = _configuration.GetSection("JwtSettings:Audience").ToString();

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(IdentityData.SuperUserClaimName, existingUser.IsSuperUser.ToString(), ClaimValueTypes.Boolean),
                    new Claim(IdentityData.EmailClaimName, existingUser.Email, ClaimValueTypes.String),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            //return tokenHandler.WriteToken(token);
            var data = new { accessToken = tokenHandler.WriteToken(token) };
            return Ok(data);
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> RegisterUser(UserRegisterDto userRegisterDto)
        {
            var existingUser = await _repository.GetUserByEmail(userRegisterDto.Email);

            if (existingUser != null)
            {
                return BadRequest("User with this email already exist");
            }

            var userModel = _mapper.Map<User>(userRegisterDto);

            await _repository.CreateUser(userModel);
            await _repository.SaveChanges();

            var userReadDto = _mapper.Map<UserReadDto>(userModel);
            return CreatedAtAction(nameof(RegisterUser), new { id = userReadDto.Id }, userReadDto);
        }
    }
}