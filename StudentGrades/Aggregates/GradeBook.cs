using Enexure.MicroBus;
using StudentGrades.DomainModels;
using StudentGrades.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGrades.Aggregates
{
    internal class GradeBook
    {
        public IList<IEvent> Changes { get; } = new List<IEvent>();
        public GradeBook(IEnumerable<IEvent> stream)
        {
        }

        public void OpenGradeBook(string courseNumber, string courseName)
        {
            Changes.Add(new GradeBookOpened { CourseNumber = courseNumber });
        }
        public void AddStudent(Student student)
        {
            Changes.Add(new StudentAdded { });
        }
    }
}
