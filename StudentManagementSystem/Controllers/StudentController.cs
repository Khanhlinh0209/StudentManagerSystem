using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Application.DTOs.StudentDTO;
using StudentManagementSystem.Application.Services.Interface;

namespace StudentManagementSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController(IStudentService StudentService) : ControllerBase
    {
        [HttpGet]
        public IEnumerable<StudentViewModel> GetStudents(int? Id)
        {
            return StudentService.GetStudents(Id);
        }

        [HttpPost]
        public bool CreateStudent(StudentViewModel student)
        {
            return StudentService.CreateStudent(student);
        }

        [HttpPut]
        public bool UpdateStudent(StudentViewModel student)
        {
            return StudentService.UpdateStudent(student);
        }

        [HttpDelete]
        public bool DeleteStudent(int id)
        {
            return StudentService.DeleteStudent(id);
        }
    }
}