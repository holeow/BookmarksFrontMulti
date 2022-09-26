using BookmarksFront.Components;

using Newtonsoft.Json;

using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Text;

//## Summary
//# link:v:"## Folders"
//#         link:v:"# GET Root Folder Content"
//#         link:v:"# GET Folder Content"
//#         link:v:"# POST Folder"
//#         link:v:"# PUT Folder"
//#         link:v:"# GET Folder"
//#         link:v:"# DELETE Folder"
//# link:v:"## Bookmarks"
//#         link:v:"# GET Bookmark"
//#         link:v:"# POST Bookmark"
//#         link:v:"# PUT Bookmark"
//#         link:v:"# DELETE Bookmark"
//# link:v:"## Tags"
//#         link:v:"# GET user tags"
//#         link:v:"# GET bookmark tags"
//#         link:v:"# GET folder tags"
//#         link:v:"# PUT bookmark tags"
//#         link:v:"# PUT folder tags"
//#         link:v:"# GET folders with tag"
//#         link:v:"# GET bookmarks with tag"


namespace BookmarksFront.Classes.Services
{

    public interface IBookmarkService
    {
        //# Folders
        public Task<Response<FolderContent>> GetRootFolderContent(string apikey);
        public Task<Response<FolderContent>> GetFolderContent(string apikey, int folderID);
        public Task<Response<Folder>> PostFolder(string apikey, Folder folder);
        public Task<Response<Folder>> PutFolder(string apikey, Folder folder);
        public Task<Response<Folder>> GetFolder(string apikey, int folderID);
        public Task<Response<int>> DeleteFolder(string apikey, int folderID);

        //# Bookmarks
        public Task<Response<Bookmark>> PostBookmark (string apikey, Bookmark bookmark);
        public Task<Response<Bookmark>> GetBookmark(string apikey, int bookmarkID);
        public Task<Response<Bookmark>> PutBookmark(string apikey, Bookmark bookmark);
        public Task<Response<int>> DeleteBookmark(string apikey, int bookmarkID);

        //# Tags
        public Task<Response<List<string>>> GetTagsOfUser(string apikey);
        public Task<Response<List<string>>> GetTagsOfBookmark(string apikey, int bookmarkID);
        public Task<Response<List<string>>> GetTagsOfFolder(string apikey, int folderID);
        public Task<Response<object>> PutBookmarkTags(string apikey, int bookmarkID, List<string> tags);
        public Task<Response<object>> PutFolderTags(string apikey, int folderID, List<string> tags);
        public Task<Response<List<Folder>>> GetFoldersWithTag(string apikey, string tag);
        public Task<Response<List<Bookmark>>> GetBookmarksWithTag(string apikey, string tag);

    }

    public class BookmarkService : IBookmarkService
    {
        
        private readonly HttpClient httpClient;
        private readonly JsonSerializerSettings settings;
        public BookmarkService(HttpClient httpClient)
        {
            this.httpClient = httpClient;

            settings = new JsonSerializerSettings();
            settings.MissingMemberHandling = MissingMemberHandling.Error;
        }

        //## Folders
        //# GET Root Folder Content
        /// <summary>
        /// Get the content of the root folder of the user
        /// </summary>
        /// <param name="apikey">The api key of the user</param>
        /// <returns></returns>
        public async Task<Response<FolderContent>> GetRootFolderContent(string apikey)
        {
            using (var requestMessage =
                   new HttpRequestMessage(HttpMethod.Get, "https://apibookmarks.hlw.ninja/api/user/root/content"))
            {
                requestMessage.Headers.Add("APIKEY", apikey);
                var response = await httpClient.SendAsync(requestMessage);

                var r = Response<FolderContent>.FromJson(await response.Content.ReadAsStringAsync(),
                    (int)response.StatusCode);

                return r;

               
            }

        }
        //# GET Folder Content
        /// <summary>
        /// Get the content of a floder of the user
        /// </summary>
        /// <param name="apikey">The api key of the user</param>
        /// <param name="folderID"></param>
        /// <returns></returns>
        public async Task<Response<FolderContent>> GetFolderContent(string apikey, int folderID)
        {
            using (var requestMessage =
                   new HttpRequestMessage(HttpMethod.Get, $"https://apibookmarks.hlw.ninja/api/folders/{folderID}/content"))
            {
                requestMessage.Headers.Add("APIKEY", apikey);
                var response = await httpClient.SendAsync(requestMessage);


               
                    var r = Response<FolderContent>.FromJson(await response.Content.ReadAsStringAsync(),
                        (int)response.StatusCode);

                    return r;

               
            }
        }


        //# POST Folder
        public async Task<Response<Folder>> PostFolder(string apikey, Folder folder)
        {
            using (var requestMessage =
                   new HttpRequestMessage(HttpMethod.Post, $"https://apibookmarks.hlw.ninja/api/folders"))
            {
                requestMessage.Headers.Add("APIKEY", apikey);
                requestMessage.Content = new StringContent(JsonConvert.SerializeObject(folder), Encoding.UTF8, "application/json");
                var response = await httpClient.SendAsync(requestMessage);



                var r = Response<Folder>.FromJson(await response.Content.ReadAsStringAsync(),
                    (int)response.StatusCode);

                return r;

            }
        }

        //# PUT Folder
        public async Task<Response<Folder>> PutFolder(string apikey, Folder folder)
        {
            using (var requestMessage =
                   new HttpRequestMessage(HttpMethod.Put, $"https://apibookmarks.hlw.ninja/api/folders/{folder.ID}"))
            {
                requestMessage.Headers.Add("APIKEY", apikey);
                requestMessage.Content = new StringContent(JsonConvert.SerializeObject(folder), Encoding.UTF8, "application/json");
                var response = await httpClient.SendAsync(requestMessage);



                var r = Response<Folder>.FromJson(await response.Content.ReadAsStringAsync(),
                    (int)response.StatusCode);

                return r;

            }
        }

        //# GET Folder
        public async Task<Response<Folder>> GetFolder(string apikey, int folderID)
        {
            using (var requestMessage =
                   new HttpRequestMessage(HttpMethod.Get, $"https://apibookmarks.hlw.ninja/api/folders/{folderID}"))
            {
                requestMessage.Headers.Add("APIKEY", apikey);
                var response = await httpClient.SendAsync(requestMessage);



                var r = Response<Folder>.FromJson(await response.Content.ReadAsStringAsync(),
                    (int)response.StatusCode);

                return r;


            }
        }


        //# DELETE Folder
        public async Task<Response<int>> DeleteFolder(string apikey, int folderID)
        {
            using (var requestMessage =
                   new HttpRequestMessage(HttpMethod.Delete, $"https://apibookmarks.hlw.ninja/api/folders/{folderID}"))
            {
                requestMessage.Headers.Add("APIKEY", apikey);
                //requestMessage.Content = new StringContent(JsonConvert.SerializeObject(bookmark), Encoding.UTF8, "application/json");
                var response = await httpClient.SendAsync(requestMessage);



                var r = Response<int>.FromJson(await response.Content.ReadAsStringAsync(),
                    (int)response.StatusCode);

                return r;

            }
        }



        //## Bookmarks
        //# GET Bookmark
        public async Task<Response<Bookmark>> GetBookmark(string apikey, int bookmarkID)
        {
            using (var requestMessage =
                   new HttpRequestMessage(HttpMethod.Get, $"https://apibookmarks.hlw.ninja/api/bookmarks/{bookmarkID}"))
            {
                requestMessage.Headers.Add("APIKEY", apikey);
                var response = await httpClient.SendAsync(requestMessage);


               
                    var r = Response<Bookmark>.FromJson(await response.Content.ReadAsStringAsync(),
                        (int)response.StatusCode);

                    return r;

               
            }
        }

        //# POST Bookmark
        public async Task<Response<Bookmark>> PostBookmark(string apikey, Bookmark bookmark)
        {
            using (var requestMessage =
                   new HttpRequestMessage(HttpMethod.Post, $"https://apibookmarks.hlw.ninja/api/bookmarks"))
            {
                requestMessage.Headers.Add("APIKEY", apikey);
                requestMessage.Content = new StringContent(JsonConvert.SerializeObject(bookmark), Encoding.UTF8, "application/json");
                var response = await httpClient.SendAsync(requestMessage);


                
                    var r = Response<Bookmark>.FromJson(await response.Content.ReadAsStringAsync(),
                        (int)response.StatusCode);

                    return r;

            }
        }

        //# PUT Bookmark
        public async Task<Response<Bookmark>> PutBookmark(string apikey, Bookmark bookmark)
        {
            using (var requestMessage =
                   new HttpRequestMessage(HttpMethod.Put, $"https://apibookmarks.hlw.ninja/api/bookmarks/{bookmark.ID}"))
            {
                requestMessage.Headers.Add("APIKEY", apikey);
                requestMessage.Content = new StringContent(JsonConvert.SerializeObject(bookmark), Encoding.UTF8, "application/json");
                var response = await httpClient.SendAsync(requestMessage);



                var r = Response<Bookmark>.FromJson(await response.Content.ReadAsStringAsync(),
                    (int)response.StatusCode);

                return r;

            }
        }

        //# DELETE Bookmark
        public async Task<Response<int>> DeleteBookmark(string apikey, int bookmarkID)
        {
            using (var requestMessage =
                   new HttpRequestMessage(HttpMethod.Delete, $"https://apibookmarks.hlw.ninja/api/bookmarks/{bookmarkID}"))
            {
                requestMessage.Headers.Add("APIKEY", apikey);
                //requestMessage.Content = new StringContent(JsonConvert.SerializeObject(bookmark), Encoding.UTF8, "application/json");
                var response = await httpClient.SendAsync(requestMessage);



                var r = Response<int>.FromJson(await response.Content.ReadAsStringAsync(),
                    (int)response.StatusCode);

                return r;

            }
        }


        //## Tags
        //# GET user tags
        public async Task<Response<List<string>>> GetTagsOfUser(string apikey)
        {
           
                using (var requestMessage =
                       new HttpRequestMessage(HttpMethod.Get, $"https://apibookmarks.hlw.ninja/api/tags/"))
                {
                    requestMessage.Headers.Add("APIKEY", apikey);
                    var response = await httpClient.SendAsync(requestMessage);



                    var r = Response<List<string>>.FromJson(await response.Content.ReadAsStringAsync(),
                        (int)response.StatusCode);

                    return r;


                }
            
        }

        //# GET bookmark tags
        public async Task<Response<List<string>>> GetTagsOfBookmark(string apikey, int bookmarkID)
        {
            using (var requestMessage =
                   new HttpRequestMessage(HttpMethod.Get, $"https://apibookmarks.hlw.ninja/api/bookmarks/{bookmarkID}/tags"))
            {
                requestMessage.Headers.Add("APIKEY", apikey);
                var response = await httpClient.SendAsync(requestMessage);



                var r = Response<List<string>>.FromJson(await response.Content.ReadAsStringAsync(),
                    (int)response.StatusCode);

                return r;


            }
        }

        //# GET folder tags
        public async Task<Response<List<string>>> GetTagsOfFolder(string apikey, int folderID)
        {
            using (var requestMessage =
                   new HttpRequestMessage(HttpMethod.Get, $"https://apibookmarks.hlw.ninja/api/folders/{folderID}/tags"))
            {
                requestMessage.Headers.Add("APIKEY", apikey);
                var response = await httpClient.SendAsync(requestMessage);



                var r = Response<List<string>>.FromJson(await response.Content.ReadAsStringAsync(),
                    (int)response.StatusCode);

                return r;


            }
        }
        //# PUT bookmark tags
        public async Task<Response<object>> PutBookmarkTags(string apikey, int bookmarkID, List<string> tags)
        {
            using (var requestMessage =
                   new HttpRequestMessage(HttpMethod.Put, $"https://apibookmarks.hlw.ninja/api/bookmarks/{bookmarkID}/tags"))
            {
                requestMessage.Headers.Add("APIKEY", apikey);
                requestMessage.Content = new StringContent(JsonConvert.SerializeObject(new Dictionary<string, object>(){{"tags",tags}}), Encoding.UTF8, "application/json");
                var response = await httpClient.SendAsync(requestMessage);



                var r = Response<object>.FromJson(await response.Content.ReadAsStringAsync(),
                    (int)response.StatusCode);

                return r;

            }
        }

        //# PUT folder tags
        public async Task<Response<object>> PutFolderTags(string apikey, int folderID, List<string> tags)
        {
            using (var requestMessage =
                   new HttpRequestMessage(HttpMethod.Put, $"https://apibookmarks.hlw.ninja/api/folders/{folderID}/tags"))
            {
                requestMessage.Headers.Add("APIKEY", apikey);
                requestMessage.Content = new StringContent(JsonConvert.SerializeObject(new Dictionary<string, object>() { { "tags", tags } }), Encoding.UTF8, "application/json");
                var response = await httpClient.SendAsync(requestMessage);



                var r = Response<object>.FromJson(await response.Content.ReadAsStringAsync(),
                    (int)response.StatusCode);

                return r;

            }
        }
        //# GET folders with tag
        public async Task<Response<List<Folder>>> GetFoldersWithTag(string apikey, string tag)
        {
            using (var requestMessage =
                   new HttpRequestMessage(HttpMethod.Get, $"https://apibookmarks.hlw.ninja/api/tags/{tag}/folders"))
            {
                requestMessage.Headers.Add("APIKEY", apikey);
                var response = await httpClient.SendAsync(requestMessage);



                var r = Response<List<Folder>>.FromJson(await response.Content.ReadAsStringAsync(),
                    (int)response.StatusCode);

                return r;


            }
        }
        //# GET bookmarks with tag
        public async Task<Response<List<Bookmark>>> GetBookmarksWithTag(string apikey, string tag)
        {
            using (var requestMessage =
                   new HttpRequestMessage(HttpMethod.Get, $"https://apibookmarks.hlw.ninja/api/tags/{tag}/bookmarks"))
            {
                requestMessage.Headers.Add("APIKEY", apikey);
                var response = await httpClient.SendAsync(requestMessage);



                var r = Response<List<Bookmark>>.FromJson(await response.Content.ReadAsStringAsync(),
                    (int)response.StatusCode);

                return r;


            }
        }
    }
}
