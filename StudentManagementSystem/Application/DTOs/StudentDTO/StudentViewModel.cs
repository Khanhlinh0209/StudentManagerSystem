﻿namespace StudentManagementSystem.Application.DTOs.StudentDTO
{
    public class StudentViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string SchoolId { get; set; }
    }
}