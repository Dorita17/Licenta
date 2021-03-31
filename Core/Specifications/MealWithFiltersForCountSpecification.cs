using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications
{
    public class MealWithFiltersForCountSpecification : BaseSpecification<Meal>
    {
        public MealWithFiltersForCountSpecification(MealSpecParams mealParams) 
            : base(x => 
                (string.IsNullOrEmpty(mealParams.Search) || x.Name.ToLower().Contains(mealParams.Search)) && 
                (!mealParams.TypeId.HasValue || x.MealTypeId == mealParams.TypeId)
            )
        {
        }
    }
}