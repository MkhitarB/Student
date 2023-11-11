namespace Student.DTO.ViewModels.Courses.ResModels
{
    public class GetUserWithAssignedClassesResModel
    {
        public int ClassId { get; set; }
        public string CourseName { get; set; } 
        public bool IsAssigned { get; set; }
    }
}
