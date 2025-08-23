using ConstructionApp;
using Data.Services;

namespace ConstructionApp1
{
    internal class MainApp
    {
        static void Main(string[] args)
        {
            Controller controller = new Controller
                (
                    new ProjectService(),
                    new TaskService(),
                    new EmployeeService(),
                    new ResourceService(),
                    new PaymentService()
                );

            controller.Run();
        }
    }
}
