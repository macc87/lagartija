using System.ComponentModel.DataAnnotations;

namespace Fantasy.API.Host.Models
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "email")]
        public string Email { get; set; }

        [Display(Name = "question")]
        public string Question { get; set; }

        [Display(Name = "answer")]
        public string Answer { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}