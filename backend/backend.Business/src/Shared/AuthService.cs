using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using backend.Business.src.Abstractions;
using backend.Business.src.Dtos;
using backend.Domain.src.Abstractions;
using backend.Domain.src.Entities;
using AutoMapper;

namespace backend.Business.src.Shared
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepo _userRepo;
        protected readonly IMapper _mapper;

        public AuthService(IUserRepo userRepo,IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }

        public async Task<string> VerifyCredentials(UserCredentialsDto credentials)
        {
            var foundUserByEmail = await _userRepo.FindOneByEmail(credentials.Email) ?? throw ServiceException.NotFoundException("Email not found");
            var isAuthenticated = PasswordService.VerifyPassword(credentials.Password, foundUserByEmail.Password, foundUserByEmail.Salt);
            if(!isAuthenticated)
            {
                throw ServiceException.UnAuthenticatedException();
            }
            return GenerateToken(foundUserByEmail);
        }

        private string GenerateToken(User user)
        {
            var claims = new List<Claim>{
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("GRVm0^v3uFIWsJuW51hanwMsocR40U2@l6%(jocj0sH8vnX^1G"));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var securityTokenDescriptor = new SecurityTokenDescriptor {
                Issuer = "anhnguyen-ecommerce-backend",
                Expires = DateTime.Now.AddMinutes(10),
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = signingCredentials
            };
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            return jwtSecurityTokenHandler.WriteToken(token).ToString();
        }

        public async Task<UserReadDto> GetUserFromToken(Token tokenAuth)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("GRVm0^v3uFIWsJuW51hanwMsocR40U2@l6%(jocj0sH8vnX^1G")),
                ValidateIssuer = true,
                ValidIssuer = "anhnguyen-ecommerce-backend",
                ValidateAudience = false, 
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                ClaimsPrincipal claimsPrincipal = tokenHandler.ValidateToken(tokenAuth.token, tokenValidationParameters, out SecurityToken validatedToken);
                Claim userIdClaim = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier);

                if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out Guid userId))
                {
                    // Load user from the database using userId and return
                    // Assuming you have access to your repository/service to load the user
                    User user = await _userRepo.GetOneById(userId);
                    return _mapper.Map<UserReadDto>( await _userRepo.GetOneById(userId));
                }
            }
            catch (Exception)
            {
                throw new CustomException();
            }

            return null;
        }
    }
}