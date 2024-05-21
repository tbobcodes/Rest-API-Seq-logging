using System.ComponentModel.DataAnnotations;

namespace BookStoreApp.API.Models.Author
{
    public class OneAuthorWithNoBooks
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        [StringLength(250)]
        public string Bio { get; set; }
    }
}
