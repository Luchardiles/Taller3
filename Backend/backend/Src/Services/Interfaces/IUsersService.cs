using backend.Src.DTO;

namespace backend.Src.Services.Interfaces
{
    public interface IUsersService
    {
        public Task<UpdateUserDto> UpdateUser(UpdateUserDto updateUserDto, string rut);

        public Task<GetUserDto> GetUserByEmail(string email);

        public Task<GetUserDto> GetUserByRut(string rut);

        public Task<bool> CheckEmail(string email);

    }
}