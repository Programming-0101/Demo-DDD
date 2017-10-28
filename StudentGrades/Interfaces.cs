using Enexure.MicroBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentGrades
{
    /// <summary>
    /// IRecall describes the interface to load and save events for an aggregate
    /// </summary>
    public interface IRecall
    {
        IEnumerable<IEvent> LoadEventHistory(string key);
        void AppendToHistory(string key, IEnumerable<IEvent> events);
    }
}
