﻿using ACE.Domain.Abstract;
using ACE.Domain.Data;
using ACE.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE.Domain.Concrete
{
    public class EFvExamRecordsRepo : GenericViewRepository<vExamRecords>, IvExamRecordsRepo
    {
        public EFvExamRecordsRepo(ACEViewContext context) : base(context)
        {

        }
    }
}
