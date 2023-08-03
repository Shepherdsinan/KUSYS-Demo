using BusinessLayer.Abstract;
using BusinessLayer.ValidationRules;
using EntityLayer.Concrete;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KUSYS_Demo.Controllers;
public class StudentController : Controller
{
    private readonly IStudentService _studentService;
    private readonly ICourseService _courseService;
    private readonly IStudentCourseService _studentCourseService;
    private readonly IValidator<Student> _studentValidator;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly UserManager<AppUser> _userManager;

    public StudentController(IStudentService studentService, ICourseService courseService, IValidator<Student> studentValidator, IStudentCourseService studentCourseService, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
    {
        _studentService = studentService;
        _courseService = courseService;
        _studentValidator = studentValidator;
        _studentCourseService = studentCourseService;
        _signInManager = signInManager;
        _userManager = userManager;
    }

    #region Index metodu öğrencileri listeler
    public IActionResult Index()
    {
        var userId = _signInManager.UserManager.GetUserId(User);
        int studentId = (_userManager.FindByIdAsync(userId).Result).StudentId;
        List<Student> values;
        if (studentId ==0) //is admin
        {
            values = _studentService.TGetList();
        }
        else
        {
            values = _studentService.GetListById(studentId);
        }
        return View(values);
    }
    #endregion
    
    #region Us1 (create,update,delete)
    /*Us1 Codes start*/
    #region AddStudent get ile sayfa yüklenir. 
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public IActionResult AddStudent()
    {
        return View();
    }
    #endregion

    #region AddStudent post işlemi ile öğrenci kaydedilir fakat validasyon kontrollerine takılırsa uyarı verir
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public IActionResult AddStudent(Student student)
    {
        ValidationResult results = _studentValidator.Validate(student);
        if (results.IsValid)
        {
            _studentService.TAdd(student);
            return RedirectToAction("Index");
        }
        else
        {
            foreach (var item in results.Errors)
            {
                ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
            }
        }
        return View();

    }
    #endregion

    #region UpdateStudent get verilerin gelmesi sağlanır
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public IActionResult UpdateStudent(int id)
    {
        var students = _studentService.GetStudentWithCourses(id);
        return View(students);
    }
    #endregion

    #region UpdateStudent post ile öğrenci verilerinin güncellenmesi sağlanır
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public IActionResult UpdateStudent(Student student, List<string> courseIds)
    {
        ValidationResult results = _studentValidator.Validate(student);
        if (results.IsValid)
        {
            var oldStudent = _studentService.TGetById(student.StudentId);
            oldStudent.FirstName = student.FirstName;
            oldStudent.LastName = student.LastName;
            oldStudent.BirthDate = student.BirthDate;
            _studentService.TUpdate(oldStudent);
            return RedirectToAction("Index");
        }
        else
        {
            foreach (var item in results.Errors)
            {
                ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
            }
        }
        return View();

    }
    #endregion

    #region DeleteStudent metodu ile öğrenci silinir.
    /*Us1 Codes end*/
    #endregion

    [Authorize(Roles = "Admin")]
    public IActionResult DeleteStudent(int id)
    {
        var values = _studentService.TGetById(id);
        _studentService.TDelete(values);
        return RedirectToAction("Index");
    }
    #endregion

    #region Us2
    /*Us2 Codes start*/
    #region StudentDetails metodu ile öğrenci ve kurs detayları listelenir
    [Authorize(Roles = "Admin,User")]
    [HttpPost]
    public IActionResult StudentDetails(int id)
    {
        var values = _studentService.GetStudentWithCourses(id);

        return Json(new { StudentId = values.StudentId, FirstName = values.FirstName, LastName = values.LastName, BirthDate = values.BirthDate, CourseIds = string.Join(", ", values.StudentCourse.Select(sc => sc.CourseId).ToArray()) });
    }
    #endregion
    /*Us2 Codes end*/
    #endregion

    #region Us3
    /*Us3 Codes start*/
    #region CourseAssignStudent dropdownlist için kursları çeker
    [Authorize(Roles = "Admin,User")]
    [HttpGet]
    public IActionResult CourseAssignStudent(int id)
    {
        var getcourse = _courseService.TGetList();
        var students = _studentService.GetStudentWithCourses(id);
        List<SelectListItem> degerler = (from i in getcourse
                                         select new SelectListItem
                                         {
                                             Text = i.CourseName,
                                             Value = i.CourseId.ToString(),
                                             Selected = students.StudentCourse.Any(sc => sc.CourseId == i.CourseId)
                                         }).ToList();

        ViewBag.Courses = degerler;

        return View(students);
    }
    #endregion

    #region CourseAssignStudent post metodunda kursun ataması yapılır.

    [Authorize(Roles = "Admin,User")]
    [HttpPost]
    public IActionResult CourseAssignStudent(Student student, List<string> courseIds)
    {
        var oldStudent = _studentService.GetStudentWithCourses(student.StudentId);
        oldStudent.FirstName = student.FirstName;
        oldStudent.LastName = student.LastName;
        oldStudent.BirthDate = student.BirthDate;

        oldStudent.StudentCourse = courseIds.Select(c => new StudentCourse() { CourseId = c, StudentId = student.StudentId }).ToList();

        _studentService.TUpdate(oldStudent);

        return RedirectToAction("Index");

    }
    #endregion
    /*Us3 Codes end*/
    #endregion
    
    #region Us4
    /*Us3 Codes start*/
    #region GetListStudentandCourses öğrenci ve kursların eşleşerek getirilir
    [AllowAnonymous]
    public IActionResult GetListStudentandCourses()
    {
        var values = _studentService.GetStudentsByCourseId();
        return View(values);
    }
    #endregion
    /*Us3 Codes end*/
    #endregion

}