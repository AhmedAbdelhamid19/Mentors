using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElMentors.Models.Tests
{
    public class Test1
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<MidTest>? mids { get; set; } = new HashSet<MidTest>();
    }
    public class MidTest
    {
        public int Test1Id { get; set; }
        public Test1? Test1 { get; set; }
        public int Test2Id { get; set; }
        public Test2? Test2 { get; set; }
    }
    public class Test2
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<MidTest>? mids { get; set; } = new HashSet<MidTest>();
    }


    public abstract class parent
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }

    public class child1 : parent
    {
        public string? Child1Property { get; set; }
    }

    public class child2 : parent
    {
        public string? Child2Property { get; set; }
    }
}