using Student.Entity.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Student.Entity.Entities
{
    public class Class : BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}
