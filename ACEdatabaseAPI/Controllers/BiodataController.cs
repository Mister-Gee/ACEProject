using ACE.Domain.Abstract.IControlledRepo;
using ACEdatabaseAPI.Data;
using ACEdatabaseAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACEdatabaseAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BiodataController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        IReligionRepo _religionRepo;
        IMaritalStatusRepo _maritalStatusRepo;
        ILevelRepo _levelRepo;
        ISchoolRepo _schoolRepo;
        IDepartmentRepo _deptRepo;
        IGenderRepo _genderRepo;
        IProgrammeRepo _progRepo;


        public BiodataController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IReligionRepo religionRepo, IMaritalStatusRepo maritalStatusRepo,
            ILevelRepo levelRepo, ISchoolRepo schoolRepo, IDepartmentRepo deptRepo, IGenderRepo genderRepo, IProgrammeRepo progRepo)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _religionRepo = religionRepo;
            _maritalStatusRepo = maritalStatusRepo;
            _levelRepo = levelRepo;
            _schoolRepo = schoolRepo;
            _deptRepo = deptRepo;
            _genderRepo = genderRepo;
            _progRepo = progRepo;
        }

        [HttpPut]
        [Route("/Student/Update/{Id}")]
        public async Task<IActionResult> UpdateStudentData(Guid Id, StudentBioData model)
        {
            if (ModelState.IsValid)
            {
                var student = _userManager.FindByIdAsync(Id.ToString()).Result;

                var gender = _genderRepo.FindBy(x => x.Id == model.Gender).FirstOrDefault();
                var prog = _progRepo.FindBy(x => x.Id == model.Programme).FirstOrDefault();
                var maritalStatus = _maritalStatusRepo.FindBy(x => x.Id == model.MaritalStatus).FirstOrDefault();
                var cLevel = _genderRepo.FindBy(x => x.Id == model.Gender).FirstOrDefault();
                //var gender = _genderRepo.FindBy(x => x.Id == model.Gender).FirstOrDefault();
                //var gender = _genderRepo.FindBy(x => x.Id == model.Gender).FirstOrDefault();
                //var gender = _genderRepo.FindBy(x => x.Id == model.Gender).FirstOrDefault();

            }
            return Ok();
        }
    }
}
