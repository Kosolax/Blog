namespace Blog.Application.Models
{
    using AutoMapper;

    using Blog.Application.Mapping;
    using Blog.Domain.Entities;

    public class CreateArticleDto : IMapFrom<Article>
    {
        public string? Content { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Article, CreateArticleDto>().ReverseMap();
        }
    }
}