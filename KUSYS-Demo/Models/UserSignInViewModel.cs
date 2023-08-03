using System.ComponentModel.DataAnnotations;

namespace KUSYS_Demo.Models;
public class UserSignInViewModel
{
    [Required(ErrorMessage = "Lütfen Kullanıcı adını giriniz")]
    public string? username { get; set; }

    [Required(ErrorMessage = "Lütfen şifrenizi giriniz")]
    public string? password { get; set; }
}
