using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BookmarksFront.Classes
{
    public class Response<T>
    {
        public string returnType { get; set; }
        public T? content { get; set; }
        public string message { get; set; }
        [JsonIgnore] public bool isSuccess => returnType == "success";
        [JsonIgnore] public int? httpcode;
        public List<APIError> errors{ get; set; }



        public static Response<T> FromJson(string json, int httpcode)
        {
            try
            {
                var r = JsonConvert.DeserializeObject<Response<T>>(json);
                r.httpcode = httpcode;
                return r;
            }
            catch (Exception e)
            {
                Response<T> r = new Response<T>();
                r.httpcode = 999;
                r.message = e.ToString();
                r.errors = new List<APIError>() {new APIError("deserialization error", ("exceptiontype",e.GetType()),("data",json))};
                return r;
            }
            
        }
    }

    public class ErrorDeserializingResponseException : Exception
    {

    }
}
