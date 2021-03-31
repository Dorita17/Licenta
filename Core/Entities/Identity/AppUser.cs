using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Core.Entities.Identity
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }
        public decimal Weight { get; set; }
        public decimal Height { get; set; }
        public string Gender { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        public decimal ActivityCost { get; set; }
        public decimal DailyCalories { get; set; }
        public decimal DailyProteins { get; set; }
        public decimal DailyCarbohydrates { get; set; }
        public decimal DailyFats { get; set; }

        //Relationships
        public Address Address { get; set; }
    }
}