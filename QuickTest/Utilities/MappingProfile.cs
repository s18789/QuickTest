using AutoMapper;
using QuickTest.Application.Groups;
using QuickTest.Application.Schools;
using QuickTest.Application.Students;
using QuickTest.Application.Teachers;
using QuickTest.Application.Users.UserRole;
using QuickTest.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTest.Infrastructure.Utilities
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Student, StudentDto>()
                .ForMember(dest => dest.Group, opt => opt.MapFrom(src => src.Group));

            CreateMap<StudentDto, Student>()
                .ForMember(dest => dest.Group, opt => opt.Ignore());
            CreateMap<UserRole, UserRoleDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.RoleName));
            CreateMap<UserRoleDto, UserRole>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.RoleName));

            CreateMap<TeacherDto, Teacher>()
                .ForMember(dest => dest.GroupTeachers, opt => opt.MapFrom(src => src.Group.Select(gdto => new GroupTeacher { GroupId = gdto.Id })));

            CreateMap<Teacher, TeacherDto>()
                .ForMember(dest => dest.Group, opt => opt.MapFrom(src => src.GroupTeachers.Select(gt => gt.Group)));

            CreateMap<School, SchoolDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.Groups, opt => opt.MapFrom(src => src.Groups));

            CreateMap<SchoolDto, School>()
           .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
           .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
           .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
           .ForMember(dest => dest.Groups, opt => opt.MapFrom(src => src.Groups));

            CreateMap<Group, GroupDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Students, opt => opt.MapFrom(src => src.Students))
            .ForMember(dest => dest.GroupTeachers, opt => opt.MapFrom(src => src.GroupTeachers))
            .ForMember(dest => dest.School, opt => opt.MapFrom(src => src.School));

            CreateMap<GroupDto, Group>()
                .ForMember(dest => dest.Students, opt => opt.MapFrom(src => src.Students))
                .ForMember(dest => dest.GroupTeachers, opt => opt.MapFrom(src => src.GroupTeachers))
                .ForMember(dest => dest.School, opt => opt.MapFrom(src => src.School));
            CreateMap<GroupTeacher, GroupTeacherDto>();
            CreateMap<GroupTeacherDto, GroupTeacher>();
        }
    }
}
