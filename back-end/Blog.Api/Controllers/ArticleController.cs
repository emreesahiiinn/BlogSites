using Blog.Data;
using Blog.Data.Entites;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticleController : ControllerBase
    {
        private readonly BlogContext _dbContext;

        public ArticleController(BlogContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var articles = _dbContext.Articles.ToList();
            return Ok(articles);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var article = _dbContext.Articles.FirstOrDefault(a => a.Id == id);
            if (article == null)
            {
                return NotFound();
            }
            return Ok(article);
        }

        [HttpPost]
        public IActionResult Create(Article article)
        {
            _dbContext.Articles.Add(article);
            _dbContext.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = article.Id }, article);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Article updatedArticle)
        {
            var article = _dbContext.Articles.FirstOrDefault(a => a.Id == id);
            if (article == null)
            {
                return NotFound();
            }

            article.Title = updatedArticle.Title;
            article.Content = updatedArticle.Content;
            article.CategoryId = updatedArticle.CategoryId;
            article.AuthorId = updatedArticle.AuthorId;
                
            _dbContext.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var article = _dbContext.Articles.FirstOrDefault(a => a.Id == id);
            if (article == null)
            {
                return NotFound();
            }

            _dbContext.Articles.Remove(article);
            _dbContext.SaveChanges();

            return NoContent();
        }
    }
}
