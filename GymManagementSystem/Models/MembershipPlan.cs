namespace GymManagementSystem.Models
{
    public class MembershipPlan
    {
        public int Id { get; set; }              // Primary key
        public string Name { get; set; }         // Plan name (e.g., Basic, Premium)
        public string Details { get; set; }      // Plan description (previously Description)
        public decimal Cost { get; set; }        // Price of the plan (previously Price)

        public ICollection<UserMembershipPlan> UserMembershipPlans { get; set; }
    }
}
