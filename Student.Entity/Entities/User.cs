﻿using Student.Entity.Entities.BaseEntities;
using Student.Infrastructure.Enums.EntityEnums;

namespace Student.Entity.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public UserType Type { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}
