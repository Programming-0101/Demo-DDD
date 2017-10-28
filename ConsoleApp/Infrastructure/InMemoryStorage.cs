using Enexure.MicroBus;
using StudentGrades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Infrastructure
{
    public class InMemoryStorage : IRecall
    {
        private static IDictionary<string, IEnumerable<IEvent>> Events = new Dictionary<string, IEnumerable<IEvent>>();
        public IEnumerable<string> Keys { get { return Events.Keys; } }

        public void AppendToHistory(string key, IEnumerable<IEvent> events)
        {
            // if the key does not exist, then create the key and add the events as new events
            if (Events.ContainsKey(key))
            {
                var existing = new List<IEvent>(Events[key]);
                existing.AddRange(events);
                Events[key] = existing;
                Console.ForegroundColor = ConsoleColor.Gray;
                foreach (var item in events)
                    Console.WriteLine($"\t{item}");
                Console.ResetColor();
            }
            else
            {
                Events.Add(key, events);
            }
        }

        public IEnumerable<IEvent> LoadEventHistory(string key)
        {
            if (Events.ContainsKey(key))
            {
                return Events[key];
            }
            return new List<IEvent>();
        }
    }
}
