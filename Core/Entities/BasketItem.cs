namespace Core.Entities
{
    public class BasketItem
    {
        public int Id { get; set; }
        public string MealName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int Grams { get; set; }
        public decimal Calories { get; set; }
        public decimal Proteins { get; set; }
        public decimal Carbohydrates { get; set; }
        public decimal Fats { get; set; }
        public string PictureUrl { get; set; }
        public string Type { get; set; }
    }
}