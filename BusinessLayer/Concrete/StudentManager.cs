using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete;
public class StudentManager : IStudentService
{
    IStudentDal _studentDal;

    public StudentManager(IStudentDal studentDal)
    {
        _studentDal = studentDal;
    }

    public List<Student> GetStudentsByCourseId()
    {
        return _studentDal.GetListCourses();
    }
    
    public Student GetStudentWithCourses(int id)
    {
        return _studentDal.GetWithCourses(id);
    }
    
    public void TAdd(Student entity)
    {
        _studentDal.Insert(entity);
    }

    public void TDelete(Student entity)
    {
        _studentDal.Delete(entity);
    }

    public List<Student> TGetList()
    {
        return _studentDal.GetList();
    }

    public Student TGetById(int id)
    {
        return _studentDal.GetByID(id);
    }

    public void TUpdate(Student entity)
    {
        _studentDal.Update(entity);
    }
}
