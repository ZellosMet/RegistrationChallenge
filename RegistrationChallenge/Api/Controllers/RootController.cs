using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RegistrationChallenge.Api.Messages;

namespace RegistrationChallenge.Api.Controllers
{
    [Route("api")]
    [ApiController]
    public class RootController : ControllerBase
    {
        [HttpGet]
        public StringMessage Root()
        {
            return new StringMessage(Message: "server is running");
        }

        [HttpGet("ping")]
        public StringMessage Ping()
        {
            return new StringMessage(Message: "pong");
        }
    }
}
