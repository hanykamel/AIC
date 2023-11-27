﻿// <auto-generated />
using System;
using AIC.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AIC.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AIC.Data.Model.Certification", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(128)
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CertificateDate")
                        .HasColumnType("date");

                    b.Property<string>("CertificateName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CertifiedFrom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Certifications");
                });

            modelBuilder.Entity("AIC.Data.Model.Degree", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(128)
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DegreeDate")
                        .HasColumnType("date");

                    b.Property<int>("DegreeLevelId")
                        .HasMaxLength(128)
                        .HasColumnType("int");

                    b.Property<bool>("InProgress")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Specialization")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("University")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DegreeLevelId");

                    b.ToTable("Degrees");
                });

            modelBuilder.Entity("AIC.Data.Model.DegreeLevel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<string>("TitleAr")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("TitleEn")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id")
                        .HasName("PK_DegreeLevel");

                    b.ToTable("DegreeLevels");
                });

            modelBuilder.Entity("AIC.Data.Model.Document", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(128)
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ContentType")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("DisplayTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DocumentUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("DocumentURL");

                    b.Property<int>("DocumenyTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Extention")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("FolderUrl")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("FolderURL");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ProfileVersionId")
                        .HasMaxLength(128)
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("SharepointId")
                        .HasColumnType("int");

                    b.Property<decimal>("Size")
                        .HasColumnType("decimal(18,0)");

                    b.HasKey("Id");

                    b.HasIndex("DocumenyTypeId");

                    b.HasIndex("ProfileVersionId");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("AIC.Data.Model.DocumentType", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("DisplayTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("DocumentTypes");
                });

            modelBuilder.Entity("AIC.Data.Model.Internship", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<string>("NameAr")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameEn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReferenceNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id")
                        .HasName("PK_Internship");

                    b.ToTable("Internships");
                });

            modelBuilder.Entity("AIC.Data.Model.JobTypes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<string>("TitleAr")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("TitleEn")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id")
                        .HasName("PK_JobTypes");

                    b.ToTable("JobTypes");
                });

            modelBuilder.Entity("AIC.Data.Model.JoinUsProfile", b =>
                {
                    b.Property<Guid>("ProfileVersionId")
                        .HasMaxLength(128)
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("AppliedDate")
                        .HasColumnType("date");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(128)
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("datetime2");

                    b.HasKey("ProfileVersionId")
                        .HasName("PK_JoinUsProfile");

                    b.ToTable("JoinUsProfiles");
                });

            modelBuilder.Entity("AIC.Data.Model.Profile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(128)
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("AIC.Data.Model.ProfileCertification", b =>
                {
                    b.Property<Guid>("ProfileVersionId")
                        .HasMaxLength(128)
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CertificationId")
                        .HasMaxLength(128)
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(128)
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("datetime2");

                    b.HasKey("ProfileVersionId", "CertificationId")
                        .HasName("PK_ProfileCertification");

                    b.HasIndex("CertificationId");

                    b.ToTable("ProfileCertifications");
                });

            modelBuilder.Entity("AIC.Data.Model.ProfileDegree", b =>
                {
                    b.Property<Guid>("ProfileVersionId")
                        .HasMaxLength(128)
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DegreeId")
                        .HasMaxLength(128)
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(128)
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("datetime2");

                    b.HasKey("ProfileVersionId", "DegreeId")
                        .HasName("PK_ProfileDegree");

                    b.HasIndex("DegreeId");

                    b.ToTable("ProfileDegrees");
                });

            modelBuilder.Entity("AIC.Data.Model.ProfileInternship", b =>
                {
                    b.Property<Guid>("ProfileId")
                        .HasMaxLength(128)
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("InternshipId")
                        .HasColumnType("int");

                    b.Property<DateTime>("AppliedDate")
                        .HasColumnType("date");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(128)
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ProfileVersionId")
                        .HasMaxLength(128)
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ProfileId", "InternshipId");

                    b.HasIndex("InternshipId");

                    b.HasIndex("ProfileVersionId");

                    b.ToTable("ProfileInternships");
                });

            modelBuilder.Entity("AIC.Data.Model.ProfileTechnicalSkill", b =>
                {
                    b.Property<Guid>("ProfileVersionId")
                        .HasMaxLength(128)
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TechnicalSkillId")
                        .HasMaxLength(128)
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(128)
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("datetime2");

                    b.HasKey("ProfileVersionId", "TechnicalSkillId")
                        .HasName("PK_ProfileTechnicalSkill");

                    b.HasIndex("TechnicalSkillId");

                    b.ToTable("ProfileTechnicalSkills");
                });

            modelBuilder.Entity("AIC.Data.Model.ProfileVacancy", b =>
                {
                    b.Property<int>("VacancyId")
                        .HasColumnType("int");

                    b.Property<Guid>("ProfileId")
                        .HasMaxLength(128)
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("AppliedDate")
                        .HasColumnType("date");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(128)
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ProfileVersionId")
                        .HasMaxLength(128)
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("VacancyId", "ProfileId");

                    b.HasIndex("ProfileId");

                    b.HasIndex("ProfileVersionId");

                    b.ToTable("ProfileVacancies");
                });

            modelBuilder.Entity("AIC.Data.Model.ProfileVersion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(128)
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("AvaliableStartDate")
                        .HasColumnType("date");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("date");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("JoinedIn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("JoinedUsAs")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LinkToPortfolio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MobileNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ProfileId")
                        .HasMaxLength(128)
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ProfileId");

                    b.ToTable("ProfileVersions");
                });

            modelBuilder.Entity("AIC.Data.Model.ProfileWorkExperience", b =>
                {
                    b.Property<Guid>("ProfileVersionId")
                        .HasMaxLength(128)
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("workExperienceId")
                        .HasMaxLength(128)
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(128)
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("datetime2");

                    b.HasKey("ProfileVersionId", "workExperienceId")
                        .HasName("PK_ProfileWorkExperience");

                    b.HasIndex("workExperienceId");

                    b.ToTable("ProfileWorkExperience");
                });

            modelBuilder.Entity("AIC.Data.Model.Subscriber", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(128)
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastNewsLetterDate")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("date");

                    b.Property<DateTime?>("SubscriptionDate")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.ToTable("Subscribers");
                });

            modelBuilder.Entity("AIC.Data.Model.TechnicalSkills", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(128)
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<string>("TechnicalSkill")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("YearsOfExperience")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("TechnicalSkills");
                });

            modelBuilder.Entity("AIC.Data.Model.Vacancy", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<string>("NameAr")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameEn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReferenceNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id")
                        .HasName("PK_Vacancy");

                    b.ToTable("Vacancies");
                });

            modelBuilder.Entity("AIC.Data.Model.WorkExperience", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(128)
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Company")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Company");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("CurrentJob")
                        .HasColumnType("bit");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Job")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("JobTypeId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("JobTypeId");

                    b.ToTable("WorkExperience");
                });

            modelBuilder.Entity("AIC.Data.Model.Degree", b =>
                {
                    b.HasOne("AIC.Data.Model.DegreeLevel", "DegreeLevel")
                        .WithMany("Degrees")
                        .HasForeignKey("DegreeLevelId")
                        .HasConstraintName("FK_Degree_DegreeLevel")
                        .IsRequired();

                    b.Navigation("DegreeLevel");
                });

            modelBuilder.Entity("AIC.Data.Model.Document", b =>
                {
                    b.HasOne("AIC.Data.Model.DocumentType", "DocumenyType")
                        .WithMany("Documents")
                        .HasForeignKey("DocumenyTypeId")
                        .HasConstraintName("FK_Documents_DocumentTypes")
                        .IsRequired();

                    b.HasOne("AIC.Data.Model.ProfileVersion", "ProfileVersion")
                        .WithMany("Documents")
                        .HasForeignKey("ProfileVersionId")
                        .HasConstraintName("FK_Documents_ProfileVersion")
                        .IsRequired();

                    b.Navigation("DocumenyType");

                    b.Navigation("ProfileVersion");
                });

            modelBuilder.Entity("AIC.Data.Model.JoinUsProfile", b =>
                {
                    b.HasOne("AIC.Data.Model.ProfileVersion", "ProfileVersion")
                        .WithOne("JoinUsProfile")
                        .HasForeignKey("AIC.Data.Model.JoinUsProfile", "ProfileVersionId")
                        .HasConstraintName("FK_JoinUsProfile_ProfileVersion")
                        .IsRequired();

                    b.Navigation("ProfileVersion");
                });

            modelBuilder.Entity("AIC.Data.Model.ProfileCertification", b =>
                {
                    b.HasOne("AIC.Data.Model.Certification", "Certification")
                        .WithMany("ProfileCertifications")
                        .HasForeignKey("CertificationId")
                        .HasConstraintName("FK_ProfileCertification_Certification")
                        .IsRequired();

                    b.HasOne("AIC.Data.Model.ProfileVersion", "ProfileVersion")
                        .WithMany("ProfileCertifications")
                        .HasForeignKey("ProfileVersionId")
                        .HasConstraintName("FK_ProfileCertification_ProfileVersion")
                        .IsRequired();

                    b.Navigation("Certification");

                    b.Navigation("ProfileVersion");
                });

            modelBuilder.Entity("AIC.Data.Model.ProfileDegree", b =>
                {
                    b.HasOne("AIC.Data.Model.Degree", "Degree")
                        .WithMany("ProfileDegrees")
                        .HasForeignKey("DegreeId")
                        .HasConstraintName("FK_ProfileDegree_Degree")
                        .IsRequired();

                    b.HasOne("AIC.Data.Model.ProfileVersion", "ProfileVersion")
                        .WithMany("ProfileDegrees")
                        .HasForeignKey("ProfileVersionId")
                        .HasConstraintName("FK_ProfileDegrees_ProfileVersions")
                        .IsRequired();

                    b.Navigation("Degree");

                    b.Navigation("ProfileVersion");
                });

            modelBuilder.Entity("AIC.Data.Model.ProfileInternship", b =>
                {
                    b.HasOne("AIC.Data.Model.Internship", "Internship")
                        .WithMany("ProfileInternships")
                        .HasForeignKey("InternshipId")
                        .HasConstraintName("FK_ProfileInternship_Internship")
                        .IsRequired();

                    b.HasOne("AIC.Data.Model.Profile", "Profile")
                        .WithMany("ProfileInternships")
                        .HasForeignKey("ProfileId")
                        .HasConstraintName("FK_ProfileInternships_Profiles")
                        .IsRequired();

                    b.HasOne("AIC.Data.Model.ProfileVersion", "ProfileVersion")
                        .WithMany("ProfileInternships")
                        .HasForeignKey("ProfileVersionId")
                        .HasConstraintName("FK_ProfileInternship_ProfileVersion")
                        .IsRequired();

                    b.Navigation("Internship");

                    b.Navigation("Profile");

                    b.Navigation("ProfileVersion");
                });

            modelBuilder.Entity("AIC.Data.Model.ProfileTechnicalSkill", b =>
                {
                    b.HasOne("AIC.Data.Model.ProfileVersion", "ProfileVersion")
                        .WithMany("ProfileTechnicalSkills")
                        .HasForeignKey("ProfileVersionId")
                        .HasConstraintName("FK_ProfileTechnicalSkill_ProfileVersion")
                        .IsRequired();

                    b.HasOne("AIC.Data.Model.TechnicalSkills", "TechnicalSkills")
                        .WithMany("ProfileTechnicalSkills")
                        .HasForeignKey("TechnicalSkillId")
                        .HasConstraintName("FK_ProfileTechnicalSkill_TechnicalSkill")
                        .IsRequired();

                    b.Navigation("ProfileVersion");

                    b.Navigation("TechnicalSkills");
                });

            modelBuilder.Entity("AIC.Data.Model.ProfileVacancy", b =>
                {
                    b.HasOne("AIC.Data.Model.Profile", "Profile")
                        .WithMany("ProfileVacancies")
                        .HasForeignKey("ProfileId")
                        .HasConstraintName("FK_ProfileVacancies_Profiles")
                        .IsRequired();

                    b.HasOne("AIC.Data.Model.ProfileVersion", "ProfileVersion")
                        .WithMany("ProfileVacancies")
                        .HasForeignKey("ProfileVersionId")
                        .HasConstraintName("FK_ProfileVacancy_ProfileVersion")
                        .IsRequired();

                    b.HasOne("AIC.Data.Model.Vacancy", "Vacancy")
                        .WithMany("ProfileVacancies")
                        .HasForeignKey("VacancyId")
                        .HasConstraintName("FK_ProfileVacancy_Vacancy")
                        .IsRequired();

                    b.Navigation("Profile");

                    b.Navigation("ProfileVersion");

                    b.Navigation("Vacancy");
                });

            modelBuilder.Entity("AIC.Data.Model.ProfileVersion", b =>
                {
                    b.HasOne("AIC.Data.Model.Profile", "Profile")
                        .WithMany("ProfileVersions")
                        .HasForeignKey("ProfileId")
                        .HasConstraintName("FK_ProfileVersion_Profile")
                        .IsRequired();

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("AIC.Data.Model.ProfileWorkExperience", b =>
                {
                    b.HasOne("AIC.Data.Model.ProfileVersion", "ProfileVersion")
                        .WithMany("ProfileWorkExperience")
                        .HasForeignKey("ProfileVersionId")
                        .HasConstraintName("FK_ProfileWorkExperience_ProfileVersion")
                        .IsRequired();

                    b.HasOne("AIC.Data.Model.WorkExperience", "workExperience")
                        .WithMany("ProfileWorkExperience")
                        .HasForeignKey("workExperienceId")
                        .HasConstraintName("FK_Profile_WorkExperience")
                        .IsRequired();

                    b.Navigation("ProfileVersion");

                    b.Navigation("workExperience");
                });

            modelBuilder.Entity("AIC.Data.Model.WorkExperience", b =>
                {
                    b.HasOne("AIC.Data.Model.JobTypes", "JobTypes")
                        .WithMany("WorkExperiences")
                        .HasForeignKey("JobTypeId")
                        .HasConstraintName("FK_WorkExperience_JobType");

                    b.Navigation("JobTypes");
                });

            modelBuilder.Entity("AIC.Data.Model.Certification", b =>
                {
                    b.Navigation("ProfileCertifications");
                });

            modelBuilder.Entity("AIC.Data.Model.Degree", b =>
                {
                    b.Navigation("ProfileDegrees");
                });

            modelBuilder.Entity("AIC.Data.Model.DegreeLevel", b =>
                {
                    b.Navigation("Degrees");
                });

            modelBuilder.Entity("AIC.Data.Model.DocumentType", b =>
                {
                    b.Navigation("Documents");
                });

            modelBuilder.Entity("AIC.Data.Model.Internship", b =>
                {
                    b.Navigation("ProfileInternships");
                });

            modelBuilder.Entity("AIC.Data.Model.JobTypes", b =>
                {
                    b.Navigation("WorkExperiences");
                });

            modelBuilder.Entity("AIC.Data.Model.Profile", b =>
                {
                    b.Navigation("ProfileInternships");

                    b.Navigation("ProfileVacancies");

                    b.Navigation("ProfileVersions");
                });

            modelBuilder.Entity("AIC.Data.Model.ProfileVersion", b =>
                {
                    b.Navigation("Documents");

                    b.Navigation("JoinUsProfile");

                    b.Navigation("ProfileCertifications");

                    b.Navigation("ProfileDegrees");

                    b.Navigation("ProfileInternships");

                    b.Navigation("ProfileTechnicalSkills");

                    b.Navigation("ProfileVacancies");

                    b.Navigation("ProfileWorkExperience");
                });

            modelBuilder.Entity("AIC.Data.Model.TechnicalSkills", b =>
                {
                    b.Navigation("ProfileTechnicalSkills");
                });

            modelBuilder.Entity("AIC.Data.Model.Vacancy", b =>
                {
                    b.Navigation("ProfileVacancies");
                });

            modelBuilder.Entity("AIC.Data.Model.WorkExperience", b =>
                {
                    b.Navigation("ProfileWorkExperience");
                });
#pragma warning restore 612, 618
        }
    }
}
