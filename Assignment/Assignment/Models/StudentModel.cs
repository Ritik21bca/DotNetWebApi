using System;
using System.ComponentModel.DataAnnotations;

namespace Assignment.Models
{
    public class StudentModel
    {
        [Key]
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public DateTime DateOfBerth { get; set; }
        public string Course { get; set; }
        public string Address { get; set; }
    }
}
