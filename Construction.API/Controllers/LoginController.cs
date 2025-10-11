using Asp.Versioning;
using Buisnes_Logic.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Construction.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> logger;
        private readonly ILoginService login;

        public LoginController(
            ILogger<LoginController> logger,
            ILoginService login
            )
        {
            this.logger = logger;
            this.login = login;
        }

        /// <summary>
        /// This Endpoint Login to API
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost("auth")]
        public ActionResult Authenticate(string userName, string password)
        {
            try
            {
                var secuerityToken = login.Login(userName, password);
                return Ok(secuerityToken);
            } catch(Exception ex)
            {
                logger.LogDebug(ex.Message);
                return Unauthorized();
            }
        }
    }
}
