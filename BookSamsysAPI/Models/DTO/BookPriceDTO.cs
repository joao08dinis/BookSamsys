namespace BookSamsysAPI.Models.DTO
{
    public class BookPriceDTO
    {
        public BookPriceDTO(decimal price)
        {
            price = price;
        }
        public decimal price { get; set; }
    }
}
