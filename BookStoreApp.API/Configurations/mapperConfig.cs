using AutoMapper;
using BookStoreApp.API.Models.Author;
using BookStoreApp.API.Data;
using BookStoreApp.API.Models.Book;

namespace BookStoreApp.API.Configurations
{
    public class mapperConfig : Profile
    {
        public mapperConfig() {
            CreateMap<AuthorCreateDTO, Author>().ReverseMap();
            CreateMap<AuthorUpdateDTO, Author>().ReverseMap();
            CreateMap<AuthorReadOnlyDTO, Author>().ReverseMap();

            CreateMap<BookCreateDto, Book>().ReverseMap();
            CreateMap<BookUpdateDto, Book>().ReverseMap();
            CreateMap<Book, BookReadOnlyDto>()
                .ForMember(q => q.AuthorName, d => d.MapFrom(map => $"{map.Author.FirstName}"))
                .ReverseMap();
            CreateMap<Book, BookDetailsDto>()
                 .ForMember(q => q.AuthorName, d => d.MapFrom(map => $"{map.Author.FirstName}"))
                 .ReverseMap();


        }
    }
}
