using ACE.Domain.Entities.ControlledEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE.Domain.Abstract.IControlledRepo
{
    public interface IMedicalConditionsRepo : IGenericRepository<MedicalCondition>
    {
    }
}
