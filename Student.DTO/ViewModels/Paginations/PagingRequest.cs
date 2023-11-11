using Student.DTO.ViewModels.Bases;
using System.ComponentModel.DataAnnotations;

namespace Student.DTO.ViewModels.Paginations
{
    public class PagingRequest : IViewModel
    {
        [Required]
        public int Count { get; set; }
        [Required]
        public int Page { get; set; }
    }
}
