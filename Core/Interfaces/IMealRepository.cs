using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IMealRepository
    {
        Task<Meal> GetMealByIdAsync(int id);
        Task<IReadOnlyList<Meal>> GetMealsAsync();
        Task<IReadOnlyList<MealType>> GetMealTypesAsync();

    }
}