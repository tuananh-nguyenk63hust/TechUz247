using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechUZ247.Models;


namespace TechUZ247.Controllers
{
    [ApiController]
    [Route("api")]
    //[EnableCors(origins:"*", headers:"*", methods:"*")]
    public class LoginController : Controller
    {
        
        private readonly ILogger<LoginController> _logger;
        private IServices services;

        public LoginController(ILogger<LoginController> logger, IServices services)
        {
            _logger = logger;
            this.services = services;
        }

        [HttpPost("login")]
        public async Task<LoginReponsiveModel> LoginUser([FromBody] LoginQueryModel loginQueryModel)
        {
            var result = await services.LogIn(loginQueryModel,false);

            return result;
        }
        [HttpPost("signup")]
        public async Task<LoginReponsiveModel> SignUpUser([FromBody] SignUpQueryModel signUpQueryModel)
        {
            var result = await services.SignUp(signUpQueryModel);
            return result;
        }
        [HttpGet("ValidateUser")]
        public async Task<bool> ValidateUser()
        {
            return true;
        }
        [HttpPost("SignApp")]
        public async Task<SignUpQueryAppModel> SignApp([FromBody] SignUpRequestModel signUpRequestModel)
        {
            var result = await services.SignUpApp(signUpRequestModel);
            return result;
        }
        [HttpPost("GetAllMember")]
        public async Task<List<GetAllMember>> GetMembers([FromBody] PageSize pageSize)
        {
            var result = await services.GetAllMember();
            return result;
        }
        [HttpPut("EditMember")]
        public async Task<bool> EditMember([FromBody] EditNumberAccount editNumberAccount)
        {
            //var result = true;
            var result = await services.EditMember(editNumberAccount);
            return result;
        }
        [HttpPost("LoginForAdmin")]
        public async Task<LoginReponsiveModel> LoginAdmin([FromBody] LoginQueryModel loginQueryModel)
        {
            var result = await services.LogIn(loginQueryModel, true);

            return result;
        }
    }
}
