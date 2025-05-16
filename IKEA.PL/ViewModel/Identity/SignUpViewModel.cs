using System.ComponentModel.DataAnnotations;

namespace IKEA.PL.ViewModel.Identity
{
    public class SignUpViewModel
    {
        [Display(Name ="First Name")]
        public string FName { get; set; } = null!;
        [Display(Name = "Last Name")]
        public string LName { get; set; } = null!;
        [Display(Name = "User Name")]
        public string UserName { get; set; } = null!;
        [Display(Name = "Email Address")]
        public string Email { get; set; } = null!;
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password",ErrorMessage = "Confirm Password Does Not Match With Password !!")]
        public string ConfirmPassword { get; set; } = null!;
        [Display(Name = "Is Agree")]
        public bool IsAgree { get; set; }
    }
}
