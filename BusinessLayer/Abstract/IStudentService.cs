using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IStudentService : IGenericService<Student>
    {
        List<Student> GetStudentsByCourseId();
        List<Student> GetListById(int Id);
        
        Student GetStudentWithCourses(int id);
    }
}
