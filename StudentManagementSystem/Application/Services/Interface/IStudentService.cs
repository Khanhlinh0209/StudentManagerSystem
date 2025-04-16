using StudentManagementSystem.Application.DTOs.StudentDTO;

namespace StudentManagementSystem.Application.Services.Interface
{
    public interface IStudentService
    {
        IEnumerable<StudentViewModel> GetStudents(int? Id);

        bool CreateStudent(StudentViewModel student);

        bool UpdateStudent(StudentViewModel student);

        bool DeleteStudent(int id);
    }
}