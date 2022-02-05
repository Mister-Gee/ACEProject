using ACE.Domain.Abstract;
using ACE.Domain.Abstract.IControlledRepo;
using ACE.Domain.Entities;
using ACEdatabaseAPI.Data;
using ACEdatabaseAPI.Model;
using Microsoft.AspNetCore.Identity;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ACEdatabaseAPI.Helpers
{
    public class ExcelHelper : IExcelHelper
    {
        private readonly UserManager<ApplicationUser> _userManager;
        IDepartmentRepo _deptRepo;
        IProgrammeRepo _progRepo;
        ILevelRepo _levelRepo;
        ICourseRepo _courseRepo;
        IExamTimetableRepo _examTTRepo;
        public ExcelHelper(UserManager<ApplicationUser> userManager, IDepartmentRepo deptRepo, IProgrammeRepo progRepo, 
            ILevelRepo levelRepo, ICourseRepo courseRepo, IExamTimetableRepo examTTRepo)
        {
            _userManager = userManager;
            _deptRepo = deptRepo;
            _progRepo = progRepo;
            _levelRepo = levelRepo;
            _courseRepo = courseRepo;
            _examTTRepo = examTTRepo;
        }
        public async Task<(Status, List<string>, List<string>)> readStaffXLS(MemoryStream fileStream) 
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage package = new ExcelPackage(fileStream))
            {
                List<string> failedAccounts = new List<string>();
                List<string> successAccounts = new List<string>();


                //get the first worksheet in the workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                int colCount = worksheet.Dimension.End.Column;  //get Column Count
                int rowCount = worksheet.Dimension.End.Row;     //get row count
                for (int row = 2; row <= rowCount; row++)
                {
                    var user = new ApplicationUser();
                    var strippedPhone = $"0{worksheet.Cells[row, 6].Value.ToString().Substring(worksheet.Cells[row, 6].Value.ToString().Length - 10, 10)}";
                    user.FirstName = worksheet.Cells[row, 1].Value.ToString();
                    user.OtherName = worksheet.Cells[row, 2].Value.ToString();
                    user.LastName = worksheet.Cells[row, 3].Value.ToString();
                    user.StaffID = worksheet.Cells[row, 4].Value.ToString();
                    user.UserName = worksheet.Cells[row, 5].Value.ToString();
                    user.Email = worksheet.Cells[row, 5].Value.ToString();
                    user.PhoneNumber = strippedPhone;
                    user.IPPISNo = worksheet.Cells[row, 7].Value.ToString();
                    user.ForcePasswordChange = true;

                    var userCheck = _userManager.Users.Where(X => X.Email == worksheet.Cells[row, 5].Value.ToString()).FirstOrDefault();
                    if(userCheck == null)
                    {
                        try
                        {
                            var result = await _userManager.CreateAsync(user, user.StaffID);
                            if (result.Succeeded)
                            {
                                var role = await _userManager.AddToRoleAsync(user, "Staff");
                                if (role.Succeeded)
                                {
                                    successAccounts.Add(worksheet.Cells[row, 5].Value.ToString());
                                }
                            }
                            
                        }
                        catch(Exception ex)
                        {
                            string errors = worksheet.Cells[row, 5].Value.ToString() + ": " + ex.Message.ToString();
                            failedAccounts.Add(errors);
                        }
                    }
                }
                return (Status.Success, successAccounts, failedAccounts);
            }
        }

        public async Task<(Status, List<string>, List<string>)> readReturnStudentXLS(Guid ProgrammeID, Guid LevelID, Guid DepartmentID, MemoryStream fileStream)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage package = new ExcelPackage(fileStream))
            {
                List<string> failedAccounts = new List<string>();
                List<string> successAccounts = new List<string>();


                //get the first worksheet in the workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                int colCount = worksheet.Dimension.End.Column;  //get Column Count
                int rowCount = worksheet.Dimension.End.Row;     //get row count
                for (int row = 2; row <= rowCount; row++)
                {
                    var userCheck = _userManager.Users.Where(X => X.JambRegNumber == worksheet.Cells[row, 4].Value.ToString()).FirstOrDefault();
                    ApplicationUser user;
                    if(userCheck == null)
                    {
                        user = new ApplicationUser();
                    }
                    else
                    {
                        user = userCheck;
                    }
                    var strippedPhone = $"0{worksheet.Cells[row, 7].Value.ToString().Substring(worksheet.Cells[row, 7].Value.ToString().Length - 10, 10)}";

                    var dept = _deptRepo.FindBy(x => x.Id == DepartmentID).FirstOrDefault();
                    var prog = _progRepo.FindBy(x => x.Id == ProgrammeID).FirstOrDefault();
                    var level = _levelRepo.FindBy(x => x.Id == LevelID).FirstOrDefault();

                    user.FirstName = worksheet.Cells[row, 1].Value.ToString();
                    user.OtherName = worksheet.Cells[row, 2].Value.ToString();
                    user.LastName = worksheet.Cells[row, 3].Value.ToString(); 
                    user.JambRegNumber = worksheet.Cells[row, 4].Value.ToString();
                    user.MatricNumber = worksheet.Cells[row, 5].Value.ToString();
                    user.UserName = worksheet.Cells[row, 6].Value.ToString();
                    user.Email = worksheet.Cells[row, 6].Value.ToString();
                    user.PhoneNumber = strippedPhone;
                    user.ModeOfAdmission = worksheet.Cells[row, 8].Value.ToString();
                    user.SchoolID = dept.SchoolID;
                    user.DepartmentID = dept.Id;
                    user.ProgrammeID = prog.Id;
                    user.CurrentLevelID = level.Id;
                    user.ForcePasswordChange = true;
                    if (userCheck == null)
                    {
                        try
                        {
                            var result = await _userManager.CreateAsync(user, user.MatricNumber);
                            if (result.Succeeded)
                            {
                                var role = await _userManager.AddToRoleAsync(user, "Student");
                                if (role.Succeeded)
                                {
                                    successAccounts.Add(worksheet.Cells[row, 5].Value.ToString());
                                }
                            }

                        }
                        catch (Exception ex)
                        {
                            string errors = worksheet.Cells[row, 5].Value.ToString() + ": " + ex.Message.ToString();
                            failedAccounts.Add(errors);
                        }
                    }
                    else
                    {
                        try
                        {
                            var result = await _userManager.UpdateAsync(user);
                            if (result.Succeeded)
                            {
                                successAccounts.Add(worksheet.Cells[row, 5].Value.ToString());
                            }

                        }
                        catch (Exception ex)
                        {
                            string errors = worksheet.Cells[row, 5].Value.ToString() + ": " + ex.Message.ToString();
                            failedAccounts.Add(errors);
                        }
                    }
                }
                return (Status.Success, successAccounts, failedAccounts);
            }
        }

        public async Task<(Status, List<string>, List<string>)> readNewStudentXLS(Guid ProgrammeID, Guid LevelID, Guid DepartmentID, MemoryStream fileStream)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage package = new ExcelPackage(fileStream))
            {
                List<string> failedAccounts = new List<string>();
                List<string> successAccounts = new List<string>();


                //get the fi rst worksheet in the workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                int colCount = worksheet.Dimension.End.Column;  //get Column Count
                int rowCount = worksheet.Dimension.End.Row;     //get row count
                for (int row = 2; row <= rowCount; row++)
                {
                    var user = new ApplicationUser();
                    var dept = _deptRepo.FindBy(x => x.Id == DepartmentID).FirstOrDefault();
                    var prog = _progRepo.FindBy(x => x.Id == ProgrammeID).FirstOrDefault();
                    var level = _levelRepo.FindBy(x => x.Id == LevelID).FirstOrDefault();

                    var strippedPhone = $"0{worksheet.Cells[row, 5].Value.ToString().Substring(worksheet.Cells[row, 5].Value.ToString().Length - 10, 10)}";


                    user.FirstName = worksheet.Cells[row, 1].Value.ToString();
                    user.OtherName = worksheet.Cells[row, 2].Value.ToString();
                    user.LastName = worksheet.Cells[row, 3].Value.ToString();
                    user.JambRegNumber = worksheet.Cells[row, 4].Value.ToString();
                    user.PhoneNumber = strippedPhone;
                    user.ModeOfAdmission = worksheet.Cells[row, 6].Value.ToString();
                    user.SchoolID = dept.SchoolID;
                    user.DepartmentID = dept.Id;
                    user.ProgrammeID = prog.Id;
                    user.CurrentLevelID = level.Id;
                    user.ForcePasswordChange = true;
                    var userCheck = _userManager.Users.Where(X => X.Email == worksheet.Cells[row, 5].Value.ToString()).FirstOrDefault();
                    if (userCheck == null)
                    {
                        try
                        {
                            var result = await _userManager.CreateAsync(user, user.JambRegNumber);
                            if (result.Succeeded)
                            {
                                var role = await _userManager.AddToRoleAsync(user, "Student");
                                if (role.Succeeded)
                                {
                                    successAccounts.Add(worksheet.Cells[row, 5].Value.ToString());
                                }
                            }

                        }
                        catch (Exception ex)
                        {
                            string errors = worksheet.Cells[row, 5].Value.ToString() + ": " + ex.Message.ToString();
                            failedAccounts.Add(errors);
                        }
                    }
                }
                return (Status.Success, successAccounts, failedAccounts);
            }
        }

        public async Task<(Status, List<string>)> ExamTimeTable(string username, string ip, MemoryStream fileStream)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage package = new ExcelPackage(fileStream))
            {
                List<string> failedTimeTable = new List<string>();


                //get the first worksheet in the workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                int colCount = worksheet.Dimension.End.Column;  //get Column Count
                int rowCount = worksheet.Dimension.End.Row;     //get row count
                for (int row = 2; row <= rowCount; row++)
                {
                    var examtTT = new ExamTimetable();

                    var course = _courseRepo.FindBy(x => x.CourseCode.ToUpper() == worksheet.Cells[row, 4].Value.ToString().ToUpper()).FirstOrDefault();
                    var supervisor = _userManager.Users.Where(x => x.StaffID.ToUpper() == worksheet.Cells[row, 6].Value.ToString().ToUpper()).FirstOrDefault();

                    var date = DateTime.Parse(worksheet.Cells[row, 1].Value.ToString());
                    string time = worksheet.Cells[row, 2].Value.ToString();

                    var timeSplit = time.Split(":".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                    DateTime obj = new DateTime(date.Year, date.Month, date.Day, int.Parse(timeSplit[0]), int.Parse(timeSplit[1]), 0);

                    examtTT.ExamDateTime = obj;
                    examtTT.ExamStartTime = worksheet.Cells[row, 2].Value.ToString();
                    examtTT.ExamDuration = worksheet.Cells[row, 3].Value.ToString();
                    examtTT.CourseID = course.Id;
                    examtTT.Venue = worksheet.Cells[row, 5].Value.ToString();
                    examtTT.SupervisorID = Guid.Parse(supervisor.Id);

                    if (supervisor == null)
                    {
                        string error = worksheet.Cells[row, 6].Value.ToString() + " Does not exist for " + worksheet.Cells[row, 4].Value.ToString();
                        failedTimeTable.Add(error);
                    }
                    else
                    {
                        try
                        {
                            _examTTRepo.Add(examtTT);
                            _examTTRepo.Save(username, ip);
                        }
                        catch (Exception ex)
                        {
                            string errors = worksheet.Cells[row, 4].Value.ToString() + ": " + ex.Message.ToString();
                            failedTimeTable.Add(errors);
                        }
                    }
                }
                return (Status.Success, failedTimeTable);
            }
        }
    }

}
