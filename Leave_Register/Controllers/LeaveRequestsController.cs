using Leave_Register.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Leave_Register.Models;
using System;

namespace Leave_Register.Controllers
{
    public class LeaveRequestsController : Controller
    {

        private readonly ApplicationDbContext _context;

        public LeaveRequestsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var leaveApplications = _context.LeaveRequests.Include(x => x.Employee).ToList();

            var leaveDuration = new Dictionary<string, int>();
            foreach (var leave in leaveApplications)
            {
                int daysRequested = (leave.EndDateOfLeave - leave.StartDateOfLeave).Days + 1;
                if (leaveDuration.ContainsKey(leave.Employee.EmployeeFullName))
                {
                    leaveDuration[leave.Employee.EmployeeFullName] += daysRequested;
                }
                else
                {
                    leaveDuration[leave.Employee.EmployeeFullName] = daysRequested;
                }
            }

            ViewBag.LeaveDuration = leaveDuration;

            return View("Index", leaveApplications);
        }

        public IActionResult FilterLeaveByMonth(int month)
        {
            var filteredLeaveApplications = _context.LeaveRequests
                .Where(x => x.LeaveRequestRegistrationTime.Month == month)
                .Include(x => x.Employee)
                .ToList();

            var leaveDuration = new Dictionary<string, int>();
            foreach (var leave in filteredLeaveApplications)
            {
                int daysRequested = (leave.EndDateOfLeave - leave.StartDateOfLeave).Days + 1;
                if (leaveDuration.ContainsKey(leave.Employee.EmployeeFullName))
                {
                    leaveDuration[leave.Employee.EmployeeFullName] += daysRequested;
                }
                else
                {
                    leaveDuration[leave.Employee.EmployeeFullName] = daysRequested;
                }
            }

            ViewBag.LeaveDuration = leaveDuration;

            return View("Index", filteredLeaveApplications);
        }

        [HttpGet]
        public IActionResult FilterEmployeeLeave()
        {
            var employeesList = _context.Employees.Select(x => new SelectListItem
            {
                Value = x.EmployeeId.ToString(),
                Text = x.EmployeeFullName
            }).ToList();

            ViewBag.EmployeesList = employeesList;

            return View();
        }

        [HttpPost]
        public IActionResult FilterEmployeeLeave(int selectedEmployeeId)
        {
            var employee = _context.Employees.Find(selectedEmployeeId);
            var hasAppliedForLeave = _context.LeaveRequests.Any(l => l.FkEmployeeId == selectedEmployeeId);

            if (hasAppliedForLeave)
            {
                ViewBag.Status = $"{employee.EmployeeFullName} has applied for leave.";
            }
            else
            {
                ViewBag.Status = $"{employee.EmployeeFullName} has not applied for leave.";
            }

            var employeesList = _context.Employees.Select(x => new SelectListItem
            {
                Value = x.EmployeeId.ToString(),
                Text = x.EmployeeFullName
            }).ToList();

            ViewBag.EmployeesList = employeesList;

            return View("FilterEmployeeLeave");
        }

        [HttpGet]
        public IActionResult CreateLeaveApplication()
        {
            ViewBag.Employees = _context.Employees.Select(x => new SelectListItem
            {
                Value = x.EmployeeId.ToString(),
                Text = x.EmployeeFullName
            }).ToList();
            return View();
        }
        [HttpPost]
        public IActionResult CreateLeaveApplication(LeaveRequest timeOff)
        {
            if (ModelState.IsValid)
            {
                timeOff.LeaveRequestRegistrationTime = DateTime.Now;
                _context.LeaveRequests.Add(timeOff);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Employees = _context.Employees.Select(x => new SelectListItem
            {
                Value = x.EmployeeId.ToString(),
                Text = x.EmployeeFullName
            }).ToList();
            return View("CreateLeaveApplication", timeOff); 
        } 
    }
}

