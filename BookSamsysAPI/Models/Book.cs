namespace BookSamsysAPI.Models
{
    public class Book
    {
        public int Id { get; set; }
        public long ISBN { get; set; }
        public string Author { get; set; }
        public float Price { get; set; }
        public Book()
        {

        }
    }

}
