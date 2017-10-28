using Enexure.MicroBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGrades.Aggregates
{
    internal class GradeBook
    {
        public IEnumerable<IEvent> Changes { get; } = new List<IEvent>();
        public GradeBook(IEnumerable<IEvent> stream)
        {
        }

        public void OpenGradeBook(string courseNumber, string courseName)
        {

        }
    }
}
