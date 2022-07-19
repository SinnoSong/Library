using System.Net.Http.Headers;
using Blazored.LocalStorage;
using Library.Web.Models;
using Library.Web.Services.Interface;

namespace Library.Web.Services
{
    public class BaseHttpService
    {
        #region field
        private readonly IClient client;
        private readonly ILocalStorageService localStorage;
        #endregion

        #region ctor
        public BaseHttpService(IClient client, ILocalStorageService localStorage)
        {
            this.client = client;
            this.localStorage = localStorage;
        }
        #endregion

        protected Response<Guid> ConvertApiException<Guid>(ApiException apiException)
        {
            if (apiException.StatusCode == 400)
            {
                return new Response<Guid>() { Message = "Validation errors have occured.", ValidationErrors = apiException.Response, Success = false };
            }
            if (apiException.StatusCode == 404)
            {
                return new Response<Guid>() { Message = "The requested item could not be found.", Success = false };
            }
            if (apiException.StatusCode == 401)
            {
                return new Response<Guid>() { Message = "Invalid Credentials, Please Try Again", Success = false };
            }

            if (apiException.StatusCode >= 200 && apiException.StatusCode <= 299)
            {
                return new Response<Guid>() { Message = "Operation Reported Success", Success = true };
            }

            return new Response<Guid>() { Message = "Something went wrong, please try again.", Success = false };
        }

        protected async Task GetBearerToken()
        {
            var token = await localStorage.GetItemAsync<string>("accessToken");
            if (token != null)
            {
                client.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }
    }
}
