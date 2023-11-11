using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Student.Entity.Entities;

namespace Student.Entity.Configurations.UsersConfig
{
    public class CourseConfig : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasOne(b => b.Class).WithMany(b => b.Courses).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(b => b.User).WithMany(b => b.Courses).OnDelete(DeleteBehavior.Cascade);
            builder.HasQueryFilter(m => EF.Property<bool>(m, "IsDeleted") == false);
        }
    }
}
