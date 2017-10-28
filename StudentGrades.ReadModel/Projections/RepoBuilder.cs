using Enexure.MicroBus;
using StudentGrades.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGrades.ReadModel.Projections
{
    public class RepoBuilder
        : IEventHandler<GradeBookOpened>
    {
        public async Task Handle(GradeBookOpened e)
        {
            GradeBookRepo.Db.Add(e.CourseNumber, new GradeBookRepo.GradeBookTable());
        }
    }
}
