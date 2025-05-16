using System.ComponentModel.DataAnnotations;

namespace IKEA.PL.ViewModel.Identity
{
    public class LoginViewModel
    {
        [Display(Name = "Email Address")]
        public string Email { get; set; } = null!;

        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    
        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
