using backend.Src.Services.Interfaces;
using backend.Src.Repositories.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using backend.Src.DTO;
using backend.Src.Util;
using DotNetEnv;
namespace backend.Src.Services
{
    public class AuthService : IAuthService
    {
        private readonly string _jwtSecret;
        private readonly IUsersRepository _usersRepository;
        private readonly IMappingService _mappingService;
        public AuthService(IConfiguration config, IUsersRepository usersRepository, IMappingService mappingService)
        {
            Env.Load();
            var token = Env.GetString("SECRET_KEY") ?? throw new ArgumentNullException("SECRET_KEY is null");
            _jwtSecret = token;
            _usersRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
            _mappingService = mappingService ?? throw new ArgumentNullException(nameof(mappingService));
        }

        public string? GenerateToken(string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSecret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]{
                    new Claim(ClaimTypes.NameIdentifier, email),

                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<bool> CheckCredentials(LoginUserDto loginUserDto)
        {
            if (string.IsNullOrEmpty(loginUserDto.Email) || string.IsNullOrEmpty(loginUserDto.Password))
            {
                return false;
            }

            var user = await _usersRepository.GetByEmail(loginUserDto.Email);
            if (user == null) return false;

            return BCryptHelper.CheckPassword(loginUserDto.Password, user.Password);
        }

        public async Task<CreateUserDto?> Register(CreateUserDto createUserDto)
        {
            var rut = createUserDto.Rut.Replace(".", "").Replace("-", "");
            var password = rut.Substring(0, rut.Length - 1);
            createUserDto.Password = BCrypt.Net.BCrypt.HashPassword(password);

            var user = _mappingService.CreateClientDtoToUser(createUserDto);
            var createdUser = await _usersRepository.Add(user);
            var mappedDto = _mappingService.MapUserToCreateUserDto(createdUser);
            return mappedDto;
        }

        public async Task<GetUserDto?> UpdatePassword(UpdatePasswordDto updatePasswordDto, GetUserDto userDto)
        {
            var user = await _usersRepository.GetByEmail(userDto.Email);
            if (user == null) return null;
            var password = BCrypt.Net.BCrypt.HashPassword(updatePasswordDto.NewPassword);
            user.Password = password;
            var updatedUser = await _usersRepository.Update(user);
            var mappedDto = _mappingService.MapUserToGetUserDto(updatedUser);
            return mappedDto;
        }

        public async Task<GetUserDto?> GetUserByRut(string rut)
        {
            var user = await _usersRepository.GetByRut(rut);
            if (user == null) return null;
            var mappedDto = _mappingService.MapUserToGetUserDto(user);
            return mappedDto;
        }

        public async Task<GetUserDto?> GetUserByEmail(string email)
        {
            var user = await _usersRepository.GetByEmail(email);
            if (user == null) return null;
            var mappedDto = _mappingService.MapUserToGetUserDto(user);
            return mappedDto;
        }
    }
}