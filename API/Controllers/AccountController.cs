using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.DTOs;
using API.Errors;
using API.Extensions;
using AutoMapper;
using Core.Entities.Identity;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AccountController : BaseController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService,
        IMapper mapper)
        {
            _mapper = mapper;
            _tokenService = tokenService;
            _signInManager = signInManager;
            _userManager = userManager;
        }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<UserDto>> GetCurrentUser()
    {
        var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

        var user = await _userManager.FindByEmailAsync(email);

        return new UserDto
        {
            Email = user.Email,
            Token = _tokenService.CreateToken(user),
            DisplayName = user.DisplayName,
            Weight = user.Weight,
            Height = user.Height,
            Gender = user.Gender,
            DateOfBirth = user.DateOfBirth.ToString("MM-dd-yyyy"),
            ActivityCost = user.ActivityCost,
            DailyCalories = user.DailyCalories,
            DailyProteins = user.DailyProteins,
            DailyCarbohydrates = user.DailyCarbohydrates,
            DailyFats = user.DailyFats
        };
    }

    [HttpGet("emailexists")]
    public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
    {
        return await _userManager.FindByEmailAsync(email) != null;
    }

    [Authorize]
    [HttpGet("address")]
    public async Task<ActionResult<AddressDto>> GetUserAddress()
    {
        var user = await _userManager.FindUserByClaimsPrincipalWithAddressAsync(HttpContext.User);

        return _mapper.Map<Address, AddressDto>(user.Address);
    }

    [Authorize]
    [HttpPut("address")]
    public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto address)
    {
        var user = await _userManager.FindUserByClaimsPrincipalWithAddressAsync(HttpContext.User);

        user.Address = _mapper.Map<AddressDto, Address>(address);

        var result = await _userManager.UpdateAsync(user);

        if (result.Succeeded) return Ok(_mapper.Map<Address, AddressDto>(user.Address));

        return BadRequest("Problem updating the user");
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);

        if (user == null) return Unauthorized(new ApiResponse(401));

        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

        if (!result.Succeeded) return Unauthorized(new ApiResponse(401));

        return new UserDto
        {
            Email = user.Email,
            Token = _tokenService.CreateToken(user),
            DisplayName = user.DisplayName,
            Weight = user.Weight,
            Height = user.Height,
            Gender = user.Gender,
            DateOfBirth = user.DateOfBirth.ToString("MM-dd-yyyy"),
            ActivityCost = user.ActivityCost,
            DailyCalories = user.DailyCalories,
            DailyProteins = user.DailyProteins,
            DailyCarbohydrates = user.DailyCarbohydrates,
            DailyFats = user.DailyFats
        };
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        if(CheckEmailExistsAsync(registerDto.Email).Result.Value)
        {
            return new BadRequestObjectResult(new ApiValidationErrorResponse{Errors = new []{"Email address is in use"}});
        }

        //calculate the user's calories, proteins, carbohydrates and fats
        var userCalories = 0M;
        var userProteins = 0M;
        var userCarbohydrates = 0M;
        var userFats = 0M;
        var userAge = DateTime.Now.Year - DateTime.Parse(registerDto.DateOfBirth).Year;

        if (registerDto.Gender == "male")
        {
            var RMB = (long)(10 * registerDto.Weight + (decimal)6.25 * registerDto.Height - 5 * userAge + 5);
            userProteins = (long)((decimal) 2.2 * registerDto.Weight);
            userFats = (long)(((decimal) 0.3 * RMB)/9);

            var proteinInCalories = userProteins * 4;
            var fatsInCalories = userFats * 9;

            userCalories = (long)(RMB * registerDto.ActivityCost);

            userCarbohydrates = (long)((userCalories - (proteinInCalories + fatsInCalories))/4);

        }
        else 
        {
            var RMB = (long)(10 * registerDto.Weight + (decimal) 6.25 * registerDto.Height - 5 * userAge - 161);

            userProteins = (long)((decimal) 2.2 * registerDto.Weight);
            userFats = (long)(((decimal) 0.3 * RMB)/9);

            var proteinInCalories = userProteins * 4;
            var fatsInCalories = userFats * 9;

            userCalories = (long)(RMB * registerDto.ActivityCost);

            userCarbohydrates = (long)((userCalories - (proteinInCalories + fatsInCalories))/4); 

        }

        var user = new AppUser
        {
            DisplayName = registerDto.DisplayName,
            Email = registerDto.Email,
            UserName = registerDto.Email,
            Weight = registerDto.Weight,
            Height = registerDto.Height,
            Gender = registerDto.Gender,
            DateOfBirth = DateTime.Parse(registerDto.DateOfBirth),
            ActivityCost = registerDto.ActivityCost,
            DailyCalories = userCalories,
            DailyProteins = userProteins,
            DailyCarbohydrates = userCarbohydrates,
            DailyFats = userFats
        };

        var result = await _userManager.CreateAsync(user, registerDto.Password);

        if (!result.Succeeded) return BadRequest(new ApiResponse(400));


        return new UserDto
        {
            Email = user.Email,
            Token = _tokenService.CreateToken(user),
            DisplayName = user.DisplayName,
            Weight = user.Weight,
            Height = user.Height,
            Gender = user.Gender,
            DateOfBirth = user.DateOfBirth.ToString("MM-dd-yyyy"),
            ActivityCost = user.ActivityCost,
            DailyCalories = user.DailyCalories,
            DailyProteins = user.DailyProteins,
            DailyCarbohydrates = user.DailyCarbohydrates,
            DailyFats = user.DailyFats
        };
    }
}
}