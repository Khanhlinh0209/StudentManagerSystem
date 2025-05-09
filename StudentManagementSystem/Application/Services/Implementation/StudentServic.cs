﻿using StudentManagementSystem.Application.DTOs.StudentDTO;
using StudentManagementSystem.Application.Services.Interface;
using StudentManagementSystem.Domain.Entities;
using StudentManagementSystem.Infrastructures;

namespace StudentManagementSystem.Application.Services.Implementation
{
    public class StudentService(IApplicationDbContext _context) : IStudentService
    {
        public IEnumerable<StudentViewModel> GetStudents(int? Id)
        {
            var query = _context.Students.AsQueryable();

            if (Id.HasValue)
            {
                query = query.Where(student => student.Id == Id.Value);
            }

            var students = query.Select(student => new StudentViewModel
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                DateOfBirth = student.DateOfBirth,
                Address = student.Address,
                SchoolId = student.SchoolId.ToString(),
            }).ToList();

            return students;
        }

        public bool CreateStudent(StudentViewModel student)
        {
            var data = new Student
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                DateOfBirth = student.DateOfBirth,
                Address = student.Address,
                SchoolId = int.Parse(student.SchoolId), // Convert string to int
            };
            _context.Students.Add(data);
            _context.SaveChanges();
            return true;
        }

        public bool UpdateStudent(StudentViewModel student)
        {
            var Student = _context.Students.Find(student.Id);
            if (student != null)
            {
                Student.FirstName = student.FirstName;
                Student.LastName = student.LastName;
                Student.DateOfBirth = student.DateOfBirth;
                Student.Address = student.Address;
                Student.SchoolId = int.Parse(student.SchoolId); // Convert string to int
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false; // Or handle the case where the student is not found
            }
        }

        public bool DeleteStudent(int id)
        {
            var Student = _context.Students.Find(id);
            if (Student != null)
            {
                _context.Students.Remove(Student);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false; // Or handle the case where the student is not found
            }
        }
    }
}