using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfStudentDal : GenericRepository<Student>, IStudentDal
    {
        private readonly EntityContext _context;

        public EfStudentDal(EntityContext context): base(context)
        {
            _context = context;
        }

        public List<Student> GetListCourses()
        {
            return _context.Students.Include(x => x.StudentCourse).ToList();
        }
        
        public List<Student> GetListById(int id)
        {
            return _context.Students.Where(s => s.StudentId == id).ToList();
        }
        
        public Student GetWithCourses(int id)
        {
            return _context.Students.Include(x => x.StudentCourse).FirstOrDefault(s => s.StudentId == id);
        }
    }
}
