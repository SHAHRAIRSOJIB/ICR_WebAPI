using ICR_WEB_API.Service.BLL.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ICR_DashBoard.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserRepo _userRepo;
        public LoginController(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> VerifyUser(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                ViewBag.Message = "Please provide both username and password.";
                return View("Index");  // Show the same login page with the message.
            }

            var response = await _userRepo.VerifyUser(userName, password);

            if (response != null)
            {
                return RedirectToAction("Create", "Question"); // Login successful
            }
            else
            {
                ViewBag.Message = "Insert Valid Credentials";
                return View("Index");  // Stay on login page and show message
            }
        }

    }
}
