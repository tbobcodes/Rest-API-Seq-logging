using Blazored.LocalStorage;
using BookStoreApp.Blazor.ServerUI.Services.Base;
using Microsoft.AspNetCore.Authorization;

namespace BookStoreApp.Blazor.ServerUI.Services
{
    public class AuthorService : BaseHttpService, IAuthorService
    {
        private readonly IClient client;

        public AuthorService(IClient client, ILocalStorageService localStorage) : base(client, localStorage)
        {
            this.client = client;
        }

        public async Task<Response<List<AuthorReadOnlyDTO>>> GetAllAuthors()
        {
            Response<List<AuthorReadOnlyDTO>> response;

            try
            {
                await GetBearerToken();
                var data = await client.AuthorsAllAsync();
                response = new Response<List<AuthorReadOnlyDTO>>
                {
                    Data = data.ToList(),
                    Success = true
                };
            }
            catch (ApiException exception)
            {
                response = ConvertApiExceptions<List<AuthorReadOnlyDTO>>(exception);

            }
            return response;
        }

        private Response<T> ConvertApiExceptions<T>(ApiException exception)
        {
            throw new NotImplementedException();
        }
    }
}

