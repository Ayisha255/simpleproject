using GymManagementSystem.Data;
using GymManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GymManagementSystem.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Admin Dashboard (Index)
        public IActionResult Index()
        {
            return View();
        }

        // Manage Users - List all users
        public async Task<IActionResult> ManageUsers()
        {
            var users = await _context.Users.ToListAsync();
            return View(users);
        }

        // Create User (GET)
        public IActionResult CreateUser()
        {
            return View();
        }

        // Create User (POST)
        [HttpPost]
        public async Task<IActionResult> CreateUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            // Check if the username exists
            var existingUser = await _context.Users
                                              .FirstOrDefaultAsync(u => u.Username == user.Username);
            if (existingUser != null)
            {
                ModelState.AddModelError("", "Username already exists.");
                return View(user);
            }

            // Add new user
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return RedirectToAction("ManageUsers");
        }

        // Edit User (GET)
        public async Task<IActionResult> EditUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // Edit User (POST)
        [HttpPost]
        public async Task<IActionResult> EditUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                _context.Update(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("ManageUsers");
            }
            return View(user);
        }

        // Delete User
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return RedirectToAction("ManageUsers");
        }





        


        



        public IActionResult CreateGymClass()
        {
            // Ensure the model is not null when rendering the view
            var gymClass = new GymClass(); // Initialize the model
            return View(gymClass); // Pass the model to the view
        }



        // Create Gym Class (POST) - Handle the form submission
        [HttpPost]
        public async Task<IActionResult> CreateGymClass(GymClass gymClass)
        {
            if (gymClass == null)
            {
                return BadRequest(); // Handle the case where the model is null
            }

            // Save the gym class or handle any logic here
            _context.GymClasses.Add(gymClass);
            await _context.SaveChangesAsync();

            return RedirectToAction("ManageGymClasses");
        }

        
        // Manage Gym Classes - List all gym classes
        public async Task<IActionResult> ManageGymClasses()
        {
            var gymClasses = await _context.GymClasses.ToListAsync();
            return View(gymClasses); // Pass gym classes list to the view
        }



        public async Task<IActionResult> EditGymClass(int id)
        {
            var gymClass = await _context.GymClasses.FindAsync(id);
            if (gymClass == null)
            {
                return NotFound();
            }
            return View(gymClass); // Pass the gym class data to the view
        }


        [HttpPost]
        public async Task<IActionResult> EditGymClass(int id, GymClass gymClass)
        {
            if (id != gymClass.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gymClass); // Update gym class data
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GymClassExists(gymClass.Id)) // Handle concurrency issues if the class is no longer in the database
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(ManageGymClasses)); // Redirect back to the list of gym classes after successful update
            }
            return View(gymClass); // Return the view with the original model if there's any validation issue
        }

        private bool GymClassExists(int id)
        {
            return _context.GymClasses.Any(e => e.Id == id);
        }


        // Delete Gym Class
        public async Task<IActionResult> DeleteGymClass(int id)
        {
            var gymClass = await _context.GymClasses.FindAsync(id);
            if (gymClass == null)
            {
                return NotFound();
            }

            _context.GymClasses.Remove(gymClass);
            await _context.SaveChangesAsync();

            return RedirectToAction("ManageGymClasses");
        }



          // Manage Membership Plans - List all plans
        public async Task<IActionResult> ManageMembershipPlans()
        {
            var plans = await _context.MembershipPlans.ToListAsync();
            return View(plans);  // Display all membership plans
        }

        // Create Membership Plan (GET)
        public IActionResult CreateMembershipPlan()
        {
            return View();
        }

        // Create Membership Plan (POST)
        [HttpPost]
        public async Task<IActionResult> CreateMembershipPlan(MembershipPlan plan)
        {
            if (ModelState.IsValid)
            {
                _context.MembershipPlans.Add(plan);  // Add the new plan
                await _context.SaveChangesAsync();   // Save to the database

                return RedirectToAction(nameof(ManageMembershipPlans));  // Redirect back to the plans management view
            }

            return View(plan);  // Return the view with validation errors if any
        }

        // Edit Membership Plan (GET)
        public async Task<IActionResult> EditMembershipPlan(int id)
        {
            var plan = await _context.MembershipPlans.FindAsync(id);
            if (plan == null)
            {
                return NotFound();
            }

            return View(plan);  // Pass the existing plan to the view
        }

        // Edit Membership Plan (POST)
        [HttpPost]
        public async Task<IActionResult> EditMembershipPlan(int id, MembershipPlan plan)
        {
            if (id != plan.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                _context.Update(plan);  // Update the plan
                await _context.SaveChangesAsync();  // Save changes to the database

                return RedirectToAction(nameof(ManageMembershipPlans));  // Redirect to plans management
            }

            return View(plan);  // Return the view with validation errors if any
        }

        // Delete Membership Plan
        public async Task<IActionResult> DeleteMembershipPlan(int id)
        {
            var plan = await _context.MembershipPlans.FindAsync(id);
            if (plan == null)
            {
                return NotFound();
            }

            _context.MembershipPlans.Remove(plan);  // Remove the plan from the database
            await _context.SaveChangesAsync();  // Save changes to the database

            return RedirectToAction(nameof(ManageMembershipPlans));  // Redirect to plans management
        }

    }
}

