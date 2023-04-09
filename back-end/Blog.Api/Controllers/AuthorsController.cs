using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Blog.Data;
using Blog.Data.Entites;

namespace Blog.Controllers
{
    [ApiController]
    [Route("api/authors")]
    public class AuthorsController : ControllerBase
    {
        private readonly BlogContext _dbContext;

        public AuthorsController(BlogContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAuthors()
        {
            var authors = await _dbContext.Authors.ToListAsync();

            if (authors == null || authors.Count == 0)
            {
                return NotFound("No authors found");
            }

            return Ok(authors);
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(AuthorDto authorDto)
        {
            if (await _dbContext.Authors.AnyAsync(a => a.Email == authorDto.Email))
            {
                return BadRequest("Email already exists");
            }

            var author = new Author
            {
                FirstName = authorDto.FirstName,
                LastName = authorDto.LastName,
                Email = authorDto.Email,
                Password = authorDto.Password
            };

            await _dbContext.Authors.AddAsync(author);
            await _dbContext.SaveChangesAsync();

            return Ok("Registration successful");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var author = await _dbContext.Authors.SingleOrDefaultAsync(a => a.Email == loginDto.Email && a.Password == loginDto.Password);

            if (author == null)
            {
                return BadRequest("Invalid email or password");
            }

            return Ok("Login successful");
        }
    }

    public class AuthorDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
