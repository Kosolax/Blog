namespace Blog.Application.Services
{
    using AutoMapper;

    using Blog.Application.Models;
    using Blog.Domain.Entities;
    using Blog.Infrastructure.Repositories;

    using CSharpFunctionalExtensions;

    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ArticleService
    {
        private readonly IMapper _mapper;

        private readonly ArticleRepository _repository;

        public ArticleService(ArticleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<ArticleDto>> Create(CreateArticleDto itemToCreate)
        {
            Article article = _mapper.Map<Article>(itemToCreate);
            if (article == null)
            {
                return Result.Failure<ArticleDto>("Could not map dto into entity");
            }

            if (string.IsNullOrEmpty(article.Content))
            {
                return Result.Failure<ArticleDto>("Content is required");
            }

            Article createdArticle = await _repository.Create(article);
            if (createdArticle == null)
            {
                return Result.Failure<ArticleDto>("Could not create the article");
            }

            ArticleDto articleDto = _mapper.Map<ArticleDto>(createdArticle);
            if (articleDto == null)
            {
                return Result.Failure<ArticleDto>("Could not map the new article from entity");
            }

            return Result.Success(articleDto);
        }

        public async Task Delete(int id)
        {
            await _repository.Delete(id);
            Result.Success();
        }

        public async Task<Result<ArticleDto>> Get(int id)
        {
            Article article = await _repository.Get(id);
            if (article == null)
            {
                return Result.Failure<ArticleDto>("Could not fetch entity");
            }

            ArticleDto articleDto = _mapper.Map<ArticleDto>(article);
            if (articleDto == null)
            {
                return Result.Failure<ArticleDto>("Could not map entity");
            }

            return Result.Success(articleDto);
        }

        public async Task<Result<List<ArticleDto>>> List()
        {
            List<Article> articles = await _repository.List();
            if (articles == null)
            {
                return Result.Failure<List<ArticleDto>>("Could not fetch entitites");
            }

            List<ArticleDto> articleDtos = _mapper.Map<List<ArticleDto>>(articles);
            if (articleDtos == null)
            {
                return Result.Failure<List<ArticleDto>>("Could not map entitites into dtos");
            }

            return Result.Success(articleDtos);
        }

        public async Task<Result<ArticleDto>> Update(ArticleDto itemToUpdate, int id)
        {
            Article article = _mapper.Map<Article>(itemToUpdate);
            if (article == null)
            {
                return Result.Failure<ArticleDto>("Could not map dto into entity");
            }

            if (article.Id != id)
            {
                return Result.Failure<ArticleDto>("You are trying to update an other object or to modify an id");
            }

            Article updatedArticle = await _repository.Update(article, id);
            if (updatedArticle == null)
            {
                return Result.Failure<ArticleDto>("Could not update the article");
            }

            ArticleDto articleDto = _mapper.Map<ArticleDto>(updatedArticle);
            if (articleDto == null)
            {
                return Result.Failure<ArticleDto>("Could not map the new article from entity");
            }

            return Result.Success(articleDto);
        }
    }
}