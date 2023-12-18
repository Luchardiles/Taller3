using backend.Src.DTO;
using Microsoft.AspNetCore.Mvc;
using backend.Src.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
namespace backend.Src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        /// <summary>
        /// Método para obtener un usuario por su email.
        /// </summary>
        /// <param name="email">
        /// - email: Email del usuario a obtener.
        /// </param>
        /// <returns>
        /// Retorna a un usuario con sus datos.
        /// Si el usuario no existe, retorna un BadRequest con el mensaje "User not found".
        /// </returns>
         /// Si ocurre un error, retorna un InternalServerError, con el mensaje "Internal Server Error".
        [Authorize]
        [HttpGet("{email}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GetUserDto>> GetUser(string email)
        {
            try
            {
                var user = await _usersService.GetUserByEmail(email);
                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Método para actualizar un usuario.
        /// </summary>
        /// <param name="updateUserDto">
        /// - updateUserDto: Objeto con los datos del usuario a actualizar.
        /// </param>
        /// <returns>
        /// Retorna a un usuario con sus datos actualizados.
        /// Si el usuario no existe, retorna un BadRequest, con el mensaje "User not found".
        /// Si ocurre un error, retorna un InternalServerError, con el mensaje "Internal Server Error".
        [Authorize]
        [HttpPut("{rut}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UpdateUserDto>> UpdateUser(UpdateUserDto updateUserDto, string rut)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _usersService.GetUserByRut(rut);
            if (user == null)
            {
                return BadRequest("User not found");
            }

            var existsEmail = await _usersService.CheckEmail(updateUserDto.Email);
            if (existsEmail && user.Email != updateUserDto.Email)
            {
                return BadRequest("Email already exists");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return await _usersService.UpdateUser(updateUserDto, rut);
        }

    }
}