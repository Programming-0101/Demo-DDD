using Enexure.MicroBus;
using StudentGrades.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGrades.Commands.Handlers
{
    public sealed class GradingService
        : ICommandHandler<OpenGradeBook>
        , ICommandHandler<AddStudent>
        , ICommandHandler<RemoveStudent>
        , ICommandHandler<EnterGrade>
    {
        #region Constructor and private "plumbing"
        private readonly IRecall ES;
        private readonly IMicroBus Publisher;
        public GradingService(IRecall eventStore, IMicroBus publisher)
        {
            ES = eventStore;
            Publisher = publisher;
        }

        private void Update(string id, Action<GradeBook> execute)
        {
            // Load event stream from the store
            var stream = ES.LoadEventHistory(id);
            // create new aggregate from the history
            var ar = new GradeBook(stream);
            // execute delegated action
            execute(ar);
            // append resulting changes to the stream
            ES.AppendToHistory(id, ar.Changes); // stream.Version, ar.Changes);
            foreach (var change in ar.Changes)
                Publisher.PublishAsync(change);
        }
        #endregion

        #region Specific Command Handlers
        public async Task Handle(OpenGradeBook command)
        {
            Update(command.CourseNumber, ar => ar.OpenGradeBook(command.CourseNumber, command.CourseName));
        }

        public Task Handle(AddStudent command)
        {
            throw new NotImplementedException();
        }

        public Task Handle(RemoveStudent command)
        {
            throw new NotImplementedException();
        }

        public Task Handle(EnterGrade command)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
