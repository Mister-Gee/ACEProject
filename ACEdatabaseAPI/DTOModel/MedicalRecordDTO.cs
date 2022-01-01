using ACE.Domain.Entities;
using ACE.Domain.Entities.ControlledEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACEdatabaseAPI.DTOModel
{
    public class MedicalRecordDTO
    {
        public vMedicalRecord Record { get; set; }
        public List<MedicalCondition> MedicalConditionsList { get; set; }
    }
}
