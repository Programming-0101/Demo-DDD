using Enexure.MicroBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGrades.Events
{
    public class GradeBookOpened : IEvent
    {
        public string CourseNumber { get; set; }
        public string CourseName { get; set; }
    }
    public class StudentAdded : IEvent
    {
    }
}
