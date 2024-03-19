namespace BookSamsysAPI.Models
{
    public class MessagingHelper
    {
        public MessagingHelper(string type, string description,Object obj, bool success)
        {
            Type = type;
            Description = description;
            Obj = obj;
            Success = success;
        }
        public MessagingHelper(string type, string description, bool success)
        {
            Type = type;
            Description = description;
            Success = success;
        }

        public string Type { get; private set; }
        public string Description { get; private set; }
        public Object? Obj { get; private set; }
        public bool Success { get; private set; }
    }
}
