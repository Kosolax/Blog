namespace Blog.Web.Controllers
{
    using Blog.Application.Models;
    using Blog.Application.Services;

    using CSharpFunctionalExtensions;

    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public class ArticlesController : ControllerBase
    {
        private readonly ArticleService _articleService;

        public ArticlesController(ArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateArticleDto createArticleDto)
        {
            Result<ArticleDto> res = await _articleService.Create(createArticleDto);
            if (res.IsFailure)
            {
                return BadRequest(res.Error);
            }

            return Ok(res.Value);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put(ArticleDto articleDto, int id)
        {
            Result<ArticleDto> res = await _articleService.Update(articleDto, id);
            if (res.IsFailure)
            {
                return BadRequest(res.Error);
            }

            return Ok(res.Value);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _articleService.Delete(id);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            Result<List<ArticleDto>> res = await _articleService.List();
            if (res.IsFailure)
            {
                return BadRequest(res.Error);
            }

            return Ok(res.Value);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Result<ArticleDto> res = await _articleService.Get(id);
            if (res.IsFailure)
            {
                return BadRequest(res.Error);
            }

            return Ok(res.Value);
        }
    }
}
