using AutoMapper;
using QuickTest.Application.Groups;
using QuickTest.Application.Students;
using QuickTest.Application.Teachers;
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
                .ForMember(dest => dest.GroupDto, opt => opt.MapFrom(src => src.Group));

            CreateMap<StudentDto, Student>()
                .ForMember(dest => dest.Group, opt => opt.Ignore());

            CreateMap<TeacherDto, Teacher>()
                .ForMember(dest => dest.GroupTeachers, opt => opt.MapFrom(src => src.GroupDtos.Select(gdto => new GroupTeacher { GroupId = gdto.Id })));

            CreateMap<Teacher, TeacherDto>()
                .ForMember(dest => dest.GroupDtos, opt => opt.MapFrom(src => src.GroupTeachers.Select(gt => gt.Group)));

            CreateMap<Group, GroupDto>()
            .ForMember(dest => dest.Students, opt => opt.MapFrom(src => src.Students))
            .ForMember(dest => dest.GroupTeachers, opt => opt.MapFrom(src => src.GroupTeachers))
            .ForMember(dest => dest.School, opt => opt.MapFrom(src => src.School));

            CreateMap<GroupDto, Group>()
                .ForMember(dest => dest.Students, opt => opt.MapFrom(src => src.Students))
                .ForMember(dest => dest.GroupTeachers, opt => opt.MapFrom(src => src.GroupTeachers))
                .ForMember(dest => dest.School, opt => opt.MapFrom(src => src.School));
        }
    }
}
