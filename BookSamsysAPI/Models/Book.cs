namespace BookSamsysAPI.Models
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ISBN { get; set; }
        public string Author { get; set; }
        public float Price { get; set; }
        public Book()
        {

        }
    }

}
