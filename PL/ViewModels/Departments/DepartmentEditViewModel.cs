using System.ComponentModel.DataAnnotations;
using BLL.Dtos.DepartementsDto;
namespace PL.ViewModels.Departments
{
    public class DepartmentEditViewModel
    {
        [Required]
        //public int Id { get; set; }
        public string Name { get; set; } = null!;
        [Required(ErrorMessage ="U Must add Code!!")]
        public string Code { get; set; } = null!;
        public string? Description { get; set; } = null!;
        [Display(Name ="Creation Date")]
        public DateOnly CreationDate { get; set; }
    }
}
