using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete;
public class StudentCourseManager : IStudentCourseService
{
    IStudentCourseDal _studentCourseDal;

    public StudentCourseManager(IStudentCourseDal studentCourseDal)
    {
        _studentCourseDal = studentCourseDal;
    }

    public void TAdd(StudentCourse entity)
    {
        _studentCourseDal.Insert(entity);
    }

    public void TDelete(StudentCourse entity)
    {
        _studentCourseDal.Delete(entity);
    }

    public List<StudentCourse> TGetList()
    {
        return _studentCourseDal.GetList();
    }

    public StudentCourse TGetById(int id)
    {
        return _studentCourseDal.GetByID(id);
    }

    public void TUpdate(StudentCourse entity)
    {
        _studentCourseDal.Update(entity);
    }
}
