using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Application.DTOs.CourseStudentDTO;
using StudentManagementSystem.Application.Services.Interface;
using StudentManagementSystem.Domain.Entities;
using StudentManagementSystem.Infrastructures;

namespace StudentManagementSystem.Application.Services.Implementation
{
    public class CourseStudentService(IApplicationDbContext _context) : ICourseStudentService
    {
        public IEnumerable<CourseStudentViewModel> GetCourseStudents(int? courseId, int? studentId)
        {
            var query = _context.CourseStudents
                .Include(cs => cs.Course)
                .Include(cs => cs.Student)
                .AsQueryable();

            if (courseId is not null)
                query = query.Where(cs => cs.CourseId == courseId);

            if (studentId is not null)
                query = query.Where(cs => cs.StudentId == studentId);

            var result = query.Select(cs => new CourseStudentViewModel
            {
                CourseId = cs.CourseId,
                StudentId = cs.StudentId,
                CourseName = cs.Course.CourseName,
                StudentName = cs.Student.FirstName + " " + cs.Student.LastName,
                AssignmentScore = cs.AssignmentScore,
                PracticalScore = cs.PracticalScore,
                FinalScore = cs.FinalScore
            }).ToList();

            return result;
        }

        public bool AddCourseStudent(CreateCourseStudentModel courseStudent)
        {
            var courseStudentEntity = new CourseStudent
            {
                CourseId = courseStudent.CourseId,
                StudentId = courseStudent.StudentId,
                AssignmentScore = courseStudent.AssignmentScore,
                PracticalScore = courseStudent.PracticalScore,
                FinalScore = courseStudent.FinalScore
            };
            _context.CourseStudents.Add(courseStudentEntity);
            return _context.SaveChanges() > 0;
        }

        public bool UpdateCourseStudent(int courseId, int studentId, UpdateCourseStudentModel updateCourseStudent)
        {
            var courseStudent = _context.CourseStudents
                .FirstOrDefault(cs => cs.CourseId == courseId && cs.StudentId == studentId);
            if (courseStudent != null)
            {
                courseStudent.AssignmentScore = updateCourseStudent.AssignmentScore;
                courseStudent.PracticalScore = updateCourseStudent.PracticalScore;
                courseStudent.FinalScore = updateCourseStudent.FinalScore;
            }
            return _context.SaveChanges() > 0;
        }

        public IEnumerable<CourseScoreViewModel> GetScoresByStudent(int studentId)
        {
            var scores = _context.CourseStudents
                .Where(cs => cs.StudentId == studentId)
                .Include(cs => cs.Course)
                .Select(cs => new CourseScoreViewModel
                {
                    CourseName = cs.Course.CourseName,
                    AssignmentScore = cs.AssignmentScore,
                    PracticalScore = cs.PracticalScore,
                    FinalScore = cs.FinalScore
                }).ToList();
            return scores;
        }

        public float GetAverageScore(int studentId)
        {
            var scores = _context.CourseStudents.Where(cs => cs.StudentId == studentId)
                .Select(cs => (cs.AssignmentScore + cs.PracticalScore + cs.FinalScore) / 3f)
                .ToList();
            if (!scores.Any()) return 0;

            return scores.Average();
        }
    }
}