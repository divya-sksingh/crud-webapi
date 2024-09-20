using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductsCrud.DataModel
{
    [Serializable]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; }

        [Required(ErrorMessage = "Product Name cannot be empty")]
        public string Name { get; set; }

        [Range(1.00, double.MaxValue, ErrorMessage = "Product price must be greater than zero")]
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
    }
}
