using System.Data.Entity;

namespace TestNinja.Mocking
{
    public class EmployeeStorage : IEmployeeStorage
    {
        private EmployeeContext _employeeContext;

        public EmployeeStorage(EmployeeContext context = null)
        {
            _employeeContext = context ?? new EmployeeContext();
        }
        
        public void DeleteEmployee(int id)
        {
            var employee = _employeeContext.Employees.Find(id);
            if (employee == null) return;
            _employeeContext.Employees.Remove(employee);
            _employeeContext.SaveChanges();
        }
    }
    
    
    public class EmployeeController
    {
        private readonly IEmployeeStorage _employeeStorage;

        public EmployeeController(IEmployeeStorage storage = null)
        {
            
            _employeeStorage = storage ?? new EmployeeStorage();
        }

        public ActionResult DeleteEmployee(int id)
        {
            _employeeStorage.DeleteEmployee(id);
            return RedirectToAction("Employees");
        }

        private ActionResult RedirectToAction(string employees)
        {
            return new RedirectResult();
        }
    }

    public class ActionResult { }
 
    public class RedirectResult : ActionResult { }
    
    public class EmployeeContext
    {
        public DbSet<Employee> Employees { get; set; }

        public void SaveChanges()
        {
        }
    }

    public class Employee
    {
    }
}