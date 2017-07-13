using System.ComponentModel.DataAnnotations;

namespace BCPAO.PhotoManager.Models.AccountViewModels
{
   public class RegisterViewModel
   {
      [Required]
      [StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
      [Display(Name = "First Name")]
      public string FirstName { get; set; }

      [Required]
      [StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
      [Display(Name = "Last Name")]
      public string LastName { get; set; }

      [Required]
      [StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
      [Display(Name = "Username")]
      public string Username { get; set; }

      [Required]
      [EmailAddress]
      [Display(Name = "Email Address")]
      public string Email { get; set; }

      [Required]
      [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
      [DataType(DataType.Password)]
      [Display(Name = "Password")]
      public string Password { get; set; }

      [DataType(DataType.Password)]
      [Display(Name = "Confirm password")]
      [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
      public string ConfirmPassword { get; set; }
   }
}
