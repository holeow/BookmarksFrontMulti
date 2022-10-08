namespace BookmarksFront.Classes
{
    public class WebUser
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }

        public string ResourceRouteToken => $"UserResource{ID}";

    }
}
