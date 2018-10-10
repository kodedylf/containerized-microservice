using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Google.Apis.Auth;

namespace postgres_service.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly ILogger _logger;
        public AuthController(ILogger<AuthController> logger)
        {
            _logger = logger;
        }
        
        [Route("api/[controller]/google/{token}")]
        [HttpGet]
        public async Task<string> LoginWithGoogleToken(string token)
        {
            _logger.LogInformation(token);
            var payload = await GoogleJsonWebSignature.ValidateAsync(token);
            return "j.w.t";
        }
    }
}
