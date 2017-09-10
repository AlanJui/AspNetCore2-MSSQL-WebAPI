using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DevExtreme.AspNet.Data;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

using WebAPI_001.Models;

namespace WebAPI_001.Controllers
{
    [Produces("application/json")]
    [Route("api")]
    public class EmployeesController : Controller
    {
        private readonly NorthwindContext _context;

        public EmployeesController(NorthwindContext context)
        {
            _context = context;
        }

        [HttpGet("employees-list")]
        public object GetList(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_context.Employees, loadOptions);
        }

        [HttpPost("employees-create")]
        public IActionResult CreateRecord(string values)
        {
            var record = new Employee();
            JsonConvert.PopulateObject(values, record);

            if (!TryValidateModel(record))
                return BadRequest(ModelState.ToFullErrorString());
            _context.Employees.Add(record);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPut("employees-update")]
        public IActionResult UpdateRecord(int key, string values)
        {
            var record = _context.Employees.First<Employee>(r => r.EmployeeId == key);
            JsonConvert.PopulateObject(values, record);

            if (!TryValidateModel(record))
                return BadRequest(ModelState.ToFullErrorString());

            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete("employees-delete")]
        public void DeleteRecord(int key)
        {
            var record = _context.Employees.First<Employee>(r => r.EmployeeId == key);
            _context.Employees.Remove(record);
            _context.SaveChanges();
        }

        // =========================================================================================
        // RESTful API
        // =========================================================================================

        // GET: api/Employees
        [HttpGet("employees")]
        public IEnumerable<Employee> GetEmployees()
        {
            return _context.Employees;
        }

        // GET: api/Employees/5
        [HttpGet("employees/{id}")]
        public async Task<IActionResult> GetEmployee([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employee = await _context.Employees.SingleOrDefaultAsync(m => m.EmployeeId == id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        // PUT: api/Employees/5
        [HttpPut("employees/{id}")]
        public async Task<IActionResult> PutEmployee([FromRoute] int id, [FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employee.EmployeeId)
            {
                return BadRequest();
            }

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        // POST: api/Employees
        [HttpPost("employees")]
        public async Task<IActionResult> PostEmployee([FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployee", new { id = employee.EmployeeId }, employee);
        }

        // DELETE: api/Employees/5
        [HttpDelete("employees/{id}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employee = await _context.Employees.SingleOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return Ok(employee);
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.EmployeeId == id);
        }
    }


    //[Route("api/[controller]")]
    ////[Route("api/employee")]
    //public class EmployeesController : Controller
    //{
    //    private readonly NorthwindContext _context;

    //    public EmployeesController(NorthwindContext context)
    //    {
    //        _context = context;
    //    }

    //    // GET: api/Employees
    //    [HttpGet]
    //    public IEnumerable<Employee> GetEmployees()
    //    {
    //        return _context.Employees;
    //    }

    //    // GET: api/Employees/5
    //    [HttpGet("{id}")]
    //    public async Task<IActionResult> GetEmployee([FromRoute] int id)
    //    {
    //        if (!ModelState.IsValid)
    //        {
    //            return BadRequest(ModelState);
    //        }

    //        var employee = await _context.Employees.SingleOrDefaultAsync(m => m.EmployeeId == id);

    //        if (employee == null)
    //        {
    //            return NotFound();
    //        }

    //        return Ok(employee);
    //    }

    //    // PUT: api/Employees/5
    //    [HttpPut("{id}")]
    //    public async Task<IActionResult> PutEmployee([FromRoute] int id, [FromBody] Employee employee)
    //    {
    //        if (!ModelState.IsValid)
    //        {
    //            return BadRequest(ModelState);
    //        }

    //        if (id != employee.EmployeeId)
    //        {
    //            return BadRequest();
    //        }

    //        _context.Entry(employee).State = EntityState.Modified;

    //        try
    //        {
    //            await _context.SaveChangesAsync();
    //        }
    //        catch (DbUpdateConcurrencyException)
    //        {
    //            if (!EmployeeExists(id))
    //            {
    //                return NotFound();
    //            }
    //            else
    //            {
    //                throw;
    //            }
    //        }

    //        return NoContent();
    //    }


    //    // POST: api/Employees
    //    [HttpPost]
    //    public async Task<IActionResult> PostEmployee([FromBody] Employee employee)
    //    {
    //        if (!ModelState.IsValid)
    //        {
    //            return BadRequest(ModelState);
    //        }

    //        _context.Employees.Add(employee);
    //        await _context.SaveChangesAsync();

    //        return CreatedAtAction("GetEmployee", new { id = employee.EmployeeId }, employee);
    //    }

    //    // DELETE: api/Employees/5
    //    [HttpDelete("{id}")]
    //    public async Task<IActionResult> DeleteEmployee([FromRoute] int id)
    //    {
    //        if (!ModelState.IsValid)
    //        {
    //            return BadRequest(ModelState);
    //        }

    //        var employee = await _context.Employees.SingleOrDefaultAsync(m => m.EmployeeId == id);
    //        if (employee == null)
    //        {
    //            return NotFound();
    //        }

    //        _context.Employees.Remove(employee);
    //        await _context.SaveChangesAsync();

    //        return Ok(employee);
    //    }

    //    private bool EmployeeExists(int id)
    //    {
    //        return _context.Employees.Any(e => e.EmployeeId == id);
    //    }
    //}
}