using System.ComponentModel.DataAnnotations;

namespace BCPAO.PhotoManager.Models.AccountViewModels
{
   public class LoginViewModel
   {
      [Required]
      //[EmailAddress]
      [Display(Name = "Username or Email")]
      public string Email { get; set; }

      [Required]
      [DataType(DataType.Password)]
      public string Password { get; set; }

      [Display(Name = "Remember me?")]
      public bool RememberMe { get; set; }
   }
}
