using Microsoft.JSInterop;

namespace BookmarksFront.Classes.Services
{
	public static class WindowSize
	{
		public delegate void WindowSizeChangedHandler(float width,float height);

		public static event WindowSizeChangedHandler WindowSizeChanged;


        public static async Task<float> GetWidth()
        {
            return await runtime.InvokeAsync<float>("getWindowWidth");
        }
        public static async Task<float> GetHeight()
        {
            return await runtime.InvokeAsync<float>("getWindowHeight");
        }

        public static IJSRuntime runtime;

        [JSInvokable]
        public static void RaiseWindowSizeChanged(float width, float height)
        {

			WindowSizeChanged?.Invoke(width,height);
        }

	}
}
