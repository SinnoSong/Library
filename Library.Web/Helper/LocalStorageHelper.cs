using Blazored.LocalStorage;

namespace Library.Web.Helper
{
    public static class LocalStorageHelper
    {
        public static async Task<string?> GetAccessTokenAsync(this ILocalStorageService localStorageService)
        {
            var accessToken = await localStorageService.GetItemAsync<string>("accessToken");
            if (accessToken != null)
            {
                await localStorageService.SetItemAsync("accessToken", accessToken);
            }
            return accessToken;
        }
    }
}
