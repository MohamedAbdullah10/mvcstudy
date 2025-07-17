using System.ComponentModel.DataAnnotations;

namespace PL.ViewModels.Identity
{
    public class SignUpVM
    {
        [Required]
        public string FirstName { get; set; } = null!;
        [Required]
        public string LastName { get; set; }=null!;
        [Required]
        public string UserName { get; set; } = null!;
        [EmailAddress]
        public string Email { get; set; }= null!;
        [DataType(DataType.Password)]
        public string Password { get; set; }=null!;
        [Display(Name ="ConfirmedPassword")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Confirmed Password doesnt match Password")]
        public string ConfirmedPassword { get; set; } = null!;

        public bool IsAgree { get; set; }
    }
}
