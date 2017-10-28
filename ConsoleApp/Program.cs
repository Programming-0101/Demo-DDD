using Autofac;
using Autofac.Core;
using ConsoleApp.Infrastructure;
using Enexure.MicroBus;
using Enexure.MicroBus.Autofac;
using StudentGrades;
using StudentGrades.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        #region Starup
        static void Main(string[] args)
        {
            var app = new Program(ConfigureApp());
            app.Run();
        }

        static IContainer ConfigureApp()
        {
            // Set up DI container using Autofac
            var builder = new ContainerBuilder();
            // Register my "event store"
            builder.RegisterType<InMemoryStorage>().AsSelf().AsImplementedInterfaces();

            // Register the Command Bus
            var thisAssembly = typeof(Program).Assembly;
            var studentGradeAssembly = AppDomain.CurrentDomain.GetAssemblies()
                                      .SingleOrDefault(assm => assm.GetName().Name.StartsWith("StudentGrades"));
            var busBuilder = new BusBuilder().RegisterHandlers(thisAssembly, studentGradeAssembly);
            builder.RegisterMicroBus(busBuilder);


            return builder.Build();

        }
        #endregion

        #region Driver
        static IContainer DiContainer;
        public Program(IContainer container)
        {
            DiContainer = container;
        }
        InMemoryStorage Repo { get { return DiContainer.Resolve<InMemoryStorage>(); } }
        IMicroBus Bus { get { return DiContainer.Resolve<IMicroBus>(); } }

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
                    UI_OpenGradeBook();
                    break;
                case "2":
                    UI_AddStudent();
                    break;
                case "3":
                    UI_RemoveStudent();
                    break;
                case "4":
                    UI_EnterGrade();
                    break;
                case "5":
                    UI_DisplayGrades();
                    break;
                case "6":
                    UI_ListStudents();
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
        void NeedsGradeBook(string message = "Open a grade book before selecting this option.", bool needed = true)
        {
            var hasGradebook = Repo.Keys.Count() > 0;
            if (hasGradebook != needed)
                throw new Exception(message);
        }

        void UI_OpenGradeBook()
        {
            // One grade book only
            NeedsGradeBook($"Grade book for {Repo.Keys.FirstOrDefault()} already opened.", false);
            Console.Write("Enter Course Number (existing, or a new one): ");
            string num = Console.ReadLine();
            Console.Write("Enter Course Name: ");
            string name = Console.ReadLine();
            var cmd = new OpenGradeBook(num, name);
            Bus.SendAsync(cmd);
        }

        void UI_AddStudent()
        {
            NeedsGradeBook();
            Console.Write("Enter first name: ");
            string first = Console.ReadLine();
            Console.Write("Enter last name: ");
            string last = Console.ReadLine();
            var cmd = new AddStudent(Guid.NewGuid(), first, last);
            Bus.SendAsync(cmd);
        }

        void UI_RemoveStudent()
        {
            NeedsGradeBook();
        }

        void UI_EnterGrade()
        {
            NeedsGradeBook();
        }

        void UI_DisplayGrades()
        {
            NeedsGradeBook();
        }

        void UI_ListStudents()
        {
            NeedsGradeBook();
        }
        #endregion
    }
}

#region Infrastructure

//public class Publisher : IPublish
//{
//    public Publisher(IMicroBus bus)
//    {
//        Bus = bus;
//    }
//    public IMicroBus Bus { get; set; }
//    public void Publish<TEvent>(TEvent e)
//    {
//        Bus.PublishAsync(e);
//    }
//}

//public interface IDispatch
//{
//    void Send<TCommand>(TCommand c);
//}

//public class CommandBus : IDispatch
//{
//    public void Send<TCommand>(TCommand c)
//    {
//        // Relay the command to the appropriate command handler (via DI Container)
//    }
//}
#endregion

#region Common Domain Interfaces
///// <summary>
///// IExecute describes the interface to execute commands
///// </summary>
///// <typeparam name="TCommand"></typeparam>
//public interface IExecute<TCommand>
//{
//    void Execute(TCommand c);
//}


///// <summary>
///// ISubscribe describes the interface for handling events
///// </summary>
///// <typeparam name="TEvent"></typeparam>
//public interface ISubscribe<TEvent>
//{
//    void Handle(TEvent e);
//}
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
