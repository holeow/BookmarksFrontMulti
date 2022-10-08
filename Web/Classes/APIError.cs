namespace BookmarksFront.Classes
{
    public class APIError
    {
        public string type;
        public Dictionary<string, object> data;

        public APIError()
        {

        }
        public APIError(string type, Dictionary<string, object> data)
        {
            this.type = type;
            this.data = data;
        }

        public APIError(string type, params (string key, object value)[] data)
        {
            this.type = type;
            this.data = new();
            foreach (var valueTuple in data)
            {
                this.data.Add(valueTuple.key, valueTuple.value);
            }
        }
    }
}
