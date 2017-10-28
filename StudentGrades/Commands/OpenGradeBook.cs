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
        private Guid Id;
        private string FirstName;
        private string LastName;

        public AddStudent(Guid guid, string first, string last)
        {
            this.Id = guid;
            this.FirstName = first;
            this.LastName = last;
        }
    }
    public class RemoveStudent : ICommand
    {
    }
    public class EnterGrade : ICommand
    {
    }
}
