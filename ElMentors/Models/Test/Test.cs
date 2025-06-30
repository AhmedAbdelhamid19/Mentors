using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElMentors.Models.Tests
{
    public class Test1
    {
        public int Id { get; set; }

        [ForeignKey("test2")]
        public int Test2Id { get; set; }
		public virtual Test2 test2 { get; set; }
    }
    public class Test2
    {
        public int Id { get; set; }
        public virtual Test1 test1 { get; set; }
    }
}