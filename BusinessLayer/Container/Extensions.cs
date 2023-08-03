using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLayer.Container;
public static class Extensions
{
    public static void ContainerDependencies(this IServiceCollection services)
    {
        services.AddScoped<IStudentService, StudentManager>();        
        services.AddScoped<IStudentDal, EfStudentDal>();
        services.AddScoped<IStudentCourseService, StudentCourseManager>();
        services.AddScoped<IStudentCourseDal, EfStudentCourseDal>();
        services.AddScoped<ICourseService, CourseManager>();
        services.AddScoped<ICourseDal, EfCourseDal>();
        services.AddScoped<IValidator<Student>, StudentValidator>();
    }
}
