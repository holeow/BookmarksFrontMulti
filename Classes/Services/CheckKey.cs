using Microsoft.AspNetCore.Components;

namespace BookmarksFront.Classes.Services
{
	public class CheckKey
	{
		[Inject]
		public ICookie cookie { get; set; }
		[Inject]
		public NavigationManager navigationManager { get; set; }
        public string APIKey;
		public CheckKey(ICookie cookie, NavigationManager navigationManager)
		{
			this.cookie = cookie;
			this.navigationManager = navigationManager;

			
		}

        public async Task Check()
        {
            APIKey = await Classes.APIKey.GetKey(cookie);
            if (string.IsNullOrWhiteSpace(APIKey) || APIKey == "Wrong Key")
            {
                navigationManager.NavigateTo("/setkey");
            }
        }

        public async Task ResetKeyAndRedirect()
        {
            await Classes.APIKey.SetKey(cookie, "Wrong Key");
            navigationManager.NavigateTo("/wrongkey");
        }


	}
}
