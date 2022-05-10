using Assignment.Data;
using Assignment.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assignment.Services
{
    public interface IStudentService
    {
        Task<List<StudentModel>> GetStudentList();
        Task<StudentModel> GetStudentById(int? id);
        Task<int> AddStudent(StudentModel student);
        Task UpdateStudent(StudentModel student);
        Task<int> DeleteStudent(int? id);
    }
    public class StudentService : IStudentService
    {
        private readonly ApplicationDbContext _dbContext;
        public StudentService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> AddStudent(StudentModel student)
        {
            if (_dbContext != null)
            {
                await _dbContext.AddAsync(student);
                await _dbContext.SaveChangesAsync();
                return student.StudentId;
            }
            return 0;
        }

        public async Task<int> DeleteStudent(int? id)
        {
            int result = 0;
            var student = await _dbContext.Students.FirstOrDefaultAsync(e => e.StudentId == id);
            if (student != null)
            {
                _dbContext.Students.Remove(student);
                result = await _dbContext.SaveChangesAsync();
                return result;
            }
            return result;
        }

        public async Task<StudentModel> GetStudentById(int? id)
        {
            if (_dbContext != null)
            {
                return await _dbContext.Students.FirstOrDefaultAsync(e => e.StudentId == id);
            }
            return null;
        }

        public async Task<List<StudentModel>> GetStudentList()
        {
            if (_dbContext != null)
            {
                return await _dbContext.Students.ToListAsync();
            }
            return null; ;
        }

        public async Task UpdateStudent(StudentModel student)
        {
            if (student != null)
            {
                _dbContext.Students.Update(student);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
