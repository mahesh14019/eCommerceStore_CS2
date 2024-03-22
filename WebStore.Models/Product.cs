using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebStore.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required]
        [MaxLength(30)]
        [RegularExpression("[a-zA-Z]+$", ErrorMessage = "Product Name should be a valid string value.")]
        [DisplayName("Product Name")]
        public string ProductName { get; set; }
        [Required]
        [MaxLength(30)]
        public string Author { get; set; }
        [Required]
        [MaxLength(30)]
        public string ISBN { get; set; }
        [Required]
        [Range(1, 10000,ErrorMessage ="Price between 1 and 10000")]
        public decimal Price { get; set; }
        [DisplayName("Added Date")]
        [DataType(DataType.Date)]
        public DateTime? AddedDate { get; set; }

        public bool IsActive { get; set; }
    }
}
