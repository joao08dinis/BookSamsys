namespace BookSamsysAPI.Models
{
    public class AddBookRequest
    {
        public string ISBN { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public float Price { get; set; }

    }
}
