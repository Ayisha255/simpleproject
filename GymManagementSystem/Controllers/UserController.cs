using GymManagementSystem.Data;
using GymManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GymManagementSystem.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
           
           

            return View();
        }






       




        // View Gym Classes
        public async Task<IActionResult> ViewGymClasses()
        {
            var gymClasses = await _context.GymClasses.ToListAsync();
            return View(gymClasses); // Display available gym classes
        }


       


        public async Task<IActionResult> EnrollInClass(int classId)
        {
            // Get the user ID from session (ensure this is set correctly)
            var userId = int.Parse(HttpContext.Session.GetString("UserId"));

            // Check if the gym class exists
            var gymClass = await _context.GymClasses.FindAsync(classId);

            if (gymClass == null)
            {
                TempData["Error"] = "Gym class not found.";
                return RedirectToAction("ViewGymClasses");
            }

            // Check if the user is already enrolled in this class
            var existingEnrollment = await _context.ClassEnrollments
                                                   .FirstOrDefaultAsync(ce => ce.UserId == userId && ce.ClassId == classId);

            if (existingEnrollment != null)
            {
                TempData["Error"] = "You are already enrolled in this class.";
                return RedirectToAction("ViewClassEnrollment");
            }

            // Create a new enrollment if no existing enrollment is found
            var enrollment = new ClassEnrollment
            {
                UserId = userId,    // Current user ID
                ClassId = classId,  // Selected class ID
                EnrollmentDate = DateTime.Now // Enrollment date
            };

            // Add enrollment to the database
            _context.ClassEnrollments.Add(enrollment);

            try
            {
                await _context.SaveChangesAsync(); // Save changes to the database
            }
            catch (DbUpdateException ex)
            {
                // Log the error message or handle the exception
                TempData["Error"] = "An error occurred while saving your enrollment.";
                return RedirectToAction("ViewGymClasses");
            }

            // Redirect to the view where the user can see their enrollments
            return RedirectToAction("ViewClassEnrollment");
        }


        public async Task<IActionResult> ViewClassEnrollment()
        {
            // Retrieve user ID from session
            var userId = int.Parse(HttpContext.Session.GetString("UserId"));

            // Retrieve all class enrollments for the current user
            var enrollments = await _context.ClassEnrollments
                                             .Where(ce => ce.UserId == userId)
                                             .Include(ce => ce.GymClass) // Include GymClass data
                                             .ToListAsync();

            // Pass the enrollments to the view
            return View(enrollments);
        }



        

            // View All Membership Plans (GET)
            public async Task<IActionResult> ViewMembershipPlans()
            {
                var membershipPlans = await _context.MembershipPlans.ToListAsync();
                return View(membershipPlans);  // Display all available membership plans
            }

            // Enroll in a Membership Plan (POST)
            public async Task<IActionResult> EnrollInPlan(int planId)
            {
                var userId = int.Parse(HttpContext.Session.GetString("UserId")); // Get user ID from session

                // Find the selected membership plan
                var membershipPlan = await _context.MembershipPlans.FindAsync(planId);
                if (membershipPlan == null)
                {
                    TempData["Error"] = "Membership plan not found.";
                    return RedirectToAction("ViewMembershipPlans");
                }

                // Check if the user is already enrolled in the plan
                var existingEnrollment = await _context.UserMembershipPlans
                    .FirstOrDefaultAsync(up => up.UserId == userId && up.MembershipPlanId == planId);

                if (existingEnrollment != null)
                {
                    TempData["Error"] = "You are already enrolled in this plan.";
                    return RedirectToAction("ViewMembershipPlans");
                }

                // Create a new enrollment
                var enrollment = new UserMembershipPlan
                {
                    UserId = userId,
                    MembershipPlanId = planId,
                    EnrollmentDate = DateTime.Now
                };

                // Add the new enrollment and save changes
                _context.UserMembershipPlans.Add(enrollment);
                await _context.SaveChangesAsync();

                TempData["Success"] = "You have successfully enrolled in the membership plan!";
                return RedirectToAction("ViewMembershipPlans");
            }

            // View User's Current Membership Plan (GET)
            public async Task<IActionResult> ViewUserPlan()
            {
                var userId = int.Parse(HttpContext.Session.GetString("UserId"));
                var enrollment = await _context.UserMembershipPlans
                    .Where(up => up.UserId == userId)
                    .Include(up => up.MembershipPlan)
                    .FirstOrDefaultAsync();

                return View(enrollment);  // Display the user's enrolled membership plan
            }



        }
}
