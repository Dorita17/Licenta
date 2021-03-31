using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class MealsController : BaseController
    {
        private readonly IGenericRepository<Meal> _mealsRepo;
        private readonly IGenericRepository<MealType> _mealTypesRepo;
        private readonly IMapper _mapper;

        public MealsController(IGenericRepository<Meal> mealsRepo, IGenericRepository<MealType> mealTypesRepo,
        IMapper mapper)
        {
            _mapper = mapper;
            _mealTypesRepo = mealTypesRepo;
            _mealsRepo = mealsRepo;

        }

    [HttpGet]
    public async Task<ActionResult<Pagination<MealToReturnDto>>> GetMeals([FromQuery] MealSpecParams mealParams)
    {
        var spec = new MealsWithTypesSpecification(mealParams);
        var countSpec = new MealWithFiltersForCountSpecification(mealParams);
        var totalItems = await _mealsRepo.CountAsync(countSpec);
        var meals = await _mealsRepo.ListAsync(spec);
        var data = _mapper.Map<IReadOnlyList<Meal>, IReadOnlyList<MealToReturnDto>>(meals); 

        return Ok(new Pagination<MealToReturnDto>(mealParams.PageIndex, mealParams.PageSize, totalItems, data));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MealToReturnDto>> GetMeal(int id)
    {
        var spec = new MealsWithTypesSpecification(id);
        var meal = await _mealsRepo.GetEntityWithSpec(spec);

        if (meal == null) return NotFound(new ApiResponse(404));

        return _mapper.Map<Meal, MealToReturnDto>(meal);
    }

    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<MealType>>> GetMealTypes()
    {
        return Ok(await _mealTypesRepo.ListAllAsync());
    }
}
}