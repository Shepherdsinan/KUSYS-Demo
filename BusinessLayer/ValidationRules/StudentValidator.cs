using EntityLayer.Concrete;
using FluentValidation;

namespace BusinessLayer.ValidationRules;
public class StudentValidator : AbstractValidator<Student>
{
    public StudentValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("Adı boş olamaz");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("Soyadı boş olamaz");
        RuleFor(x => x.BirthDate).NotEmpty().WithMessage("Doğum tarihi boş olamaz");
        RuleFor(x => x.FirstName).MinimumLength(3).WithMessage("Adı kısmına minimum 3 karakter girilmelidir");
        RuleFor(x => x.LastName).MinimumLength(3).WithMessage("Soyadı kısmına minimum 3 karakter girilmelidir");
    }
}
