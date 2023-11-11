using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student.DTO.ViewModels.Users.ResModels
{
    public class GetStudentListWithAssignedClassesResModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public List<GetStudentAssignedCourses> AssignedCourses { get; set; }
    }

    public class GetStudentAssignedCourses
    {
        public int ClassId { get; set; }
        public string CourseName { get; set; }
    } 
}
