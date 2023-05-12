using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShareYouTubeAPI.IRepository;
using ShareYouTubeAPI.Models;
using ShareYouTubeAPI.Models.DTO;

namespace ShareYouTubeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var test = _userRepository.GetAll().ProjectTo<AppUserDTO>(_mapper.ConfigurationProvider).ToList();
            return Ok(test.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var user = _userRepository.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        [HttpPost]

        public IActionResult Add(AppUser user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _userRepository.Add(user);
            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }




    }
}
