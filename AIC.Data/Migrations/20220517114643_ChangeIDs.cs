using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AIC.Data.Migrations
{
    public partial class ChangeIDs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Certifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 128, nullable: false),
                    CertificateName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CertifiedFrom = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CertificateDate = table.Column<DateTime>(type: "date", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DegreeLevels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleEn = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TitleAr = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DegreeLevel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisplayTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Internships",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Internship", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleEn = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TitleAr = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 128, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subscribers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 128, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastNewsLetterDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    SubscriptionDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Created = table.Column<DateTime>(type: "date", nullable: false),
                    Modified = table.Column<DateTime>(type: "date", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscribers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TechnicalSkills",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 128, nullable: false),
                    TechnicalSkill = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    YearsOfExperience = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnicalSkills", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vacancies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacancy", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Degrees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 128, nullable: false),
                    DegreeLevelId = table.Column<int>(type: "int", maxLength: 128, nullable: false),
                    DegreeDate = table.Column<DateTime>(type: "date", nullable: false),
                    University = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Specialization = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Degrees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Degree_DegreeLevel",
                        column: x => x.DegreeLevelId,
                        principalTable: "DegreeLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkExperience",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 128, nullable: false),
                    Job = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    JobTypeId = table.Column<int>(type: "int", nullable: false),
                    Company = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CurrentJob = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkExperience", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkExperience_JobType",
                        column: x => x.JobTypeId,
                        principalTable: "JobTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProfileVersions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 128, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MobileNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "date", nullable: false),
                    AvaliableStartDate = table.Column<DateTime>(type: "date", nullable: false),
                    LinkToPortfolio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JoinedUsAs = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    JoinedIn = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ProfileId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 128, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileVersions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfileVersion_Profile",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 128, nullable: false),
                    ProfileVersionId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 128, nullable: false),
                    DisplayTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Extention = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Size = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    DocumentURL = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: false),
                    SharepointId = table.Column<int>(type: "int", nullable: true),
                    FolderURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumenyTypeId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documents_DocumentTypes",
                        column: x => x.DocumenyTypeId,
                        principalTable: "DocumentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Documents_ProfileVersion",
                        column: x => x.ProfileVersionId,
                        principalTable: "ProfileVersions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JoinUsProfiles",
                columns: table => new
                {
                    ProfileVersionId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 128, nullable: false),
                    AppliedDate = table.Column<DateTime>(type: "date", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 128, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JoinUsProfile", x => x.ProfileVersionId);
                    table.ForeignKey(
                        name: "FK_JoinUsProfile_ProfileVersion",
                        column: x => x.ProfileVersionId,
                        principalTable: "ProfileVersions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProfileCertifications",
                columns: table => new
                {
                    ProfileVersionId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 128, nullable: false),
                    CertificationId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 128, nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 128, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileCertification", x => new { x.ProfileVersionId, x.CertificationId });
                    table.ForeignKey(
                        name: "FK_ProfileCertification_Certification",
                        column: x => x.CertificationId,
                        principalTable: "Certifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProfileCertification_ProfileVersion",
                        column: x => x.ProfileVersionId,
                        principalTable: "ProfileVersions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProfileDegrees",
                columns: table => new
                {
                    ProfileVersionId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 128, nullable: false),
                    DegreeId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 128, nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 128, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileDegree", x => new { x.ProfileVersionId, x.DegreeId });
                    table.ForeignKey(
                        name: "FK_ProfileDegree_Degree",
                        column: x => x.DegreeId,
                        principalTable: "Degrees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProfileDegrees_ProfileVersions",
                        column: x => x.ProfileVersionId,
                        principalTable: "ProfileVersions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProfileInternships",
                columns: table => new
                {
                    ProfileId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 128, nullable: false),
                    InternshipId = table.Column<int>(type: "int", nullable: false),
                    ProfileVersionId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 128, nullable: false),
                    AppliedDate = table.Column<DateTime>(type: "date", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 128, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileInternships", x => new { x.ProfileId, x.InternshipId });
                    table.ForeignKey(
                        name: "FK_ProfileInternship_Internship",
                        column: x => x.InternshipId,
                        principalTable: "Internships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProfileInternship_ProfileVersion",
                        column: x => x.ProfileVersionId,
                        principalTable: "ProfileVersions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProfileInternships_Profiles",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProfileTechnicalSkills",
                columns: table => new
                {
                    ProfileVersionId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 128, nullable: false),
                    TechnicalSkillId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 128, nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 128, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileTechnicalSkill", x => new { x.ProfileVersionId, x.TechnicalSkillId });
                    table.ForeignKey(
                        name: "FK_ProfileTechnicalSkill_ProfileVersion",
                        column: x => x.ProfileVersionId,
                        principalTable: "ProfileVersions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProfileTechnicalSkill_TechnicalSkill",
                        column: x => x.TechnicalSkillId,
                        principalTable: "TechnicalSkills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProfileVacancies",
                columns: table => new
                {
                    VacancyId = table.Column<int>(type: "int", nullable: false),
                    ProfileId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 128, nullable: false),
                    ProfileVersionId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 128, nullable: false),
                    AppliedDate = table.Column<DateTime>(type: "date", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 128, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileVacancies", x => new { x.VacancyId, x.ProfileId });
                    table.ForeignKey(
                        name: "FK_ProfileVacancies_Profiles",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProfileVacancy_ProfileVersion",
                        column: x => x.ProfileVersionId,
                        principalTable: "ProfileVersions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProfileVacancy_Vacancy",
                        column: x => x.VacancyId,
                        principalTable: "Vacancies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProfileWorkExperience",
                columns: table => new
                {
                    ProfileVersionId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 128, nullable: false),
                    workExperienceId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 128, nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 128, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileWorkExperience", x => new { x.ProfileVersionId, x.workExperienceId });
                    table.ForeignKey(
                        name: "FK_Profile_WorkExperience",
                        column: x => x.workExperienceId,
                        principalTable: "WorkExperience",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProfileWorkExperience_ProfileVersion",
                        column: x => x.ProfileVersionId,
                        principalTable: "ProfileVersions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Degrees_DegreeLevelId",
                table: "Degrees",
                column: "DegreeLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_DocumenyTypeId",
                table: "Documents",
                column: "DocumenyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_ProfileVersionId",
                table: "Documents",
                column: "ProfileVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileCertifications_CertificationId",
                table: "ProfileCertifications",
                column: "CertificationId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileDegrees_DegreeId",
                table: "ProfileDegrees",
                column: "DegreeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileInternships_InternshipId",
                table: "ProfileInternships",
                column: "InternshipId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileInternships_ProfileVersionId",
                table: "ProfileInternships",
                column: "ProfileVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileTechnicalSkills_TechnicalSkillId",
                table: "ProfileTechnicalSkills",
                column: "TechnicalSkillId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileVacancies_ProfileId",
                table: "ProfileVacancies",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileVacancies_ProfileVersionId",
                table: "ProfileVacancies",
                column: "ProfileVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileVersions_ProfileId",
                table: "ProfileVersions",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileWorkExperience_workExperienceId",
                table: "ProfileWorkExperience",
                column: "workExperienceId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkExperience_JobTypeId",
                table: "WorkExperience",
                column: "JobTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "JoinUsProfiles");

            migrationBuilder.DropTable(
                name: "ProfileCertifications");

            migrationBuilder.DropTable(
                name: "ProfileDegrees");

            migrationBuilder.DropTable(
                name: "ProfileInternships");

            migrationBuilder.DropTable(
                name: "ProfileTechnicalSkills");

            migrationBuilder.DropTable(
                name: "ProfileVacancies");

            migrationBuilder.DropTable(
                name: "ProfileWorkExperience");

            migrationBuilder.DropTable(
                name: "Subscribers");

            migrationBuilder.DropTable(
                name: "DocumentTypes");

            migrationBuilder.DropTable(
                name: "Certifications");

            migrationBuilder.DropTable(
                name: "Degrees");

            migrationBuilder.DropTable(
                name: "Internships");

            migrationBuilder.DropTable(
                name: "TechnicalSkills");

            migrationBuilder.DropTable(
                name: "Vacancies");

            migrationBuilder.DropTable(
                name: "WorkExperience");

            migrationBuilder.DropTable(
                name: "ProfileVersions");

            migrationBuilder.DropTable(
                name: "DegreeLevels");

            migrationBuilder.DropTable(
                name: "JobTypes");

            migrationBuilder.DropTable(
                name: "Profiles");
        }
    }
}
