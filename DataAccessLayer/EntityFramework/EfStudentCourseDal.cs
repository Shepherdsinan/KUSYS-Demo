using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;

namespace DataAccessLayer.EntityFramework;

public class EfStudentCourseDal : GenericRepository<StudentCourse>, IStudentCourseDal
{
    private readonly EntityContext _context;
    
    public EfStudentCourseDal(EntityContext context) : base(context)
    {
        _context = context;
    }
}