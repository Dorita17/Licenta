using Core.Entities;

namespace Core.Specifications
{
    public class MealsWithTypesSpecification : BaseSpecification<Meal>
    {
        public MealsWithTypesSpecification(MealSpecParams mealParams)
            : base(x =>
                (string.IsNullOrEmpty(mealParams.Search) || x.Name.ToLower().Contains(mealParams.Search)) && 
                (!mealParams.TypeId.HasValue || x.MealTypeId == mealParams.TypeId)
            )
        {
            AddInclude(x => x.MealType);
            AddOrderBy(x => x.Name);
            ApplyPaging(mealParams.PageSize * (mealParams.PageIndex - 1), mealParams.PageSize);

            if (!string.IsNullOrEmpty(mealParams.Sort))
            {
                switch (mealParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }
        }

        public MealsWithTypesSpecification(int id) 
            : base(x => x.Id == id)
        {
            AddInclude(x => x.MealType);
        }
    }
}