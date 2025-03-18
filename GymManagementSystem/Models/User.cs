using System.ComponentModel.DataAnnotations;

namespace GymManagementSystem.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string BloodGroup { get; set; }

        public string Department { get; set; }

        public ICollection<UserMembershipPlan> UserMembershipPlans { get; set; }
    }
}
