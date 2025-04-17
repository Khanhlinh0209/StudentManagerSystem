using StudentManagementSystem.Domain.Entities;

namespace StudentManagementSystem.Application.DTOs.CourseDTO
{
    public class CourseViewModel
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }

        public DateTime StartDate { get; set; }
        public virtual ICollection<CourseStudent> CourseStudents { get; set; }
    }
}