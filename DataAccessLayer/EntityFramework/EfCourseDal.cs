using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;

namespace DataAccessLayer.EntityFramework
{
    public class EfCourseDal : GenericRepository<Course>, ICourseDal
    {
        private readonly EntityContext _context;
        public EfCourseDal(EntityContext context) : base(context)
        {
            _context = context;
        }
    }
}
