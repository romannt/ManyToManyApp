using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManyToManyApp.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        // Ключевое слово virtual означает, что нужно использовать "Ленивую загрузку"
        public virtual ICollection<Student> Students { get; set; }
        public Course()
        {
            Students = new List<Student>();
        }

    }
}