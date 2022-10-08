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
//# link:v:"## Search"
//# link:v:"## User"


namespace BookmarksFront.Classes.Services
{

    public interface IBookmarkService
    {
        //# Folders
        public Task<Response<FolderContent>> GetRootFolderContent();
        public Task<Response<FolderContent>> GetFolderContent( int folderID);
        public Task<Response<Folder>> PostFolder( Folder folder);
        public Task<Response<Folder>> PutFolder( Folder folder);
        public Task<Response<Folder>> GetFolder( int folderID);
        public Task<Response<int>> DeleteFolder( int folderID);

        //# Bookmarks
        public Task<Response<Bookmark>> PostBookmark ( Bookmark bookmark);
        public Task<Response<Bookmark>> GetBookmark( int bookmarkID);
        public Task<Response<Bookmark>> PutBookmark( Bookmark bookmark);
        public Task<Response<int>> DeleteBookmark( int bookmarkID);

        //# Tags
        public Task<Response<List<string>>> GetTagsOfUser();
        public Task<Response<List<string>>> GetTagsOfBookmark( int bookmarkID);
        public Task<Response<List<string>>> GetTagsOfFolder( int folderID);
        public Task<Response<object>> PutBookmarkTags( int bookmarkID, List<string> tags);
        public Task<Response<object>> PutFolderTags( int folderID, List<string> tags);
        public Task<Response<List<Folder>>> GetFoldersWithTag( string tag);
        public Task<Response<List<Bookmark>>> GetBookmarksWithTag( string tag);

        //# Search
        public Task<Response<SearchResult>> Search( string query);

        //# User
        public Task<Response<WebUser>> GetUser();

        public Task<Response<RemoteConnection>> GetRemoteConnection(string username, string pass);

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
        public async Task<Response<FolderContent>> GetRootFolderContent()
        {
            using (var requestMessage =
                   new HttpRequestMessage(HttpMethod.Get, "https://apibookmarks.hlw.ninja/api/user/root/content"))
            {
                var auth = await Auth.instance.GetAuth();
                requestMessage.Headers.Add(auth.authType, auth.code);
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
        public async Task<Response<FolderContent>> GetFolderContent( int folderID)
        {
            using (var requestMessage =
                   new HttpRequestMessage(HttpMethod.Get, $"https://apibookmarks.hlw.ninja/api/folders/{folderID}/content"))
            {
                var auth = await Auth.instance.GetAuth();
                requestMessage.Headers.Add(auth.authType, auth.code);
                var response = await httpClient.SendAsync(requestMessage);


               
                    var r = Response<FolderContent>.FromJson(await response.Content.ReadAsStringAsync(),
                        (int)response.StatusCode);

                    return r;

               
            }
        }


        //# POST Folder
        public async Task<Response<Folder>> PostFolder( Folder folder)
        {
            using (var requestMessage =
                   new HttpRequestMessage(HttpMethod.Post, $"https://apibookmarks.hlw.ninja/api/folders"))
            {
                var auth = await Auth.instance.GetAuth();
                requestMessage.Headers.Add(auth.authType, auth.code);
                requestMessage.Content = new StringContent(JsonConvert.SerializeObject(folder), Encoding.UTF8, "application/json");
                var response = await httpClient.SendAsync(requestMessage);



                var r = Response<Folder>.FromJson(await response.Content.ReadAsStringAsync(),
                    (int)response.StatusCode);

                return r;

            }
        }

        //# PUT Folder
        public async Task<Response<Folder>> PutFolder( Folder folder)
        {
            using (var requestMessage =
                   new HttpRequestMessage(HttpMethod.Put, $"https://apibookmarks.hlw.ninja/api/folders/{folder.ID}"))
            {
                var auth = await Auth.instance.GetAuth();
                requestMessage.Headers.Add(auth.authType, auth.code);
                requestMessage.Content = new StringContent(JsonConvert.SerializeObject(folder), Encoding.UTF8, "application/json");
                var response = await httpClient.SendAsync(requestMessage);



                var r = Response<Folder>.FromJson(await response.Content.ReadAsStringAsync(),
                    (int)response.StatusCode);

                return r;

            }
        }

        //# GET Folder
        public async Task<Response<Folder>> GetFolder( int folderID)
        {
            using (var requestMessage =
                   new HttpRequestMessage(HttpMethod.Get, $"https://apibookmarks.hlw.ninja/api/folders/{folderID}"))
            {
                var auth = await Auth.instance.GetAuth();
                requestMessage.Headers.Add(auth.authType, auth.code);
                var response = await httpClient.SendAsync(requestMessage);



                var r = Response<Folder>.FromJson(await response.Content.ReadAsStringAsync(),
                    (int)response.StatusCode);

                return r;


            }
        }


        //# DELETE Folder
        public async Task<Response<int>> DeleteFolder( int folderID)
        {
            using (var requestMessage =
                   new HttpRequestMessage(HttpMethod.Delete, $"https://apibookmarks.hlw.ninja/api/folders/{folderID}"))
            {
                var auth = await Auth.instance.GetAuth();
                requestMessage.Headers.Add(auth.authType, auth.code);
                //requestMessage.Content = new StringContent(JsonConvert.SerializeObject(bookmark), Encoding.UTF8, "application/json");
                var response = await httpClient.SendAsync(requestMessage);



                var r = Response<int>.FromJson(await response.Content.ReadAsStringAsync(),
                    (int)response.StatusCode);

                return r;

            }
        }



        //## Bookmarks
        //# GET Bookmark
        public async Task<Response<Bookmark>> GetBookmark( int bookmarkID)
        {
            using (var requestMessage =
                   new HttpRequestMessage(HttpMethod.Get, $"https://apibookmarks.hlw.ninja/api/bookmarks/{bookmarkID}"))
            {
                var auth = await Auth.instance.GetAuth();
                requestMessage.Headers.Add(auth.authType, auth.code);
                var response = await httpClient.SendAsync(requestMessage);


               
                    var r = Response<Bookmark>.FromJson(await response.Content.ReadAsStringAsync(),
                        (int)response.StatusCode);

                    return r;

               
            }
        }

        //# POST Bookmark
        public async Task<Response<Bookmark>> PostBookmark( Bookmark bookmark)
        {
            using (var requestMessage =
                   new HttpRequestMessage(HttpMethod.Post, $"https://apibookmarks.hlw.ninja/api/bookmarks"))
            {
                var auth = await Auth.instance.GetAuth();
                requestMessage.Headers.Add(auth.authType, auth.code);
                requestMessage.Content = new StringContent(JsonConvert.SerializeObject(bookmark), Encoding.UTF8, "application/json");
                var response = await httpClient.SendAsync(requestMessage);


                
                    var r = Response<Bookmark>.FromJson(await response.Content.ReadAsStringAsync(),
                        (int)response.StatusCode);

                    return r;

            }
        }

        //# PUT Bookmark
        public async Task<Response<Bookmark>> PutBookmark( Bookmark bookmark)
        {
            using (var requestMessage =
                   new HttpRequestMessage(HttpMethod.Put, $"https://apibookmarks.hlw.ninja/api/bookmarks/{bookmark.ID}"))
            {
                var auth = await Auth.instance.GetAuth();
                requestMessage.Headers.Add(auth.authType, auth.code);
                requestMessage.Content = new StringContent(JsonConvert.SerializeObject(bookmark), Encoding.UTF8, "application/json");
                var response = await httpClient.SendAsync(requestMessage);



                var r = Response<Bookmark>.FromJson(await response.Content.ReadAsStringAsync(),
                    (int)response.StatusCode);

                return r;

            }
        }

        //# DELETE Bookmark
        public async Task<Response<int>> DeleteBookmark( int bookmarkID)
        {
            using (var requestMessage =
                   new HttpRequestMessage(HttpMethod.Delete, $"https://apibookmarks.hlw.ninja/api/bookmarks/{bookmarkID}"))
            {
                var auth = await Auth.instance.GetAuth();
                requestMessage.Headers.Add(auth.authType, auth.code);
                //requestMessage.Content = new StringContent(JsonConvert.SerializeObject(bookmark), Encoding.UTF8, "application/json");
                var response = await httpClient.SendAsync(requestMessage);



                var r = Response<int>.FromJson(await response.Content.ReadAsStringAsync(),
                    (int)response.StatusCode);

                return r;

            }
        }


        //## Tags
        //# GET user tags
        public async Task<Response<List<string>>> GetTagsOfUser()
        {
           
                using (var requestMessage =
                       new HttpRequestMessage(HttpMethod.Get, $"https://apibookmarks.hlw.ninja/api/tags/"))
                {
                var auth = await Auth.instance.GetAuth();
                requestMessage.Headers.Add(auth.authType, auth.code);
                var response = await httpClient.SendAsync(requestMessage);



                    var r = Response<List<string>>.FromJson(await response.Content.ReadAsStringAsync(),
                        (int)response.StatusCode);

                    return r;


                }
            
        }

        //# GET bookmark tags
        public async Task<Response<List<string>>> GetTagsOfBookmark( int bookmarkID)
        {
            using (var requestMessage =
                   new HttpRequestMessage(HttpMethod.Get, $"https://apibookmarks.hlw.ninja/api/bookmarks/{bookmarkID}/tags"))
            {
                var auth = await Auth.instance.GetAuth();
                requestMessage.Headers.Add(auth.authType, auth.code);
                var response = await httpClient.SendAsync(requestMessage);



                var r = Response<List<string>>.FromJson(await response.Content.ReadAsStringAsync(),
                    (int)response.StatusCode);

                return r;


            }
        }

        //# GET folder tags
        public async Task<Response<List<string>>> GetTagsOfFolder( int folderID)
        {
            using (var requestMessage =
                   new HttpRequestMessage(HttpMethod.Get, $"https://apibookmarks.hlw.ninja/api/folders/{folderID}/tags"))
            {
                var auth = await Auth.instance.GetAuth();
                requestMessage.Headers.Add(auth.authType, auth.code);
                var response = await httpClient.SendAsync(requestMessage);



                var r = Response<List<string>>.FromJson(await response.Content.ReadAsStringAsync(),
                    (int)response.StatusCode);

                return r;


            }
        }
        //# PUT bookmark tags
        public async Task<Response<object>> PutBookmarkTags( int bookmarkID, List<string> tags)
        {
            using (var requestMessage =
                   new HttpRequestMessage(HttpMethod.Put, $"https://apibookmarks.hlw.ninja/api/bookmarks/{bookmarkID}/tags"))
            {
                var auth = await Auth.instance.GetAuth();
                requestMessage.Headers.Add(auth.authType, auth.code);
                requestMessage.Content = new StringContent(JsonConvert.SerializeObject(new Dictionary<string, object>(){{"tags",tags}}), Encoding.UTF8, "application/json");
                var response = await httpClient.SendAsync(requestMessage);



                var r = Response<object>.FromJson(await response.Content.ReadAsStringAsync(),
                    (int)response.StatusCode);

                return r;

            }
        }

        //# PUT folder tags
        public async Task<Response<object>> PutFolderTags( int folderID, List<string> tags)
        {
            using (var requestMessage =
                   new HttpRequestMessage(HttpMethod.Put, $"https://apibookmarks.hlw.ninja/api/folders/{folderID}/tags"))
            {
                var auth = await Auth.instance.GetAuth();
                requestMessage.Headers.Add(auth.authType, auth.code);
                requestMessage.Content = new StringContent(JsonConvert.SerializeObject(new Dictionary<string, object>() { { "tags", tags } }), Encoding.UTF8, "application/json");
                var response = await httpClient.SendAsync(requestMessage);



                var r = Response<object>.FromJson(await response.Content.ReadAsStringAsync(),
                    (int)response.StatusCode);

                return r;

            }
        }
        //# GET folders with tag
        public async Task<Response<List<Folder>>> GetFoldersWithTag( string tag)
        {
            using (var requestMessage =
                   new HttpRequestMessage(HttpMethod.Get, $"https://apibookmarks.hlw.ninja/api/tags/{tag}/folders"))
            {
                var auth = await Auth.instance.GetAuth();
                requestMessage.Headers.Add(auth.authType, auth.code);
                var response = await httpClient.SendAsync(requestMessage);



                var r = Response<List<Folder>>.FromJson(await response.Content.ReadAsStringAsync(),
                    (int)response.StatusCode);

                return r;


            }
        }
        //# GET bookmarks with tag
        public async Task<Response<List<Bookmark>>> GetBookmarksWithTag( string tag)
        {
            using (var requestMessage =
                   new HttpRequestMessage(HttpMethod.Get, $"https://apibookmarks.hlw.ninja/api/tags/{tag}/bookmarks"))
            {
                var auth = await Auth.instance.GetAuth();
                requestMessage.Headers.Add(auth.authType, auth.code);
                var response = await httpClient.SendAsync(requestMessage);



                var r = Response<List<Bookmark>>.FromJson(await response.Content.ReadAsStringAsync(),
                    (int)response.StatusCode);

                return r;


            }
        }


        //## Search

        public async Task<Response<SearchResult>> Search( string query)
        {
            using (var requestMessage =
                   new HttpRequestMessage(HttpMethod.Get, $"https://apibookmarks.hlw.ninja/api/search?{query}"))
            {
                var auth = await Auth.instance.GetAuth();
                requestMessage.Headers.Add(auth.authType, auth.code);
                var response = await httpClient.SendAsync(requestMessage);



                var r = Response<SearchResult>.FromJson(await response.Content.ReadAsStringAsync(),
                    (int)response.StatusCode);

                return r;


            }
        }

        //## User
        public async Task<Response<WebUser>> GetUser()
        {
            using (var requestMessage =
                   new HttpRequestMessage(HttpMethod.Get, $"https://apibookmarks.hlw.ninja/api/user"))
            {
                var auth = await Auth.instance.GetAuth();
                requestMessage.Headers.Add(auth.authType, auth.code);
                var response = await httpClient.SendAsync(requestMessage);



                var r = Response<WebUser>.FromJson(await response.Content.ReadAsStringAsync(),
                    (int)response.StatusCode);

                return r;


            }
        }

        public record Connection(string username, string password);
        public async Task<Response<RemoteConnection>> GetRemoteConnection(string username, string pass)
        {
            using (var requestMessage =
                   new HttpRequestMessage(HttpMethod.Post, $"https://apibookmarks.hlw.ninja/api/auth/login"))
            {
                Connection pl = new Connection(username, pass);
                
                requestMessage.Content = new StringContent(JsonConvert.SerializeObject(pl), Encoding.UTF8, "application/json");
                var response = await httpClient.SendAsync(requestMessage);

                

                var r = Response<RemoteConnection>.FromJson(await response.Content.ReadAsStringAsync(),
                    (int)response.StatusCode);

                return r;

            }
        }
    }
}
