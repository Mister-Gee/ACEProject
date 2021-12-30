﻿using ACEdatabaseAPI.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ACEdatabaseAPI.Helpers
{
    public interface IExcelHelper 
    {
        Task<(Status, List<string>, List<string>)> readStaffXLS(Stream fileStream);
        Task<(Status, List<string>, List<string>)> readReturnStudentXLS(Guid ProgrammeID, Guid LevelID, Guid DepartmentID, Stream fileStream);
        Task<(Status, List<string>, List<string>)> readNewStudentXLS(Guid ProgrammeID, Guid LevelID, Guid DepartmentID, Stream fileStream);
        Task<(Status, List<string>)> ExamTimeTable(string username, string ip, Stream fileStream);


    }
}
