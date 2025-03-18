namespace GymManagementSystem.Models
{
    public class UserMembershipPlan
    {
        public int Id { get; set; }  // Primary Key

        public int UserId { get; set; }  // Foreign Key to User
        public User User { get; set; }

        public int MembershipPlanId { get; set; }  // Foreign Key to MembershipPlan
        public MembershipPlan MembershipPlan { get; set; }

        public DateTime EnrollmentDate { get; set; }  // Date of enrollment
    }
}
