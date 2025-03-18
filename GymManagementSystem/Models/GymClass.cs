namespace GymManagementSystem.Models
{
    public class GymClass
    {
        public int Id { get; set; }         // Primary key
        public string Name { get; set; }    // Class name
        public DateTime Time { get; set; }  // Class time
        public string Instructor { get; set; } // Instructor name
        public string Department { get; set; } // Department (e.g., Yoga, Pilates, etc.)

       
    }
}
