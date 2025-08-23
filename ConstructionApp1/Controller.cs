using Data.Services;
using Domain.Entities;

namespace ConstructionApp
{
    public class Controller
    {
        private readonly ProjectService _projectService;
        private readonly TaskService _taskService;
        private readonly EmployeeService _employeeService;
        private readonly ResourceService _resourceService;
        private readonly PaymentService _paymentTransactionService;

        public Controller(
            ProjectService projectService,
            TaskService taskService,
            EmployeeService employeeService,
            ResourceService resourceService,
            PaymentService paymentTransactionService)
        {
            _projectService = projectService;
            _taskService = taskService;
            _employeeService = employeeService;
            _resourceService = resourceService;
            _paymentTransactionService = paymentTransactionService;
        }

        public void Run()
        {
            Console.WriteLine("=== Welcome to Construction Management ===");
            CreateProject();

            while (true)
            {
                Console.WriteLine("\n=== Main Menu ===");
                Console.WriteLine("1. Add Project");
                Console.WriteLine("2. Add Task to Project");
                Console.WriteLine("3. Add Employee to Project");
                Console.WriteLine("4. Add Resource");
                Console.WriteLine("5. Record Resource Usage");
                Console.WriteLine("6. Record Payment Transaction");
                Console.WriteLine("7. View Projects");
                Console.WriteLine("0. Exit");
                Console.Write("Choose an option: ");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": CreateProject(); break;
                    case "2": CreateTask(); break;
                    case "3": CreateEmployee(); break;
                    case "4": CreateResource(); break;
                    //case "5": RecordResourceUsage(); break;
                    case "6": RecordPaymentTransaction(); break;
                    case "7": ViewProjects(); break;
                    case "0": return;
                    default: Console.WriteLine("Invalid option."); break;
                }
            }
        }

        public int ChooseProject()
        {
            var projects = _projectService.ReadProjects();
            if (projects is null || projects.Count == 0)
            {
                Console.WriteLine("No projects available. Please add a project first.");
                return -1;
            }

            Console.WriteLine("\n=== Select a Project ===");
            for (int i = 0; i < projects.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {projects[i].Name} (ID: {projects[i].Id})");
            }

            Console.Write("Enter project number: ");
            int index = int.Parse(Console.ReadLine());
            if (index < 1 || index > projects.Count)
            {
                Console.WriteLine("Invalid project number.");
                return -1;
            }

            return projects[index - 1].Id;
        }

        public void CreateProject()
        {
            Console.WriteLine("\n=== Add New Project ===");
            Console.Write("Enter project name: ");
            string name = Console.ReadLine();
            Console.Write("Enter project budget: ");
            decimal budget = Convert.ToDecimal(Console.ReadLine());
            Console.Write("Enter project start date (yyyy-mm-dd): ");
            DateTime startDate = DateTime.Parse(Console.ReadLine());

            _projectService.AddProject(name, budget, startDate);
        }

        public void CreateTask()
        {
            int projectId = ChooseProject();
            if (projectId == -1) return;

            Console.Write("Enter task name: ");
            string name = Console.ReadLine();

            Console.Write("Enter task description: ");
            string description = Console.ReadLine();

            Console.Write("Enter deadline (yyyy-mm-dd): ");
            DateTime deadline = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Select task status:");
            foreach (var status in Enum.GetValues(typeof(ProjectStatus)))
            {
                Console.WriteLine($"{(byte)status} - {status}");
            }

            Console.Write("Enter status number: ");
            byte statusNumber = byte.Parse(Console.ReadLine());
            var taskStatus = (ProjectStatus)statusNumber;

            _taskService.AddTask(projectId, name, description, deadline, taskStatus);
        }

        public void CreateEmployee()
        {
            int projectId = ChooseProject();
            if (projectId == -1) return;

            Console.Write("Enter employee first name: ");
            string f_name = Console.ReadLine();

            Console.Write("Enter employee last name: ");
            string l_name = Console.ReadLine();

            Console.WriteLine("Select User Role:");
            foreach (var role in Enum.GetValues(typeof(UserRole)))
            {
                Console.WriteLine($"{(byte)role} - {role}");
            }

            Console.Write("Enter status number: ");
            byte roleNumber = byte.Parse(Console.ReadLine());
            var userRole = (UserRole)roleNumber;

            _employeeService.AddEmployeeToProject(projectId, l_name, f_name, userRole);
        }

        private void CreateResource()
        {
            Console.WriteLine("\n=== Add Resource ===");

            Console.Write("Enter resource name: ");
            string name = Console.ReadLine();

            Console.Write("Enter resource description: ");
            string description = Console.ReadLine();

            Console.Write("Enter unit quantity: ");
            decimal quantity = Convert.ToDecimal(Console.ReadLine());

            Console.WriteLine("Select Unit Cost:");
            foreach (var unit in Enum.GetValues(typeof(UnitCost)))
            {
                Console.WriteLine($"{(byte)unit} - {unit}");
            }

            Console.Write("Enter Unit number: ");
            byte unitNumber = byte.Parse(Console.ReadLine());
            var unitCost = (UnitCost)unitNumber;

            _resourceService.AddResource(name, description, unitCost, quantity);
        }

        public void RecordPaymentTransaction()
        {
            int projectId = ChooseProject();
            if (projectId == -1) return;

            Console.WriteLine("Select Transaction Type:");
            foreach (var transaction in Enum.GetValues(typeof(TransactionType)))
            {
                Console.WriteLine($"{(byte)transaction} - {transaction}");
            }

            Console.Write("Enter Transaction Number: ");
            byte transactionNumber = byte.Parse(Console.ReadLine());
            var transactionType = (TransactionType)transactionNumber;

            Console.Write("Enter amount: ");
            decimal amount = Convert.ToDecimal(Console.ReadLine());

            Console.Write("Enter date (yyyy-mm-dd): ");
            DateTime date = DateTime.Parse(Console.ReadLine());

            Console.Write("Enter description: ");
            string desc = Console.ReadLine();

            _paymentTransactionService.AddTransaction(projectId, transactionType, amount, desc);
        }

        public void ViewProjects()
        {
            Console.WriteLine("\n=== Projects List ===");
            var projects = _projectService.ReadProjects();

            int count = 0;

            foreach (var p in projects)
            {
                if (count == projects.Count)
                    break;

                Console.WriteLine($"---Project {++count}---");
                Console.WriteLine($"Name {p.Name} - Budget: {p.Budget}, Start: {p.StartDate}");
                foreach (var t in p.ProjectTasks)
                {
                    Console.WriteLine($"Name: {t.Name} - Description: {t.Description} - StartDate: {t.StartDate} - Status: {t.Status}");
                }

                foreach (var e in p.Employees)
                {
                    Console.WriteLine($"Firs Name: {e.FirstName} - Last Name: {e.LastName} - Role: {e.Role}");
                }

                foreach (var r in p.ResourceUsage)
                {
                    Console.WriteLine($"Quantity Used: {r.QuantityUsed} - Usage Date: {r.UsageDate}");
                }

                foreach (var pt in p.PaymentTransactions)
                {
                    Console.WriteLine($"Transaction Type: {pt.Type} - Amount: {pt.Amount} - Transaction Date: {pt.TransactionDate} - Description: {pt.Description}");
                }
            }
        }
    }
}
