using StudentManagementSystem.Application.DTOs.CourseDTO;
using StudentManagementSystem.Application.Services.Interface;
using StudentManagementSystem.Domain.Entities;
using StudentManagementSystem.Infrastructures;

namespace StudentManagementSystem.Application.Services.Implementation
{
    public class CourseService(IApplicationDbContext _context) : ICourseService
    {
        public IEnumerable<CourseViewModel> GetCourses(int? CourseId)
        {
            var course = _context.Courses.FirstOrDefault(x => x.CourseId == CourseId);
            if (course == null)
            {
                return null;
            }
            else
            {
                var courseViewModel = new CourseViewModel
                {
                    CourseId = course.CourseId,
                    CourseName = course.CourseName,
                    StartDate = course.StartDate,
                    CourseStudents = course.CourseStudents
                };
                return new List<CourseViewModel> { courseViewModel };
            }
        }

        public bool CreateCourse(CreateCourseModel course)
        {
            var newCourse = new Course
            {
                CourseName = course.CourseName,
                StartDate = course.StartDate,
            };
            _context.Courses.Add(newCourse);
            var result = _context.SaveChanges();
            return true;
        }

        public bool UpdateCourse(UpdateCourseModel updateCourse)
        {
            var course = _context.Courses.FirstOrDefault(x => x.CourseId == updateCourse.CourseId);
            if (course == null)
            {
                return false;
            }
            else
            {
                course.CourseName = updateCourse.CourseName;
                course.StartDate = updateCourse.StartDate;
                _context.SaveChanges();
                return true;
            }
        }

        public bool DeleteCourse(int courseId)
        {
            var course = _context.Courses.FirstOrDefault(x => x.CourseId == courseId);
            if (course == null)
            {
                return false;
            }
            else
            {
                _context.Courses.Remove(course);
                _context.SaveChanges();
                return true;
            }
        }
    }
}