using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class MealRepository : IMealRepository
    {
        private readonly DataContext _context;
        public MealRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Meal> GetMealByIdAsync(int id)
        {
            return await _context.Meals
                .Include(p => p.MealType)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IReadOnlyList<Meal>> GetMealsAsync()
        {
            return await _context.Meals
                .Include(p => p.MealType)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<MealType>> GetMealTypesAsync()
        {
            return await _context.MealTypes.ToListAsync();
        }
    }
}