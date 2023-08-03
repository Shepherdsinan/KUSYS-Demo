namespace EntityLayer.Concrete;
public class StudentCourse
{
    public int StudentId { get; set; }
    public Student Student { get; set; }
    public string CourseId { get; set; }
    public Course Course { get; set; }
}
