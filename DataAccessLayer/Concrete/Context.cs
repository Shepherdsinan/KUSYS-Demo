using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;

namespace DataAccessLayer.Concrete
{
    public class EntityContext : IdentityDbContext<AppUser, AppRole, int>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseSqlite("Data Source=KUSYSDemo.db");
           // optionsBuilder.UseSqlServer("server=localhost;database=KUSYSDemo;User Id=sa;Password=1;");
        //optionsBuilder.UseInMemoryDatabase(databaseName: "KUSYSDemo"); //Test connstring

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Course>(entity =>
            {
                entity.Property(p => p.CourseId).HasColumnName("CourseID").HasColumnType("NCHAR").HasMaxLength(6);
                entity.Property(p => p.CourseName).IsRequired().HasMaxLength(50);
            });

            modelBuilder.Entity<Course>().HasData
            (
                new Course() { CourseId = "CSI101", CourseName = "Introduction to Computer Science" },
                new Course() { CourseId = "CSI102", CourseName = "Algorithms" },
                new Course() { CourseId = "MAT101", CourseName = "Calculus" },
                new Course() { CourseId = "PHY101", CourseName = "Physics" }
            );

            modelBuilder.Entity<AppRole>().HasData(
                new AppRole()
                {
                    Id = 1,
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new AppRole()
                {
                    Id = 2,
                    Name = "User",
                    NormalizedName = "USER"
                }
            );

            var hasher = new PasswordHasher<AppUser>();

            var admin = new AppUser()
            {
                Id = 1,
                Email = "admin@example.com",
                UserName = "admin",
                SecurityStamp = Guid.NewGuid().ToString()
            };

            admin.NormalizedUserName = admin.UserName.ToUpperInvariant();
            admin.NormalizedEmail = admin.Email.ToUpperInvariant();
            admin.PasswordHash = hasher.HashPassword(admin, "admin");
            admin.StudentId = 0;
            
            var user = new AppUser()
            {
                Id = 2,
                Email = "user@example.com",
                UserName = "202301001",
                SecurityStamp = Guid.NewGuid().ToString()
            };

            user.NormalizedUserName = user.UserName.ToUpperInvariant();
            user.NormalizedEmail = user.Email.ToUpperInvariant();
            user.PasswordHash = hasher.HashPassword(user, "202301001");
            user.StudentId = 202301001;
            

            modelBuilder.Entity<AppUser>().HasData(
                admin,user
            );

            modelBuilder.Entity<IdentityUserRole<int>>().HasData(
                new IdentityUserRole<int>()
                {
                    UserId = 1,
                    RoleId = 1
                },
                new IdentityUserRole<int>()
                {
                    UserId = 2,
                    RoleId = 2
                }
            );

            modelBuilder.Entity<Student>().HasOne(u => u.AppUser).WithOne(u => u.Student)
                .HasForeignKey<Student>(u => u.AppUserId);

            modelBuilder.Entity<Student>().HasData(
                new Student()
                {
                    StudentId = 202301001,
                    FirstName = "Hüsnü",
                    LastName = "Çoban",
                    BirthDate = new DateTime(2022, 06, 16),
                },
                new Student()
                {
                    StudentId = 202301002,
                    FirstName = "Rıza",
                    LastName = "Çoban",
                    BirthDate = new DateTime(2000, 05, 15), //Convert.ToDateTime("2000-05-15"),
                }
            );

            modelBuilder.Entity<StudentCourse>().HasKey(sc => new { sc.StudentId, sc.CourseId });
            modelBuilder.Entity<StudentCourse>().HasData(
                new { StudentId = 202301001, CourseId = "CSI101" },
                new { StudentId = 202301001, CourseId = "MAT101" },
                new { StudentId = 202301002, CourseId = "CSI102" },
                new { StudentId = 202301002, CourseId = "PHY101" }
            );
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
    }
}