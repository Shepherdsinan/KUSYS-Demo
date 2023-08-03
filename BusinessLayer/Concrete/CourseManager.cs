using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    internal class CourseManager : ICourseService
    {
        ICourseDal _courseDal;

        public CourseManager(ICourseDal courseDal)
        {
            _courseDal = courseDal;
        }

        public void TAdd(Course entity)
        {
            _courseDal.Insert(entity);
        }

        public void TDelete(Course entity)
        {
            _courseDal.Delete(entity);
        }

        public List<Course> TGetList()
        {
            return _courseDal.GetList();
        }

        public Course TGetById(int id)
        {
            return _courseDal.GetByID(id);
        }

        public void TUpdate(Course entity)
        {
            _courseDal.Update(entity);
        }
    }
}
