using Microsoft.EntityFrameworkCore;
using ServiceStack.DataAnnotations;
using PrimaryKeyAttribute = ServiceStack.DataAnnotations.PrimaryKeyAttribute;

namespace BookSamsysAPI.Models.Doman
{
    public class Book
    {
        [Required]
        [PrimaryKey]
        [AutoIncrement]
        public int id { get; set; }
        public string name { get; set; }
        public string iSBN { get; set; }
        public string author { get; set; }
        public decimal price { get; set; }
        public Book()
        {

        }
    }

}
