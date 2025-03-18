namespace GymManagementSystem.Models
{
    public class Subscription
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Plan { get; set; }
        public string Status { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
