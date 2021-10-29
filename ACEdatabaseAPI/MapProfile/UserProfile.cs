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
}
