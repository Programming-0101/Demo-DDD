using Autofac;
using Autofac.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        #region Driver
        static void Main(string[] args)
        {
            var app = new Program();
            app.Run();
        }

        public Program()
        {
            // Set up DI container using Autofac
        }

        private void Run()
        {
            ClsWithTitle(false);
            string menuChoice = "";
            do
            {
                try
                {
                    DisplayMenu();
                    menuChoice = Console.ReadLine().ToUpper();
                    ProcessMenuChoice(menuChoice);
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ResetColor();
                }
                finally
                {
                    ClsWithTitle();
                }
            } while (menuChoice != "X");
        }

        void ClsWithTitle(bool pause = true)
        {
            if (pause)
            {
                Console.Write("\n\nPress [Enter] to continue");
                Console.ReadLine();
            }
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Demo Domain Driven Design - Applied");
            Console.WriteLine("===================================\n");
            Console.ResetColor();
        }

        void DisplayMenu()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("1 ) Open Grade Book:");
            Console.WriteLine("2 ) Add Student");
            Console.WriteLine("3 ) Remove Student");
            Console.WriteLine("4 ) Enter Grade");
            Console.WriteLine("5 ) Display Grades");
            Console.WriteLine("6 ) List Students");
            Console.WriteLine("X ) Exit");
            Console.ResetColor();
        }

        void ProcessMenuChoice(string choice)
        {
            switch (choice)
            {
                case "1":
                    OpenGradeBook();
                    break;
                case "2":

                    break;
                case "3":
                    break;
                case "4":
                    break;
                case "5":
                    break;
                case "6":
                    break;
                case "X":
                    Console.WriteLine("Thank you for trying out this demo.\n");
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\tInvalid menu choice");
                    Console.ResetColor();
                    break;
            }
        }
        #endregion

        #region UI
        void OpenGradeBook()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

#region Infrastructure
public class InMemoryStorage : IRecall
{
    private static IDictionary<string, IEnumerable<object>> Events = new Dictionary<string, IEnumerable<object>>();

    public void AppendToHistory(string key, IEnumerable<object> events)
    {
        // if the key does not exist, then create the key and add the events as new events
    }

    public IEnumerable<object> LoadEventHistory(string key)
    {
        // if the key does not exist, then return an empty list;
        return new List<object>();
    }
}

public class Publisher : IPublish
{
    public void Publish<TEvent>(TEvent e)
    {
        // send information to all subscribers
    }
}

public interface IDispatch
{
    void Send<TCommand>(TCommand c);
}

public class DemoApplication : IDispatch
{
    private readonly IContainer Container;
    public DemoApplication(IContainer diContainer)
    {
        Container = diContainer;
    }

    public void Send<TCommand>(TCommand c)
    {
        // Relay the command to the appropriate command handler (via DI Container)
    }
}
#endregion

#region Common Domain Interfaces
/// <summary>
/// IExecute describes the interface to execute commands
/// </summary>
/// <typeparam name="TCommand"></typeparam>
public interface IExecute<TCommand>
{
    void Execute(TCommand c);
}

/// <summary>
/// IRecall describes the interface to load and save events for an aggregate
/// </summary>
public interface IRecall
{
    IEnumerable<object> LoadEventHistory(string key);
    void AppendToHistory(string key, IEnumerable<object> events);
}

/// <summary>
/// IPublish describes the interface to publish events to subscribers
/// </summary>
public interface IPublish
{
    void Publish<TEvent>(TEvent e);
}

/// <summary>
/// ISubscribe describes the interface for handling events
/// </summary>
/// <typeparam name="TEvent"></typeparam>
public interface ISubscribe<TEvent>
{
    void Handle(TEvent e);
}
#endregion

#region Domain
#region Interfaces
#endregion

#region Commands
public class OpenGradeBook
{
    public string CourseNumber { get; set; }
    public string CourseName { get; set; }
    public OpenGradeBook(string courseNumber, string courseName)
    {
        CourseNumber = courseNumber;
        CourseName = courseName;
    }
}
#endregion

#region Events
#endregion

#region Command Handlers
public sealed class GradingService
    : IExecute<OpenGradeBook>
{
    #region Constructor and private "plumbing"
    private readonly IRecall ES;
    private readonly IPublish Publisher;
    public GradingService(IRecall eventStore, IPublish publisher)
    {
        ES = eventStore;
        Publisher = publisher;
    }

    private void Update(Guid id, Action<GradeBook> execute)
    {
        // Load event stream from the store
        var stream = ES.LoadEventHistory(id.ToString());
        // create new aggregate from the history
        var ar = new GradeBook(stream);
        // execute delegated action
        execute(ar);
        // append resulting changes to the stream
        ES.AppendToHistory(id.ToString(), ar.Changes); // stream.Version, ar.Changes);
        foreach (var change in ar.Changes)
            Publisher.Publish(change);
    }
    #endregion

    #region Specific Command Handlers
    public void Execute(OpenGradeBook c)
    {
        throw new NotImplementedException();
    }
    #endregion
}
#endregion

#region Domain Model
internal class GradeBook
{
    public IEnumerable<object> Changes { get; } = new List<object>();
    public GradeBook(IEnumerable<object> stream)
    {
    }
}
#endregion
#endregion

#region Domain
#region Interfaces
#endregion

#region Commands
#endregion

#region Events
#endregion

#region Command Handlers
#endregion

#region Domain Model
#endregion
#endregion
