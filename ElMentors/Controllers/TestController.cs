using Microsoft.AspNetCore.Mvc;
using ElMentors.Models.Tests;
using ElMentors.Models.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

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
            Test1 test1 = new Test1();
            context.Add(test1);
            test1.test2 = new Test2();

            context.SaveChanges();
			return Ok("added test2 to test1");
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