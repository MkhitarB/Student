using System.ComponentModel.DataAnnotations;

namespace Student.Entity.Entities.BaseEntities
{
    public interface IBaseEntity
    {
        [Key]
        int Id { get; set; }
        int CreatedBy { get; set; }
        DateTime CreatedDt { get; set; }
        int UpdatedBy { get; set; }
        DateTime UpdatedDt { get; set; }
        bool IsDeleted { get; set; }
    }
}
