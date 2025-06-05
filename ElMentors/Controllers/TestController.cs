using Microsoft.AspNetCore.Mvc;
using ElMentors.Models.Tests;
using ElMentors.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace ElMentors.Controllers
{
    [Route("[controller]")]
    public class TestController : Controller
    {
        public Context context { get; set; }
        public TestController(Context context)
        {
            this.context = context;
        }

        [HttpGet("test1")]
        public IActionResult Test1()
        {
             var query = context.Topic.Where(t => t.Id == 1).ToList();
            int x = 1;

            return Ok(query);
        }
    }
}

/*
var student = new Student { Name = "Ahmed" };
var course = new Course { Title = "Math" };
var studentCourse = new StudentCourse
{
    Student = student,
    Course = course
};
context.StudentCourses.Add(studentCourse);
context.SaveChanges();
*/