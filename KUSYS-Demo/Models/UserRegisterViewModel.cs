using System.ComponentModel.DataAnnotations;
using EntityLayer.Concrete;

namespace KUSYS_Demo.Models;
public class UserRegisterViewModel
{
    // [Required(ErrorMessage = "Lütfen adınızı giriniz")]
    // public string? Name { get; set; }
    //
    // [Required(ErrorMessage = "Lütfen soyadınızı giriniz")]
    // public string? Surname { get; set; }
    [Required(ErrorMessage = "Lütfen öğrenci numaranızı giriniz")]
    public int StudentId { get; set; }

    [Required(ErrorMessage = "Lütfen Mail adresinizi giriniz")]
    public string? Mail { get; set; }

    [Required(ErrorMessage = "Lütfen şifreyi giriniz")]
    public string? Password { get; set; }

    [Required(ErrorMessage = "Lütfen şifreyi tekrar giriniz")]
    [Compare("Password", ErrorMessage = "şifreler uyumlu değil")]
    public string? ConfirmPassword { get; set; }
}
