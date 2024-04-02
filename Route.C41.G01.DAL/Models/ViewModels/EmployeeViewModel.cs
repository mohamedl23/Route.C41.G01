using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.C41.G01.DAL.Models.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name IS Required") ]
        [MinLength(2 ,ErrorMessage ="Min Length is 2" ) ]
        [MaxLength(40 ,ErrorMessage ="Max Length is 40" )]
        public string Name { get; set; }
        [Range(22,100)]
        public int? Age { get; set; }
        [RegularExpression(@"[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$"
            , ErrorMessage = "Address Must be Like 123-Street-City-Country")]
        public string Address { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Hiring Date")]
        public DateTime HiringDate { get; set; }
        public Gender gender { get; set; }
        public EmpType employeeType { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
        public IFormFile Image { get; set; }
        public string ImageName { get; set; }
        public int? DepartmentId { get; set; }
        public Department Department { get; set; }



   
}
}
