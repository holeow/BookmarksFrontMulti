using Microsoft.JSInterop;

namespace BookmarksFront.Classes.Services
{
	public class FeatherJS
	{
		IJSRuntime runtime;
		public FeatherJS(IJSRuntime runtime)
		{
			this.runtime = runtime;
            
        }

        public async Task Replace()
        {
            await runtime.InvokeVoidAsync("addedJSFeatherReplace");
        }
	}
}
