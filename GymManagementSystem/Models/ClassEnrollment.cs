namespace GymManagementSystem.Models
{
    public class ClassEnrollment
    {
        public int Id { get; set; }         // Primary key
        public int UserId { get; set; }     // Foreign key to User
        public int ClassId { get; set; }    // Foreign key to GymClass
        public DateTime EnrollmentDate { get; set; }

        public virtual User User { get; set; }   // Navigation property to User
        public virtual GymClass GymClass { get; set; } // Navigation property to GymClass
    }
}
