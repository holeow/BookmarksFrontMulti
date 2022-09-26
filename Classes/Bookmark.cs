namespace BookmarksFront.Classes
{
	public class Bookmark
	{
        public int ID { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public string? Comment { get; set; }
        public DateTime CreationDate { get; set; }
        public int Folder { get; set; }
        public string? ImgUrl { get; set; }

    }
}
