namespace BookSamsysAPI.Models
{
    public class UpdateBookRequest
    {
        public string iSBN { get; set; }
        public string name { get; set; }
        public string author { get; set; }
        public float price { get; set; }

    }
}
