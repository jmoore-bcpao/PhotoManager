using System.ComponentModel.DataAnnotations;

namespace BCPAO.PhotoManager.Models.AccountViewModels
{
   public class ForgotPasswordViewModel
   {
      [Required]
      //[EmailAddress]
      [Display(Name = "Username or Email")]
      public string Email { get; set; }
   }
}
