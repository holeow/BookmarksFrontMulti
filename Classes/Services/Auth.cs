using Microsoft.AspNetCore.Components;

namespace BookmarksFront.Classes.Services
{
    public class Auth
    {

        public static Auth instance;

        public ICookie cookie;
        public NavigationManager manager;
        public TempData tempdata;
        public WebUser user;

        public Auth(ICookie cookie, NavigationManager manager, TempData tempdata)
        {
            this.cookie = cookie;
            this.manager = manager;
            this.tempdata = tempdata;
            instance = this;
        }


        public async Task<bool> KeyAuthChosen()
        {
            return await cookie.GetValue("AuthChosen", "APITOKEN") == "APIKEY";
        }

        public async Task SetChoice(string type)
        {
            await cookie.SetValue("AuthChosen", type);
        }

        public async Task SetApiToken(string token)
        {
            await cookie.SetValue("apiToken", token);
            await SetChoice("APITOKEN");
            user = null;
        }

        public async Task SetApiKey(string key)
        {
            await cookie.SetValue("apiKey", key);
            await SetChoice("APIKEY");
            user = null;
        }

        public async Task<(string authType, string code)> GetAuth()
        {
            return await KeyAuthChosen()
                ? ("APIKEY", await cookie.GetValue("apiKey"))
                : ("APITOKEN", await cookie.GetValue("apiToken"));
        }

        public async Task ResetConnection()
        {
            if (await KeyAuthChosen()) await SetApiKey("");
            else await SetApiToken("");

            tempdata["afterConnectReturn"] = manager.Uri;
            manager.NavigateTo("/setkey");
        }

        public async Task<WebUser> GetWebUser(IBookmarkService service)
        {
            var auth = await GetAuth();
            if (string.IsNullOrWhiteSpace(auth.code))
            {
                return null;
            }
            else if (user == null)
            {
                var res = await service.GetUser();
                if (!res.isSuccess)
                {
                    return null;
                }
                else
                {
                    user = res.content;
                    return user;
                }
            }
            else return user;
        }

        /// <summary>
        /// You should return from your current method if this returns true
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public async Task<bool> ConnectionRequired()
        {
            var resp = await GetAuth();
            if (string.IsNullOrWhiteSpace(resp.code))
            {
                this.user = null;

                tempdata["afterConnectReturn"] = manager.Uri;
                manager.NavigateTo("/setkey");
                return true;

            }
            else return false;
        }
    }
}
