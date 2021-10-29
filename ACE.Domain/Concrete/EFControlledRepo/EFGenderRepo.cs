﻿using ACE.Domain.Abstract;
using ACE.Domain.Abstract.IControlledRepo;
using ACE.Domain.Data;
using ACE.Domain.Entities.ControlledEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE.Domain.Concrete.EFControlledRepo
{
    public class EFGenderRepo : GenericRepository<Gender>, IGenderRepo
    {
        public EFGenderRepo(ACEContext context) : base(context)
        {

        }
    }
}
