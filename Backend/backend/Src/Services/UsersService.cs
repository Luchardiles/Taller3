using backend.Src.DTO;
using backend.Src.Repositories.Interfaces;
using backend.Src.Services.Interfaces;

namespace backend.Src.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IMappingService _mappingService;

        public UsersService(IUsersRepository usersRepository, IMappingService mappingService)
        {
            _usersRepository = usersRepository;
            _mappingService = mappingService;
        }

        public async Task<UpdateUserDto> UpdateUser(UpdateUserDto updateUserDto, string rut)
        {
            var user = await _usersRepository.GetByRut(rut);
            if (user == null) throw new Exception("User not found");
            user = _mappingService.UpdateUserDtoToUser(user, updateUserDto);
            var updatedUser = await _usersRepository.Update(user);
            return _mappingService.CreateUpdateUserDto(updatedUser);
        }


        /// <summary>
        /// Método para obtener un usuario por su email.
        /// </summary>
        /// <param name="email">
        /// - email: Email del usuario a obtener.
        /// </param>
        public async Task<GetUserDto> GetUserByRut(string rut)
        {
            var user = await _usersRepository.GetByRut(rut);
            if (user == null) throw new Exception("User not found");
            var mappedDto = _mappingService.MapUserToGetUserDto(user);
            return mappedDto;
        }

        public async Task<GetUserDto> GetUserByEmail(string email)
        {
            var user = await _usersRepository.GetByEmail(email);
            if (user == null) throw new Exception("User not found");
            var mappedDto = _mappingService.MapUserToGetUserDto(user);
            return mappedDto;
        }

        public async Task<bool> CheckEmail(string email)
        {
            var user = await _usersRepository.GetByEmail(email);
            if (user == null) return false;
            return true;
        }
    }
}