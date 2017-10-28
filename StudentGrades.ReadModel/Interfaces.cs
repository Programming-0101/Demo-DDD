using Enexure.MicroBus;
using StudentGrades.ReadModel.Projections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGrades.ReadModel
{
    public class GradeBookRepo
        : IQueryHandler<GetCourse, Course>
        , IQueryHandler<GetStudents, IEnumerable<Student>>
    {
        internal static IDictionary<string, GradeBookTable> Db { get; } = new Dictionary<string, GradeBookTable>();
        internal class GradeBookTable
        {
            public IEnumerable<Student> Students { get; } = new List<Student>();
        }

        public Task<Course> Handle(GetCourse query)
        {
            return null;
        }

        public Task<IEnumerable<Student>> Handle(GetStudents query)
        {
            var gradeBook = Db[query.CourseNumber];
            IEnumerable<Student> students = gradeBook.Students;
            return Task.FromResult(students);
        }
    }
    public class Course
    {
    }
    public class Student
    {
    }
    public class GetCourse : IQuery<GetCourse, Course>
    {
    }
    public class GetStudents : IQuery<GetStudents, IEnumerable<Student>>
    {
        public string CourseNumber { get; set; }
    }
}
