using System.ComponentModel.DataAnnotations;

namespace BookSamsysAPI.Models
{
    public class Book
    {
<<<<<<< Updated upstream
        public int Id { get; set; }
        public string Name { get; set; }
        public long ISBN { get; set; }
=======
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
>>>>>>> Stashed changes
        public string Author { get; set; }
        [Required]
        public float Price { get; set; }
        public Book()
        {

        }
    }

}
