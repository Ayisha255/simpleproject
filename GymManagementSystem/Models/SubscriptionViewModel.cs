namespace GymManagementSystem.Models
{
    public class SubscriptionViewModel
    {
       
        public int UserId { get; set; }  // Automatically passed
        public string Plan { get; set; } // Plan chosen by the user
        public string Status { get; set; } // Status of subscription (e.g., Active/Inactive)
    }
}
