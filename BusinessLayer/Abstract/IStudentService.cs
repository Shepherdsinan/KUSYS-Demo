using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IStudentService : IGenericService<Student>
    {
        List<Student> GetStudentsByCourseId();

        Student GetStudentWithCourses(int id);
    }
}
