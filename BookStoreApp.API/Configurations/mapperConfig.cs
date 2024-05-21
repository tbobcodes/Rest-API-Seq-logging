using AutoMapper;
using BookStoreApp.API.Models.Author;
using BookStoreApp.API.Data;

namespace BookStoreApp.API.Configurations
{
    public class mapperConfig : Profile
    {
        public mapperConfig() {
            CreateMap<AuthorCreateDTO, Author>().ReverseMap();
            CreateMap<AuthorUpdateDTO, Author>().ReverseMap();
            CreateMap<AuthorReadOnlyDTO, Author>().ReverseMap();

        }
    }
}
