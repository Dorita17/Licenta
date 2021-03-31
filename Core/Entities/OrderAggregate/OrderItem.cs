namespace Core.Entities.OrderAggregate
{
    public class OrderItem : BaseEntity
    {
        public OrderItem()
        {
        }

        public OrderItem(MealItemOrdered mealItemOrdered, decimal price, int quantity, int grams, decimal calories, decimal proteins, decimal carbohydrates, decimal fats)
        {
            MealItemOrdered = mealItemOrdered;
            Price = price;
            Quantity = quantity;
            Grams = grams;
            Calories = calories;
            Proteins = proteins;
            Carbohydrates = carbohydrates;
            Fats = fats;
        }

        public MealItemOrdered MealItemOrdered { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int Grams { get; set; }
        public decimal Calories { get; set; }
        public decimal Proteins { get; set; }
        public decimal Carbohydrates { get; set; }
        public decimal Fats { get; set; }
    }
}