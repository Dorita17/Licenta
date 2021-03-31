using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class BasketItemDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string MealName { get; set; }

        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Grams must be at least 1")]
        public int Grams { get; set; }

        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Calories must be greater than 0")]
        public decimal Calories { get; set; }

        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Proteins must be greater than 0")]
        public decimal Proteins { get; set; }

        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Carbohydrates must be greater than 0")]
        public decimal Carbohydrates { get; set; }

        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Fats must be greater than 0")]
        public decimal Fats { get; set; }

        [Required]
        public string PictureUrl { get; set; }

        [Required]
        public string Type { get; set; }
    }
}