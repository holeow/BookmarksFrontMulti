
using BookmarksFront.Classes.Services;

using Microsoft.AspNetCore.Components;

using System.Runtime.CompilerServices;

namespace BookmarksFront.Classes
{
    //see link:Pages/SetKey.razor to see adding of the key.
    //see link:Classes/Services/Cookie.cs to see implementation of the cookie service.
    public static class APIKey
    {

         static string key = null;
        public static async Task<string> GetKey(ICookie cookie)
        {
            if (key == null) key = await cookie.GetValue("APIKey", null);
            return key;
        }

        public static async Task SetKey(ICookie cookie, string key)
        {
            APIKey.key = key;
            await cookie.SetValue("APIKey", key);
        }

    }
}
