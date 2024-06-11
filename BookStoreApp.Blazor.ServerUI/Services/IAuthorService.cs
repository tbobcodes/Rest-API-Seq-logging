using BookStoreApp.Blazor.ServerUI.Services.Base;

namespace BookStoreApp.Blazor.ServerUI.Services
{
    public interface IAuthorService
    {
        Task<Response<List<AuthorReadOnlyDTO>>> GetAllAuthors();
    }
}
