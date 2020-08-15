using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using User.Application.Dtos;
using User.Application.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserAppService _userAppService;

        public UserController(IUserAppService userAppService)
        {
            _userAppService = userAppService ?? throw new ArgumentNullException(nameof(userAppService));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> InsertUserAsync(Guid id)
        {
            var user = await _userAppService.GetUserById(id);

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> InsertAsync(UserDto userDto)
        {
            var user = await _userAppService.InsertAsync(userDto);

            return Ok(user);
        }
    }
}
