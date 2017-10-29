using Enexure.MicroBus;
using StudentGrades.BusinessExceptions;
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
        private readonly Records State;
        public GradeBook(params IEvent[] eventHistory)
        {
            State = new Records(eventHistory);
        }

        public void OpenGradeBook(string courseNumber, string courseName)
        {
            Changes.Add(new GradeBookOpened { CourseNumber = courseNumber, CourseName = courseName });
        }
        public void AddStudent(Student student)
        {
            if (!State.GradeBookStarted)
                throw new GradeBookUnavailableException("Grade Book has not been opened/started");
            Changes.Add(new StudentAdded { });
        }
        public void RemoveStudent(Student student)
        {
            if (!State.GradeBookStarted)
                throw new GradeBookUnavailableException("Grade Book has not been opened/started");
        }
        public void AddMark(StudentMark studentMark)
        {
            if (!State.GradeBookStarted)
                throw new GradeBookUnavailableException("Grade Book has not been opened/started");
        }
        public void CorrectMark(StudentMark studentMark, string reason)
        {
            if (!State.GradeBookStarted)
                throw new GradeBookUnavailableException("Grade Book has not been opened/started");
        }

        private class Records
        {
            public readonly bool GradeBookStarted;

            public Records(IEnumerable<IEvent> eventHistory)
            {
                
            }
        }
    }
}
