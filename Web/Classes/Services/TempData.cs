using Microsoft.AspNetCore.Components;

namespace BookmarksFront.Classes.Services
{
	public class TempData
    {
        private static Dictionary<string, object> forNext = new Dictionary<string, object>();
        private static Dictionary<string, object> forNow = new Dictionary<string, object>();
        public NavigationManager NavigationManager { get; set; }
        public TempData(NavigationManager manager)
        {
            this.NavigationManager = manager;
            manager.LocationChanged += Manager_LocationChanged;
        }

        private void Manager_LocationChanged(object? sender, Microsoft.AspNetCore.Components.Routing.LocationChangedEventArgs e)
        {
            forNow = forNext;
            forNext = new();
        }

        public object this[string key]
        {
            get
            {
                if (forNow.ContainsKey(key))
                {
                    return forNow[key];
                }
                else return null;
            }
            set
            {
                if (forNext.ContainsKey(key))
                {
                    forNext[key] = value;
                }
                else forNext.Add(key, value);
            }
        }
    }
}
