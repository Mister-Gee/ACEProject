using ACE.Domain.Abstract.IControlledRepo;
using ACE.Domain.Entities;
using ACE.Domain.Entities.ControlledEntities;
using ACEdatabaseAPI.CreateModel;
using ACEdatabaseAPI.Data;
using ACEdatabaseAPI.DTOModel;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACEdatabaseAPI.MapProfile
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<StudentBioData, ApplicationUser>().ReverseMap();
        }
    }

    public class StudentDTOProfile : Profile
    {
      
        public StudentDTOProfile()
        {
            CreateMap<ApplicationUser, StudentDTO>()
               .ForMember(destinationMember => destinationMember.Gender, memberOptions => memberOptions.MapFrom(x => x.Gender.Name))
               .ForMember(destinationMember => destinationMember.Religion, memberOptions => memberOptions.MapFrom(x => x.Religion.Name))
               .ForMember(destinationMember => destinationMember.MaritalStatus, memberOptions => memberOptions.MapFrom(x => x.MaritalStatus.Name))
               .ForMember(destinationMember => destinationMember.Department, memberOptions => memberOptions.MapFrom(x => x.Departments.Name))
               .ForMember(destinationMember => destinationMember.School, memberOptions => memberOptions.MapFrom(x => x.School.Name))
               .ForMember(destinationMember => destinationMember.StudentCategory, memberOptions => memberOptions.MapFrom(x => x.StudentCategory.Name))
               .ForMember(destinationMember => destinationMember.Programme, memberOptions => memberOptions.MapFrom(x => x.Programme.Name))
               .ForMember(destinationMember => destinationMember.EntryLevel, memberOptions => memberOptions.MapFrom(x => x.Level.Name))
               .ForMember(destinationMember => destinationMember.CurrentLevel, memberOptions => memberOptions.MapFrom(x => x.Level.Name))
               .ReverseMap();
        }
    }

    public class StaffDTOProfile : Profile
    {
        public StaffDTOProfile()
        {
            CreateMap<ApplicationUser, StaffDTO>()
              .ForMember(destinationMember => destinationMember.Gender, memberOptions => memberOptions.MapFrom(x => x.Gender.Name))
              .ForMember(destinationMember => destinationMember.Religion, memberOptions => memberOptions.MapFrom(x => x.Religion.Name))
              .ForMember(destinationMember => destinationMember.MaritalStatus, memberOptions => memberOptions.MapFrom(x => x.MaritalStatus.Name))
              .ForMember(destinationMember => destinationMember.Department, memberOptions => memberOptions.MapFrom(x => x.Departments.Name))
              .ForMember(destinationMember => destinationMember.School, memberOptions => memberOptions.MapFrom(x => x.School.Name))
              .ReverseMap();
        }
    }

    public class FlagProfile : Profile
    {
        public FlagProfile()
        {
            CreateMap<Flag, FlagStudent>()
            .ReverseMap();
        }
    }

    public class FlagDTOProfile : Profile
    {
        public FlagDTOProfile()
        {
            CreateMap<Flag, FlagDTO>()
              .ForMember(destinationMember => destinationMember.FlagLevel, memberOptions => memberOptions.MapFrom(x => x.FlagLevel.Name))
              .ReverseMap();
        }
    }

    public class CourseProfile : Profile
    {
        public CourseProfile()
        {
            CreateMap<Course, CreateCourse>()
              .ReverseMap();
        }
    }

    public class CourseRegProfile : Profile
    {
        public CourseRegProfile()
        {
            CreateMap<StudentRegisteredCourse, RegisterCourse>()
              .ReverseMap();
        }
    }
}