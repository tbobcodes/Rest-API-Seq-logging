using Blazored.LocalStorage;
using NJsonSchema.Validation;

namespace BookStoreApp.Blazor.ServerUI.Services.Base
{
    public class BaseHttpService
    {
        private readonly IClient client;
        private readonly ILocalStorageService localStorage;

        public BaseHttpService(IClient client, ILocalStorageService localStorage)
        {
            this.client = client;
            this.localStorage = localStorage;

        }

        protected Response<Guid> ConvertApiExceptions(ApiException apiException)
        {
            if (apiException.StatusCode == 400)
            {
                return new Response<Guid>()
                {
                    Message = "Validation errors have occurred.",
                    ValidationErrors = apiException.Response,
                    Success = false
                };
            }
            else if (apiException.StatusCode == 404)
            {
                return new Response<Guid>()
                {
                    Message = "Resource not found.",
                    Success = false
                };
            }
            else
            {
                // Handle other status codes if needed
                // For now, return a default response
                return new Response<Guid>()
                {
                    Message = "An error occurred.",
                    Success = false
                };
            }
        }

        protected async Task GetBearerToken()
        {
            var token = await localStorage.GetItemAsync<string>("accessToken");
            if (token != null)
            {
                client.HttpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
            }
        }

    }
}
