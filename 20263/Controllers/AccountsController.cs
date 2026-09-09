using _20263.DTOs.Identity;
using _20263.Model;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace _20263
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IMapper mapper;

        //todo
        public AccountsController(UserManager<ApplicationUser> userManager, IConfiguration configuration, SignInManager<ApplicationUser> signInManager, IMapper mapper) {
            this.userManager = userManager;
            this.configuration = configuration;
            this.signInManager = signInManager;
            this.mapper = mapper;
        }
        [HttpPost("register")]
        public async Task<ActionResult<AuthenticationResponseDTO>> Register(UserCredentialsDTO userCredentialsDTO)
        {
            var usuario = mapper.Map<ApplicationUser>(userCredentialsDTO);
            var resultado = await userManager.CreateAsync(usuario, userCredentialsDTO.Password);
            if (resultado.Succeeded)
            {
                return await BuildToken(userCredentialsDTO.Email);
            }
            return BadRequest(resultado.Errors);
        }
        private async Task<AuthenticationResponseDTO> BuildToken(string email)
        {
            var claims = new List<Claim>
            {
                new Claim("email",email),
                new Claim(ClaimTypes.Email, email)
            };
            var usuario = await userManager.FindByEmailAsync(email);
            var claimsRoles = await userManager.GetClaimsAsync(usuario);
            var usuarioId = usuario.Id;
            var roles = await userManager.GetRolesAsync(usuario);
            foreach (var rol in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, rol));
            }
            claims.AddRange(claimsRoles);

            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["llaveJWT"]!));
            var cred = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);
            var expiracion = DateTime.UtcNow.AddDays(30);

            var securityToken = new JwtSecurityToken(issuer: null, audience: null, claims: claims, expires: expiracion, signingCredentials: cred);//investigar sobre los paramtreos 
            return new AuthenticationResponseDTO
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                expiration = expiracion,
                UserId = usuarioId
            };
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<AuthenticationResponseDTO>> Renew()
        {
            var emailClaim = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "email");
            return await BuildToken(emailClaim!.Value);
        }
        [HttpPost("login")]
        public async Task<ActionResult<AuthenticationResponseDTO>> Login(UserCredentialsDTO userCredentialsDTO)
        {
            var resultado = await signInManager.PasswordSignInAsync(userCredentialsDTO.Email, userCredentialsDTO.Password, isPersistent: false, lockoutOnFailure: false);

            if (resultado.Succeeded)
            {
                return await BuildToken(userCredentialsDTO.Email);
            }
            else 
            {
                return BadRequest("login incorrecto");
            }
        }
    }
}
