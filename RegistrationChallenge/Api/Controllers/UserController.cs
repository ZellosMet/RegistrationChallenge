using Microsoft.AspNetCore.Mvc;
using RegistrationChallenge.Api.Messages;
using RegistrationChallenge.Model.DataBase;
using RegistrationChallenge.Model.Exceptions;
using RegistrationChallenge.Model.Users;

namespace RegistrationChallenge.Api.Controllers
{
    [Route("/api/user")] 
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _users;

        public UserController(UserService users)
        {
            _users = users;
        }
        //Запрос на регистрацию пользователя
        [HttpPost]
        public async Task<IActionResult> RegisterAsync(UserRegistrationMessage registrationMessage)
        {
            try
            {
                await _users.RegisterAsync(registrationMessage.Login, registrationMessage.Email);
                // 200
                return Ok(new StringMessage(Message: $"The letter has been sent, check your mail {registrationMessage.Email}"));
            }
            catch (UserFormatException ex)
            {
                // 400
                ErrorMessage error = new ErrorMessage(Type: ex.GetType().Name, Message: ex.Message);
                return BadRequest(error);
            }
            catch (DuplicatedDataException ex)
            {
                // 409
                ErrorMessage error = new ErrorMessage(Type: ex.GetType().Name, Message: ex.Message);
                return Conflict(error);
            }
        }
        //Запрос на получение пользователя
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromHeader(Name = "X-Api-Key")] string apiKey)
        {
            try
            {
                DBUser user = await _users.GetAsync(apiKey);
                UserInfoMessage usereInfo = new UserInfoMessage(
                    UUID: user.UUID,
                    Login: user.Login,
                    Email: user.Email,
                    Confirm: user.ConfirmEmail
                );
                return Ok(usereInfo);
            }
            catch (UserNotFoundException ex)
            {
                // 404
                ErrorMessage error = new ErrorMessage(Type: ex.GetType().Name, Message: ex.Message);
                return NotFound(error);
            }
        }
        //Запрос на подтверждение почты пользователя
        [HttpGet("{id}")]
        public async Task<IActionResult> ConfirEmailAsync(string id)
        {
            try
            {
                DBUser user = await _users.ConfirmEmailAsync(id);
                UserInfoMessage usereInfo = new UserInfoMessage(
                    UUID: user.UUID,
                    Login: user.Login,
                    Email: user.Email,
                    Confirm: user.ConfirmEmail
                );
                return Ok(usereInfo);
            }
            catch (UserNotFoundException ex)
            {
                // 404
                ErrorMessage error = new ErrorMessage(Type: ex.GetType().Name, Message: ex.Message);
                return NotFound(error);
            }
        }
    }
}
