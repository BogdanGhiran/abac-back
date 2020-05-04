using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Common.ExceptionHandling;
using Common.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using XPAND.Captains.API.Models;
using XPAND.Captains.API.Repositories;
using XPAND.Captains.API.Services;
using XPAND.Captains.API.ViewModels;

namespace XPAND.Captains.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CaptainController : ControllerBase
    {
        private readonly ICaptainRepository _captainRepository;
        private readonly IShuttleService _shuttleService;
        private readonly IDataReplicationService _dataReplicationService;
        private readonly IConfiguration _config;

        public CaptainController(ICaptainRepository captainRepository, IShuttleService shuttleService, IDataReplicationService dataReplicationService, IConfiguration config)
        {
            _captainRepository = captainRepository;
            _shuttleService = shuttleService;
            _dataReplicationService = dataReplicationService;
            _config = config;
        }

        [HttpGet]
        public IActionResult DefaultPage()
        {
            return Content("Service is up and running");
        }
        
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] CaptainViewModel model)
        {
            if (await _captainRepository.CheckIfUsernameIsTaken(model.UserName))
            {
                throw new UserAlreadyExistsException();
            }

            Captain captainModel = new Captain
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Username = model.UserName,
                Password = PasswordHasher.Hash(model.Password),
                Identifier = Guid.NewGuid()
            };
            await _captainRepository.Create(captainModel);

            Shuttle shuttle = await _shuttleService.CreateShuttleWithCrew(model.ShuttleName, captainModel.Id.GetValueOrDefault());

            await _dataReplicationService.RegisterCaptainInPlanetMicroservice(captainModel, shuttle);

            return Ok();
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login(LoginViewModel loginModel)
        {
            if (string.IsNullOrEmpty(loginModel.UserName) || string.IsNullOrEmpty(loginModel.Password))
            {
                return BadRequest();
            }

            Captain captain = await _captainRepository.LoadByUsername(loginModel.UserName);

            if (captain is null)
            {
                return Unauthorized();
            }

            if (!PasswordHasher.ValidatePassword(loginModel.Password, captain.Password))
            {
                return Unauthorized();
            }

            string token = GenerateTokenString(captain);
            return Ok(new
            {
                Token = token,
                FirstName = captain.FirstName,
                LastName = captain.LastName,
                ID = captain.Identifier,
                Username = captain.Username
            });
        }

        private string GenerateTokenString(Captain captain)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(_config["JWTSecret"]);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, captain.Identifier.ToString()),
                    new Claim(ClaimTypes.Name, $"{captain.FirstName} {captain.LastName}"),
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}