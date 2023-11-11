using MediatR;
using Student.BLL.Extensions;
using Student.DAL.Repository;
using Student.DTO.ViewModels.Paginations;
using Student.DTO.ViewModels.Users.ResModels;
using Student.Entity.Entities;
using Student.Infrastructure.Enums.EntityEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student.BLL.Services.UserService.Services.Queries
{
    public class GetStudentListWithAssignedClassesQueryHandler : IRequestHandler<GetStudentListWithAssignedClassesQuery, PagingResponse<GetStudentListWithAssignedClassesResModel>>
    {
        private readonly IEntityRepository _repository;
        public GetStudentListWithAssignedClassesQueryHandler(IEntityRepository repository)
        {
            _repository = repository;
        }

        public async Task<PagingResponse<GetStudentListWithAssignedClassesResModel>> Handle(GetStudentListWithAssignedClassesQuery query, CancellationToken token)
        {
            try
            {
                return await _repository.FilterAsNoTracking<User>(u => u.Type == UserType.Student)
                    .Select(u => new GetStudentListWithAssignedClassesResModel
                    {
                         Id = u.Id,
                          FullName = u.FullName,
                           AssignedCourses = u.Courses.Select(c => new GetStudentAssignedCourses
                           {
                                ClassId = c.ClassId,
                                 CourseName = c.Class.Name
                           }).ToList()
                    }).ToPagingAsync(query.paging);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e.InnerException);
            }
        }
    }
}

