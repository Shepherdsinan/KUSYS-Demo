using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        public List<StudentCourse> StudentCourse { get; set; } = new();

        public int? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}
