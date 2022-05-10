using Assignment.Data;
using Assignment.Models;
using Assignment.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        IStudentService _service;
        public TodoController(IStudentService service)
        {
            _service = service;
        }
        [HttpGet]
        [Route("GetStudents")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetStudents()
        {
            try
            {
                var students = await _service.GetStudentList();
                if (students == null)
                {
                    return NotFound();
                }
                return Ok(students);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpGet]
        [Route("GetStudetn")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetStudentById(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            try
            {
                var student = await _service.GetStudentById(id);
                if (student == null)
                {
                    return NotFound();
                }
                return Ok(student);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpPost]
        [Route("AddStudent")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddStudent([FromBody] StudentModel student)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var studentId = await _service.AddStudent(student);
                    if (studentId > 0)
                    {
                        return Ok(studentId);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }
            return BadRequest();
        }

        [HttpDelete]
        [Route("DeleteStudent")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteStudent(int? id)
        {
            int data = 0;
            if (id == null)
            {
                return BadRequest();
            }

            try
            {
                data = await _service.DeleteStudent(id);
                if (data == 0)
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
            return BadRequest(data);
        }
        [HttpPost]
        [Route("UpdateStudent")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateStudents([FromBody] StudentModel student)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _service.UpdateStudent(student);
                    return Ok();
                }
                catch (Exception ex)
                {
                    if (ex.GetType().FullName == "Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException")
                    {
                        return NotFound(ex.Message);
                    }
                    return BadRequest();
                }
            }
            return BadRequest();
        }
    }
}
