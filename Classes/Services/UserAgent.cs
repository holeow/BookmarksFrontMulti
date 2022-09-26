using Microsoft.JSInterop;

namespace BookmarksFront.Classes.Services
{
	public class UserAgent
	{
		public IJSRuntime JSRuntime { get; set; }
		public UserAgent(IJSRuntime JSRuntime)
		{
			this.JSRuntime = JSRuntime;
		}


		public async Task<string> GetUserAgent()
		{
			return await JSRuntime.InvokeAsync<string>("getUserAgent");
        }

		public async Task<bool> IsGoogleBot()
		{
			var useragent = await JSRuntime.InvokeAsync<string>("getUserAgent");
			return useragent.Contains("google.com/bot");
		}
	}
}
