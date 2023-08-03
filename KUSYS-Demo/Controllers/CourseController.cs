using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KUSYS_Demo.Controllers;
public class CourseController : Controller
{
    private readonly ICourseService _courseService;

    public CourseController(ICourseService courseService)
    {
        _courseService = courseService;
    }
    #region Index metodu ile tüm kurslar listelenir
    [AllowAnonymous]
    public IActionResult Index(){
        var value = _courseService.TGetList();
        return View(value);
    }
    #endregion
}
