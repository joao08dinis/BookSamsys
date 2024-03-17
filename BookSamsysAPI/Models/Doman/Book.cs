namespace BookSamsysAPI.Models.Doman
{
    public class Book
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string iSBN { get; set; }
        public string author { get; set; }
        public float price { get; set; }
        public Book()
        {

        }
    }

}
