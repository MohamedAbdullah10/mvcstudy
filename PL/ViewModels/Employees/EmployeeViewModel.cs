using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace PL.ViewModels.Employees
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = null!;

        [Range(18, 60)]
        public int Age { get; set; }

        public string? Address { get; set; }

        public bool IsActive { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [Range(3000, 100000)]
        public decimal Salary { get; set; }

        public string? PhoneNumber { get; set; }

        public DateTime HiringDate { get; set; } = DateTime.Now;

        [Required]
        public string Gender { get; set; } = null!;

        [Required]
        public string EmployeeType { get; set; } = null!;

        // Properties for Dropdown Lists
        public IEnumerable<SelectListItem>? Genders { get; set; }
        public IEnumerable<SelectListItem>? EmployeeTypes { get; set; }

        [Display(Name="Department")]
        public int? DepartmentId { get; set; }
        public IEnumerable<SelectListItem>? Departemnts { get; set; }
    }
}