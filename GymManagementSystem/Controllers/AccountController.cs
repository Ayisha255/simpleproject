using GymManagementSystem.Data;
using GymManagementSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GymManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        public async Task<IActionResult> Login(User loginUser, string action)
        {
            // Ensure the model is not null
            if (loginUser == null)
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View();
            }

            if (action == "LoginAsAdmin")
            {
                var admin = await _context.Admins
                                          .FirstOrDefaultAsync(a => a.Username == loginUser.Username && a.Password == loginUser.Password);
                if (admin != null)
                {
                    // Set session for admin role
                    HttpContext.Session.SetString("Role", "Admin");

                    // Redirect to Admin Dashboard
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Admin Credentials.");
                }
            }
            else if (action == "LoginAsUser")
            {
                var user = await _context.Users
                                          .FirstOrDefaultAsync(u => u.Username == loginUser.Username && u.Password == loginUser.Password);
                if (user != null)
                {
                    // Set session for user role and user ID
                    HttpContext.Session.SetString("Role", "User");
                    HttpContext.Session.SetString("UserId", user.Id.ToString());  // Store UserId in session

                    // Redirect to User Dashboard
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid User Credentials.");
                }
            }

            return View(loginUser);
        }

        // GET: Register
        [HttpGet]
        public IActionResult Register()
        {
            return View(new User()); // Ensure an empty model is passed to the view
        }

        // POST: Register
        [HttpPost]
        public async Task<IActionResult> Register(User registerUser)
        {
            if (!ModelState.IsValid)
            {
                return View(registerUser); // Pass back the model with validation errors
            }

            // Check if the username already exists
            var existingUser = await _context.Users
                                              .FirstOrDefaultAsync(u => u.Username == registerUser.Username);
            if (existingUser != null)
            {
                ModelState.AddModelError("", "Username already exists.");
                return View(registerUser); // Return the model with error messages
            }

            // Register the user
            _context.Users.Add(registerUser);
            await _context.SaveChangesAsync();

            return RedirectToAction("Login");
        }

        // Logout method
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Clear session
            return RedirectToAction("Login"); // Redirect to login page
        }
    }
}



















//using GymManagementSystem.Data;
//using GymManagementSystem.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using System.Linq;
//using System.Threading.Tasks;

//namespace GymManagementSystem.Controllers
//{
//    public class AccountController : Controller
//    {
//        private readonly ApplicationDbContext _context;

//        public AccountController(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        // GET: Login
//        [HttpGet]
//        public IActionResult Login()
//        {
//            return View();
//        }

//        // POST: Login
//        [HttpPost]
//        public async Task<IActionResult> Login(User loginUser, string action)
//        {
//            // Ensure the model is not null
//            if (loginUser == null)
//            {
//                ModelState.AddModelError("", "Invalid login attempt.");
//                return View();
//            }

//            if (action == "LoginAsAdmin")
//            {
//                var admin = await _context.Admins
//                                          .FirstOrDefaultAsync(a => a.Username == loginUser.Username && a.Password == loginUser.Password);
//                if (admin != null)
//                {
//                    // Set session for admin role
//                    HttpContext.Session.SetString("Role", "Admin");

//                    // Redirect to Admin Dashboard
//                    return RedirectToAction("Index", "Admin");
//                }
//                else
//                {
//                    ModelState.AddModelError("", "Invalid Admin Credentials.");
//                }
//            }
//            else if (action == "LoginAsUser")
//            {
//                var user = await _context.Users
//                                          .FirstOrDefaultAsync(u => u.Username == loginUser.Username && u.Password == loginUser.Password);
//                if (user != null)
//                {
//                    // Set session for user role
//                    HttpContext.Session.SetString("Role", "User");

//                    // Redirect to User Dashboard
//                    return RedirectToAction("Index", "User");
//                }
//                else
//                {
//                    ModelState.AddModelError("", "Invalid User Credentials.");
//                }
//            }

//            return View(loginUser);
//        }

//        // GET: Register
//        [HttpGet]
//        public IActionResult Register()
//        {
//            return View(new User()); // Ensure an empty model is passed to the view
//        }

//        // POST: Register
//        [HttpPost]
//        public async Task<IActionResult> Register(User registerUser)
//        {
//            if (!ModelState.IsValid)
//            {
//                return View(registerUser); // Pass back the model with validation errors
//            }

//            // Check if the username already exists
//            var existingUser = await _context.Users
//                                              .FirstOrDefaultAsync(u => u.Username == registerUser.Username);
//            if (existingUser != null)
//            {
//                ModelState.AddModelError("", "Username already exists.");
//                return View(registerUser); // Return the model with error messages
//            }

//            // Register the user
//            _context.Users.Add(registerUser);
//            await _context.SaveChangesAsync();

//            return RedirectToAction("Login");
//        }

//        // Logout method
//        public IActionResult Logout()
//        {
//            HttpContext.Session.Clear(); // Clear session
//            return RedirectToAction("Login"); // Redirect to login page
//        }
//    }
//}
