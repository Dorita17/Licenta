using System;

namespace API.DTOs
{
    public class UserDto
    {
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string Token { get; set; }
        public decimal Weight { get; set; }
        public decimal Height { get; set; }
        public string Gender { get; set; }
        public string DateOfBirth { get; set; }
        public decimal ActivityCost { get; set; }
        public decimal DailyCalories { get; set; }
        public decimal DailyProteins { get; set; }
        public decimal DailyCarbohydrates { get; set; }
        public decimal DailyFats { get; set; }
    }
}