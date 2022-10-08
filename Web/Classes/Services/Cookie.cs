namespace BookmarksFront.Classes.Services
{
    using Microsoft.JSInterop;

    //SO https://stackoverflow.com/questions/54024644/how-do-i-create-a-cookie-client-side-using-blazor to get this code.
    //! don't forget to check comments, code has a bug on SO link:v:"this line has been modified" .
    //Namespace must be added in link:_Imports.razor and service must be added in link:Program.cs

    //see to use, add @inject ICookie cookie to your razor page.

    public interface ICookie
    {
        public Task SetValue(string key, string value, int? days = null);
        public Task<string> GetValue(string key, string def = "");
    }

    public class Cookie : ICookie
    {
        

        readonly IJSRuntime JSRuntime;
        string expires = "";

        public Cookie(IJSRuntime jsRuntime)
        {
            JSRuntime = jsRuntime;
            ExpireDays = 300;
        }

        public async Task SetValue(string key, string value, int? days = null)
        {
            var curExp = (days != null) ? (days > 0 ? DateToUTC(days.Value) : "") : expires;
            await SetCookie($"{key}={value}; expires={curExp}; path=/");
        }

        public async Task<string> GetValue(string key, string def = "")
        {
            var cValue = await GetCookie();
            if (string.IsNullOrEmpty(cValue)) return def;

            var vals = cValue.Split(';');
            foreach (var val in vals)
                if (!string.IsNullOrEmpty(val) && val.IndexOf('=') > 0)
                    if (val.Substring(0, val.IndexOf('=')).Trim().Equals(key, StringComparison.OrdinalIgnoreCase))//! this line has been modified to be like shown in comments
                        return val.Substring(val.IndexOf('=') + 1);
            return def;
        }

        private async Task SetCookie(string value)
        {
            await JSRuntime.InvokeVoidAsync("eval", $"document.cookie = \"{value}\"");
        }

        private async Task<string> GetCookie()
        {
            return await JSRuntime.InvokeAsync<string>("eval", $"document.cookie");
        }

        public int ExpireDays
        {
            set => expires = DateToUTC(value);
        }

        private static string DateToUTC(int days) => DateTime.Now.AddDays(days).ToUniversalTime().ToString("R");
    }
}

