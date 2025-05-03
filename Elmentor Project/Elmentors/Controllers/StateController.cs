using Microsoft.AspNetCore.Mvc;

namespace Elmentors.Controllers
{
    public class StateController : Controller
    {
        public IActionResult SetSession(string Name, int Age)
        {
            HttpContext.Session.SetString("Name", Name);
            HttpContext.Session.SetInt32("Age", Age);

            return Content("Session Safed");
        }
        public IActionResult GetSession()
        {
            string? Name = HttpContext.Session.GetString("Name");
            int? Age = HttpContext.Session.GetInt32("Age");
            return Content($"name = {Name}, Age = {Age} session");
        }

        public IActionResult SetCookies()
        {
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddDays(2); // For Presistent Cookies

            HttpContext.Response.Cookies.Append("Name", "Ahmed", options); // presistent Cookie (end with options ending time)
            HttpContext.Response.Cookies.Append("Age", "12"); // session Cookie (end with session ending)

            return Content("Cookes Saved");
        }

        public IActionResult GetCookies()
        {
            string? Name = HttpContext.Request.Cookies["Name"];
            string? Age = HttpContext.Request.Cookies["Age"];

            return Content($"name = {Name}, Age = {Age} cookies");
        }
    }
}
