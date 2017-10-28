using Enexure.MicroBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGrades.Commands
{
    public class OpenGradeBook : ICommand
    {
        public string CourseNumber { get; set; }
        public string CourseName { get; set; }
        public OpenGradeBook(string courseNumber, string courseName)
        {
            CourseNumber = courseNumber;
            CourseName = courseName;
        }
    }
    public class AddStudent : ICommand
    {
        public string CourseNumber { get; set; }
        private Guid StudentId;
        private string FirstName;
        private string LastName;

        public AddStudent(string courseNumber, Guid guid, string first, string last)
        {
            CourseNumber = courseNumber;
            StudentId = guid;
            FirstName = first;
            LastName = last;
        }
    }
    public class RemoveStudent : ICommand
    {
    }
    public class EnterGrade : ICommand
    {
    }
}
