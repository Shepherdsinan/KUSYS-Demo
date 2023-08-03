using Microsoft.AspNetCore.Identity;
namespace EntityLayer.Concrete;
public class AppUser : IdentityUser<int>
{
   public int StudentId { get; set; } 
   public Student? Student { get; set;}
}
