using StudentGrades.Aggregates;
using StudentGrades.BusinessExceptions;
using StudentGrades.DomainModels;
using StudentGrades.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StudentGrades.Specs
{
    public class When_GradeBook_Not_Started
    {
        #region Primary Scenarios
        [Fact]
        public void Should_Open_GradeBook()
        {
            // Arrange
            const string expectedCourseNumber = "PROG-101";
            const string expectedCourseName = "OOP Fundamentals";
            var sut = new GradeBook();
            
            // Act
            sut.OpenGradeBook(expectedCourseNumber, expectedCourseName);

            // Assert
            var changes = sut.Changes;
            Assert.Equal(1, changes.Count);
            Assert.IsType<GradeBookOpened>(changes[0]);
            var e = changes[0] as GradeBookOpened;
            Assert.Equal(expectedCourseNumber, e.CourseNumber);
            Assert.Equal(expectedCourseName, e.CourseName);
        }
        #endregion

        #region Alternate Scenarios
        [Fact]
        public void Should_Not_Add_Student()
        {
            // Arrange
            var sut = new GradeBook();

            // Act/Assert
            var err = Assert.Throws<GradeBookUnavailableException>(() => sut.AddStudent(new Student { StudentId = "20170001", FirstName = "Bob", LastName = "Jones" }));
            Assert.Equal("Grade Book has not been opened/started", err.Message);
        }

        [Fact]
        public void Should_Not_Remove_Student()
        {
            // Arrange
            var sut = new GradeBook();

            // Act/Assert
            var err = Assert.Throws<GradeBookUnavailableException>(() => sut.RemoveStudent(new Student { StudentId = "20170001", FirstName = "Bob", LastName = "Jones" }));
            Assert.Equal("Grade Book has not been opened/started", err.Message);
        }

        [Fact]
        public void Should_Not_Add_Mark()
        {
            // Arrange
            var sut = new GradeBook();

            // Act/Assert
            var err = Assert.Throws<GradeBookUnavailableException>(() => sut.AddMark(new StudentMark { StudentId = "20170001", EvaluationItem = "Quiz 1", TotalPossible = 25.0, Earned = 20.0 }));
            Assert.Equal("Grade Book has not been opened/started", err.Message);
        }

        [Fact]
        public void Should_Not_Correct_Mark()
        {
            // Arrange
            var sut = new GradeBook();

            // Act/Assert
            var err = Assert.Throws<GradeBookUnavailableException>(() => sut.CorrectMark(new StudentMark { StudentId = "20170001", EvaluationItem = "Quiz 1", TotalPossible = 25.0, Earned = 22.5 }, "Corrected for question # 5"));
            Assert.Equal("Grade Book has not been opened/started", err.Message);
        }
        #endregion
    }
}
