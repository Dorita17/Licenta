namespace Core.Entities
{
    public class Meal : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Grams { get; set; }
        public decimal Calories { get; set; }
        public decimal Proteins { get; set; }
        public decimal Carbohydrates { get; set; }
        public decimal Fats { get; set; }
        public string PictureUrl { get; set; }

        //Relationships
        public MealType MealType { get; set; }
        public int MealTypeId { get; set; }
    }
}