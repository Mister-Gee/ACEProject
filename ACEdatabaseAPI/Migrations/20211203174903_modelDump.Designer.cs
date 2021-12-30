﻿// <auto-generated />
using System;
using ACEdatabaseAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ACEdatabaseAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20211203174903_modelDump")]
    partial class modelDump
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.11");

            modelBuilder.Entity("ACE.Domain.Entities.ControlledEntities.Department", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("SchoolID")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("SchoolID");

                    b.ToTable("Departments", t => t.ExcludeFromMigrations());
                });

            modelBuilder.Entity("ACE.Domain.Entities.ControlledEntities.Gender", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Genders", t => t.ExcludeFromMigrations());
                });

            modelBuilder.Entity("ACE.Domain.Entities.ControlledEntities.Level", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Levels", t => t.ExcludeFromMigrations());
                });

            modelBuilder.Entity("ACE.Domain.Entities.ControlledEntities.MaritalStatus", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("MaritalStatus", t => t.ExcludeFromMigrations());
                });

            modelBuilder.Entity("ACE.Domain.Entities.ControlledEntities.Programme", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Programmes", t => t.ExcludeFromMigrations());
                });

            modelBuilder.Entity("ACE.Domain.Entities.ControlledEntities.Religion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Religions", t => t.ExcludeFromMigrations());
                });

            modelBuilder.Entity("ACE.Domain.Entities.ControlledEntities.School", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Schools", t => t.ExcludeFromMigrations());
                });

            modelBuilder.Entity("ACE.Domain.Entities.ControlledEntities.StudentCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("StudentCategories", t => t.ExcludeFromMigrations());
                });

            modelBuilder.Entity("ACEdatabaseAPI.Data.ApplicationRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Category")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("FriendlyName")
                        .HasColumnType("TEXT");

                    b.Property<int?>("GroupId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("ACEdatabaseAPI.Data.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("AdmissionDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("AlternatePhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("CurrentLevelID")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("DepartmentID")
                        .HasColumnType("TEXT");

                    b.Property<string>("Disability")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("EmploymentDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("EntryLevelID")
                        .HasColumnType("TEXT");

                    b.Property<string>("FacebookID")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<bool>("ForcePasswordChange")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FormerName")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("GenderID")
                        .HasColumnType("TEXT");

                    b.Property<string>("Hometown")
                        .HasColumnType("TEXT");

                    b.Property<string>("IPPISNo")
                        .HasColumnType("TEXT");

                    b.Property<string>("InstagramID")
                        .HasColumnType("TEXT");

                    b.Property<string>("JambRegNumber")
                        .HasColumnType("TEXT");

                    b.Property<string>("LG")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("LeftThumbFingerBiometrics")
                        .HasColumnType("TEXT");

                    b.Property<string>("LinkedInID")
                        .HasColumnType("TEXT");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("MaritalStatusID")
                        .HasColumnType("TEXT");

                    b.Property<string>("MatricNumber")
                        .HasColumnType("TEXT");

                    b.Property<string>("ModeOfAdmission")
                        .HasColumnType("TEXT");

                    b.Property<string>("NIN")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nationality")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("OtherName")
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<Guid?>("ProgrammeID")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("ReligionID")
                        .HasColumnType("TEXT");

                    b.Property<string>("RightThumbFingerBiometrics")
                        .HasColumnType("TEXT");

                    b.Property<string>("RolesCategory")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("SchoolID")
                        .HasColumnType("TEXT");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT");

                    b.Property<string>("StaffID")
                        .HasColumnType("TEXT");

                    b.Property<string>("StateOfOrigin")
                        .HasColumnType("TEXT");

                    b.Property<string>("Status")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("StudentCategoryID")
                        .HasColumnType("TEXT");

                    b.Property<string>("TwitterID")
                        .HasColumnType("TEXT");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<long>("UserImageData")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserImageURL")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("ZipPostalCode")
                        .HasColumnType("TEXT");

                    b.Property<bool>("isDisabled")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("isIndigenous")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentID");

                    b.HasIndex("EntryLevelID");

                    b.HasIndex("GenderID");

                    b.HasIndex("MaritalStatusID");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.HasIndex("ProgrammeID");

                    b.HasIndex("ReligionID");

                    b.HasIndex("SchoolID");

                    b.HasIndex("StudentCategoryID");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("ACE.Domain.Entities.ControlledEntities.Department", b =>
                {
                    b.HasOne("ACE.Domain.Entities.ControlledEntities.School", "School")
                        .WithMany("Departments")
                        .HasForeignKey("SchoolID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("School");
                });

            modelBuilder.Entity("ACEdatabaseAPI.Data.ApplicationUser", b =>
                {
                    b.HasOne("ACE.Domain.Entities.ControlledEntities.Department", "Departments")
                        .WithMany()
                        .HasForeignKey("DepartmentID");

                    b.HasOne("ACE.Domain.Entities.ControlledEntities.Level", "Level")
                        .WithMany()
                        .HasForeignKey("EntryLevelID");

                    b.HasOne("ACE.Domain.Entities.ControlledEntities.Gender", "Gender")
                        .WithMany()
                        .HasForeignKey("GenderID");

                    b.HasOne("ACE.Domain.Entities.ControlledEntities.MaritalStatus", "MaritalStatus")
                        .WithMany()
                        .HasForeignKey("MaritalStatusID");

                    b.HasOne("ACE.Domain.Entities.ControlledEntities.Programme", "Programme")
                        .WithMany()
                        .HasForeignKey("ProgrammeID");

                    b.HasOne("ACE.Domain.Entities.ControlledEntities.Religion", "Religion")
                        .WithMany()
                        .HasForeignKey("ReligionID");

                    b.HasOne("ACE.Domain.Entities.ControlledEntities.School", "School")
                        .WithMany()
                        .HasForeignKey("SchoolID");

                    b.HasOne("ACE.Domain.Entities.ControlledEntities.StudentCategory", "StudentCategory")
                        .WithMany()
                        .HasForeignKey("StudentCategoryID");

                    b.Navigation("Departments");

                    b.Navigation("Gender");

                    b.Navigation("Level");

                    b.Navigation("MaritalStatus");

                    b.Navigation("Programme");

                    b.Navigation("Religion");

                    b.Navigation("School");

                    b.Navigation("StudentCategory");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("ACEdatabaseAPI.Data.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("ACEdatabaseAPI.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("ACEdatabaseAPI.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("ACEdatabaseAPI.Data.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ACEdatabaseAPI.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("ACEdatabaseAPI.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ACE.Domain.Entities.ControlledEntities.School", b =>
                {
                    b.Navigation("Departments");
                });
#pragma warning restore 612, 618
        }
    }
}
