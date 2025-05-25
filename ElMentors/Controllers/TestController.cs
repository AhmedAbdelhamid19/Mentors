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
            Test1 t1 = new Test1() { Name = "Test1" };
            Test2 t2 = new Test2() { Name = "Test2" };
            MidTest mt = new MidTest()
            {
                Test1 = t1,
                Test2 = t2
            };

            context.MidTests.Add(mt);
            context.SaveChanges();
            return Ok(t1);
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