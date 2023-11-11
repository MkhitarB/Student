using Student.Entity.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Student.Entity.Entities
{
    public class Course : BaseEntity
    {
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        [ForeignKey(nameof(Class))]
        public int ClassId { get; set; }

        public virtual User User { get; set; }
        public virtual Class Class { get; set; }
    }
}
