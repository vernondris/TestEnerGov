using EnerGovSolutions.DataContext;
using EnerGovSolutions.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Net;

namespace TestEnerGov.Server.Controllers
{
    public class DataManagement(ApplicationDBContext context) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("api/[controller]")]
        [ApiController]
        public class DataManagementController : ControllerBase
        {
        }

        [Route("GetEmployees")]
        [HttpGet]
        public ActionResult<IEnumerable<Employee>> GetEmployees()
        {
            return context.Employees.ToList();
        }

        [Route("GetRoleMasterFile")]
        [HttpGet]
        public ActionResult<IEnumerable<RoleMasterFile>> GetRoleMasterFile()
        {
            return context.RoleMasterFile.ToList();
        }

        class EmployeeData
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        public class EmployeeDataToBeSave
        {
            public int ReportingTo { get; set; }
            public string Fname { get; set; }
            public string Lname { get; set; }
            public int[] Roles { get; set; }
        }

        class ManagerData
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        [Route("GetManagers")]
        [HttpGet]
        public IEnumerable<Employee> GetManagers()
        {
            return (from emp in context.Set<Employee>()
                    join roles in context.Set<Role>() on emp.Id equals roles.EmployeeId
                    where roles.RoleMasterFileId == 1 || roles.RoleMasterFileId == 2
                    select new Employee { Id = emp.Id, FirstName = emp.FirstName, LastName = emp.LastName }).ToList();
        }

        [Route("GetHierarchyBasedOnSelectedManager")]
        [HttpGet("{managerId}")]
        [HttpGet]
        public ActionResult GetHierarchyBasedOnSelectedManager(int managerId)
        {
            var result = (from emp in context.Set<Employee>()
                          join hierarchy in context.Set<Hierarchy>() on emp.Id equals hierarchy.ReportingByEmployeeId
                          where hierarchy.ReportingToEmployeeId == managerId
                          select new EmployeeData { Id = emp.Id, FirstName = emp.FirstName, LastName = emp.LastName }).ToList();

            return Json(result);
        }

        [Route("GetEmployee")]
        [HttpGet("{id}")]
        public ActionResult<Employee> GetEmployee(int id)
        {
            var employee = context.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            return employee;
        }

        [Route("CreateEmployee")]
        [HttpPost]
        public ActionResult<Employee> CreateEmployee(Employee employee)
        {
            if (employee == null)
            {
                return BadRequest();
            }
            context.Employees.Add(employee);
            context.SaveChanges();
            return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee);
        }

        [Route("CreateRole")]
        [HttpPost]
        public ActionResult<Role> CreateRole(Role role)
        {
            if (role == null)
            {
                return BadRequest();
            }
            context.Roles.Add(role);
            context.SaveChanges();
            return CreatedAtAction(nameof(GetRoleMasterFile), new { id = role.Id }, role);
        }

        [Route("AddNewEmployees")]
        [HttpPost]
        public ActionResult? AddNewEmployees([FromBody]EmployeeDataToBeSave employeeDataToBeSave)
        {
            Employee employee = new Employee() { FirstName = employeeDataToBeSave.Fname, LastName = employeeDataToBeSave.Lname };

            context.Employees.Add(employee);
            context.SaveChanges();
            
            var ids = employeeDataToBeSave.Roles.Distinct();
            foreach (int id in ids)
            {
                Role role = new Role() { RoleMasterFileId = id, EmployeeId = employee.Id };
                context.Roles.Add(role);
                context.SaveChanges();
            }

            return Ok(null);
        }
    }
}
