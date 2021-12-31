using ACE.Domain.Entities;
using ACE.Domain.Entities.ControlledEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ACE.Domain.Data
{
    public class ACEContext : DbContext, IDbContext
    {
        public ACEContext(DbContextOptions<ACEContext> options) : base(options)
        {
            
        }

        public virtual DbSet<AuditLog> AuditLogs { get; set; }
        public virtual DbSet<CurrentAcademicSession> CurrentAcademicSessions { get; set; }
        public virtual DbSet<Device> Devices { get; set; }
        public virtual DbSet<School> Schools { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<Level> Levels { get; set; }
        public virtual DbSet<MaritalStatus> MaritalStatus { get; set; }
        public virtual DbSet<Programme> Programmes { get; set; }
        public virtual DbSet<Religion> Religions { get; set; }
        public virtual DbSet<StudentCategory> StudentCategories { get; set; }

        public virtual DbSet<BloodGroup> BloodGroup { get; set; }
        public virtual DbSet<Genotype> Genotype { get; set; }
        public virtual DbSet<MedicalRecord> MedicalRecords { get; set; }
        public virtual DbSet<MedicalHistory> MedicalHistory { get; set; }
        public virtual DbSet<FlagLevel> FlagLevels { get; set; }
        public virtual DbSet<Flag> Flags { get; set; }
        public virtual DbSet<AcademicYear> AcademicYears { get; set; }
        public virtual DbSet<Semester> Semesters { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<StudentRegisteredCourse> StudentRegisteredCourses { get; set; }
        public virtual DbSet<CourseRegisteration> CourseRegisterations { get; set; }
        public virtual DbSet<ClassAttendance> ClassAttendances { get; set; }
        public virtual DbSet<ExamTimetable> ExamTimetables { get; set; }
        public virtual DbSet<ExamAttendance> ExamAttendances { get; set; }
        public virtual DbSet<GradingUnit> GradingUnits { get; set; }
        public virtual DbSet<ExamRecords> ExamRecords { get; set; }



        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<vUserRole>(c => {
        //        c.HasNoKey();
        //        c.ToView("vUserRole");
        //    });
        //}






        public int SaveChanges(string userID, string IP)
        {
            try
            {
                // Get all Added/Deleted/Modified entities (not Unmodified or Detached)
                var changedEntity = this.ChangeTracker.Entries()
                            .Where(p => p.State == EntityState.Added || p.State == EntityState.Modified || p.State == EntityState.Deleted)
                            .ToList();

                foreach(var ent in changedEntity)
                {
                    // For each changed record, get the audit record entries and add them
                    var auditRecordsForChange = GetAuditRecordsForChange(ent, userID, IP);

                    foreach(var x in auditRecordsForChange)
                    {
                        this.AuditLogs.Add(x);
                    }
                }
                return base.SaveChanges();
            }
            catch(Exception x)
            {
                throw;
            }
        }

        public async Task<int> SaveChangesAsync(string userID, string IP, CancellationToken token = default)
        {
            try
            {
                // Get all Added/Deleted/Modified entities (not Unmodified or Detached)
                var changedEntity = this.ChangeTracker.Entries().Where(p => p.State == EntityState.Added ||
                                                                            p.State == EntityState.Deleted ||
                                                                            p.State == EntityState.Modified).ToList();

                foreach (var ent in changedEntity)
                {
                    // For each changed record, get the audit record entries and add them
                    var auditRecordsForChange = GetAuditRecordsForChange(ent, userID, IP);

                    foreach (var x in auditRecordsForChange)
                    {
                        await this.AuditLogs.AddAsync(x, token);
                    }
                }
                // Call the original SaveChangesAsync(), which will save both the changes made and the audit records
                return await base.SaveChangesAsync(token);
            }
            catch(Exception x)
            {
                throw;
            }
        }

        private List<AuditLog> GetAuditRecordsForChange(EntityEntry dbEntry, string userID, string IP)
        {
            List<AuditLog> result = new List<AuditLog>();

            DateTime changeTime = DateTime.UtcNow;

            // Get the Table() attribute, if one exists
            TableAttribute tableAttribute = dbEntry.Entity.GetType().GetCustomAttributes(typeof(TableAttribute), false).SingleOrDefault() as TableAttribute;

            // Get table name (if it has a Table attribute, use that, otherwise get the pluralized name)
            string tableName = tableAttribute != null ? tableAttribute.Name : dbEntry.Entity.GetType().Name;

            // Get primary key value (If you have more than one key column, this will need to be adjusted)
            if(dbEntry != null)
            {
                string keyName = dbEntry.Entity.GetType().GetProperties().FirstOrDefault(p => p.GetCustomAttributes(typeof(KeyAttribute), false).Length > 0).Name;
                if(dbEntry.State == EntityState.Added)
                {
                    // For Inserts, just add the whole record
                    // If the entity implements IDescribableEntity, use the description from Describe(), otherwise use ToString()
                    var x = new AuditLog();
                    x.AuditLogId = Guid.NewGuid();
                    x.UserId = userID;
                    x.EventDateUtc = changeTime;
                    x.EventType = "A"; //Added
                    x.Ip = IP;
                    x.TableName = tableName;
                    x.RecordId = dbEntry.Property(keyName).CurrentValue.ToString();
                    x.ColumnName = "*All";
                    x.NewValue = JsonConvert.SerializeObject(dbEntry.CurrentValues.ToObject());
                    result.Add(x);
                }
                else if(dbEntry.State == EntityState.Deleted)
                {
                    var x = new AuditLog();
                    x.AuditLogId = Guid.NewGuid();
                    x.UserId = userID;
                    x.EventDateUtc = changeTime;
                    x.EventType = "D"; //Deleted
                    x.Ip = IP;
                    x.TableName = tableName;
                    x.RecordId = dbEntry.OriginalValues[keyName].ToString();
                    x.ColumnName = "*All";
                    x.NewValue = JsonConvert.SerializeObject(dbEntry.OriginalValues.ToObject());
                    result.Add(x);
                }
                else if(dbEntry.State == EntityState.Modified)
                {
                    foreach(var propertyName in dbEntry.OriginalValues.Properties)
                    {
                        if(!object.Equals(dbEntry.OriginalValues[propertyName], dbEntry.CurrentValues[propertyName]))
                        {
                            var x = new AuditLog();
                            x.AuditLogId = Guid.NewGuid();
                            x.UserId = userID;
                            x.EventDateUtc = changeTime;
                            x.EventType = "M"; //Modified
                            x.Ip = IP;
                            x.TableName = tableName;
                            x.RecordId = dbEntry.OriginalValues[keyName].ToString();
                            x.ColumnName = propertyName.Name;
                            x.OriginalValue = dbEntry.OriginalValues[propertyName] == null ? null : dbEntry.OriginalValues[propertyName].ToString();
                            x.NewValue = dbEntry.CurrentValues[propertyName] == null ? null : dbEntry.CurrentValues[propertyName].ToString();
                            result.Add(x);

                        }
                    }
                }
            }
            return result;

        }

    }

    public class ACEViewContext : DbContext, IDbContext
    {
        public ACEViewContext(DbContextOptions<ACEViewContext> option) : base(option)
        {

        }

        public virtual DbSet<vStaff> vStaffs { get; set; }
        public virtual DbSet<vStudent> vStudents { get; set; }
        public virtual DbSet<vCourse> vCourses { get; set; }
        public virtual DbSet<vCourseRegisteration> vCourseRegisterations { get; set; }
        public virtual DbSet<vStudentRegisteredCourse> vStudentRegisteredCourses { get; set; }
        public virtual DbSet<vExamAttendance> vExamAttendances { get; set; }





        public int SaveChanges(string userID, string IP)
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync(string userID, string IP, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }
    }
}
