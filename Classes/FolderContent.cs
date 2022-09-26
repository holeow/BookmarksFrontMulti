namespace BookmarksFront.Classes
{
	public class FolderContent
	{
		public Folder current { get; set; }
		public List<Folder> children{ get; set; }
        public List<Bookmark> bookmarks { get; set; }
	}
}
