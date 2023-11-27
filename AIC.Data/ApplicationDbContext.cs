using System;
using System.Linq;
using System.Reflection;
using AIC.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.ValueGeneration;

#nullable disable

namespace AIC.Data.Model
{
    public partial class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Certification> Certifications { get; set; }
        public virtual DbSet<Degree> Degrees { get; set; }
        public virtual DbSet<DegreeLevel> DegreeLevels { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<DocumentType> DocumentTypes { get; set; }
        public virtual DbSet<WorkExperience> WorkExperience { get; set; }
        public virtual DbSet<Internship> Internships { get; set; }
        public virtual DbSet<JoinUsProfile> JoinUsProfiles { get; set; }
        public virtual DbSet<Profile> Profiles { get; set; }
        public virtual DbSet<ProfileCertification> ProfileCertifications { get; set; }
        public virtual DbSet<ProfileDegree> ProfileDegrees { get; set; }
        public virtual DbSet<ProfileWorkExperience> ProfileWorkExperience { get; set; }
        public virtual DbSet<ProfileInternship> ProfileInternships { get; set; }
        public virtual DbSet<ProfileVacancy> ProfileVacancies { get; set; }
        public virtual DbSet<ProfileVersion> ProfileVersions { get; set; }
        public virtual DbSet<Subscriber> Subscribers { get; set; }
        public virtual DbSet<Vacancy> Vacancies { get; set; }

        public virtual DbSet<JobTypes> JobTypes { get; set; }
        public virtual DbSet<TechnicalSkills> TechnicalSkills { get; set; }
        public virtual DbSet<ProfileTechnicalSkill> ProfileTechnicalSkills { get; set; }

        //        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //        {
        //            if (!optionsBuilder.IsConfigured)
        //            {
        ////#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        //                optionsBuilder.UseSqlServer("Server=10.0.20.43;Database=AIC;User ID=sa; password=P@ssw0rd;MultipleActiveResultSets=True;TrustServerCertificate=False;");
        //            }
        //        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Certification>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(128).ValueGeneratedOnAdd().HasValueGenerator<GuidValueGenerator>(); ; ;

                entity.Property(e => e.CertificateDate).HasColumnType("date");

                entity.Property(e => e.CertificateName)
                    .IsRequired();

                entity.Property(e => e.CertifiedFrom)
                    .IsRequired();
            });

            modelBuilder.Entity<Degree>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(128).ValueGeneratedOnAdd().HasValueGenerator<GuidValueGenerator>(); ; ; ;

                entity.Property(e => e.DegreeDate).HasColumnType("date");

                entity.Property(e => e.DegreeLevelId)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.Specialization)
                    .IsRequired();

                entity.Property(e => e.University)
                    .IsRequired();

                entity.HasOne(d => d.DegreeLevel)
                    .WithMany(p => p.Degrees)
                    .HasForeignKey(d => d.DegreeLevelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Degree_DegreeLevel");
            });

            modelBuilder.Entity<DegreeLevel>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK_DegreeLevel");

                entity.Property(e => e.TitleAr)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TitleEn)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Document>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(128).ValueGeneratedOnAdd().HasValueGenerator<GuidValueGenerator>();

                entity.Property(e => e.ContentType)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.DisplayTitle).IsRequired();

                entity.Property(e => e.DocumentUrl)
                    .IsRequired()
                    .HasColumnName("DocumentURL");

                entity.Property(e => e.Extention)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.FolderUrl).HasColumnName("FolderURL");

                entity.Property(e => e.ProfileVersionId)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.Size).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.DocumenyType)
                    .WithMany(p => p.Documents)
                    .HasForeignKey(d => d.DocumenyTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Documents_DocumentTypes");

                entity.HasOne(d => d.ProfileVersion)
                    .WithMany(p => p.Documents)
                    .HasForeignKey(d => d.ProfileVersionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Documents_ProfileVersion");
            });

            modelBuilder.Entity<DocumentType>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.DisplayTitle).IsRequired();

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<WorkExperience>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(128).ValueGeneratedOnAdd().HasValueGenerator<GuidValueGenerator>();

                entity.Property(e => e.Company)
                    .IsRequired()
                    .HasColumnName("Company");

                entity.Property(e => e.Job)
                    .IsRequired();

                entity.HasOne(d => d.JobTypes)
                   .WithMany(p => p.WorkExperiences)
                   .HasForeignKey(d => d.JobTypeId)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("FK_WorkExperience_JobType");
            });

            modelBuilder.Entity<JobTypes>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK_JobTypes");

                entity.Property(e => e.TitleAr)
                   .IsRequired()
                   .HasMaxLength(50);

                entity.Property(e => e.TitleEn)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Internship>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK_Internship");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.NameAr).IsRequired();

                entity.Property(e => e.NameEn).IsRequired();
            });

            modelBuilder.Entity<JoinUsProfile>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(128).ValueGeneratedOnAdd().HasValueGenerator<GuidValueGenerator>();
                entity.HasKey(e => e.ProfileVersionId)
                    .HasName("PK_JoinUsProfile");

                entity.Property(e => e.ProfileVersionId).HasMaxLength(128);

                entity.Property(e => e.AppliedDate).HasColumnType("date");

                entity.HasOne(d => d.ProfileVersion)
                    .WithOne(p => p.JoinUsProfile)
                    .HasForeignKey<JoinUsProfile>(d => d.ProfileVersionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_JoinUsProfile_ProfileVersion");
            });

            modelBuilder.Entity<Profile>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(128).ValueGeneratedOnAdd().HasValueGenerator<GuidValueGenerator>();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ProfileCertification>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(128).ValueGeneratedOnAdd().HasValueGenerator<GuidValueGenerator>();
                entity.HasKey(e => new { e.ProfileVersionId, e.CertificationId })
                    .HasName("PK_ProfileCertification");

                entity.Property(e => e.ProfileVersionId).HasMaxLength(128);

                entity.Property(e => e.CertificationId).HasMaxLength(128);

                entity.HasOne(d => d.Certification)
                    .WithMany(p => p.ProfileCertifications)
                    .HasForeignKey(d => d.CertificationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProfileCertification_Certification");

                entity.HasOne(d => d.ProfileVersion)
                    .WithMany(p => p.ProfileCertifications)
                    .HasForeignKey(d => d.ProfileVersionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProfileCertification_ProfileVersion");
            });

            modelBuilder.Entity<ProfileDegree>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(128).ValueGeneratedOnAdd().HasValueGenerator<GuidValueGenerator>();
                entity.HasKey(e => new { e.ProfileVersionId, e.DegreeId })
                    .HasName("PK_ProfileDegree");

                entity.Property(e => e.ProfileVersionId).HasMaxLength(128);

                entity.Property(e => e.DegreeId).HasMaxLength(128);

                entity.HasOne(d => d.Degree)
                    .WithMany(p => p.ProfileDegrees)
                    .HasForeignKey(d => d.DegreeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProfileDegree_Degree");

                entity.HasOne(d => d.ProfileVersion)
                    .WithMany(p => p.ProfileDegrees)
                    .HasForeignKey(d => d.ProfileVersionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProfileDegrees_ProfileVersions");
            });

            modelBuilder.Entity<ProfileWorkExperience>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(128).ValueGeneratedOnAdd().HasValueGenerator<GuidValueGenerator>();
                entity.HasKey(e => new { e.ProfileVersionId, e.workExperienceId })
                    .HasName("PK_ProfileWorkExperience");

                entity.Property(e => e.ProfileVersionId).HasMaxLength(128);

                entity.Property(e => e.workExperienceId).HasMaxLength(128);

                entity.HasOne(d => d.workExperience)
                    .WithMany(p => p.ProfileWorkExperience)
                    .HasForeignKey(d => d.workExperienceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Profile_WorkExperience");

                entity.HasOne(d => d.ProfileVersion)
                    .WithMany(p => p.ProfileWorkExperience)
                    .HasForeignKey(d => d.ProfileVersionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProfileWorkExperience_ProfileVersion");
            });

            modelBuilder.Entity<ProfileInternship>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(128).ValueGeneratedOnAdd().HasValueGenerator<GuidValueGenerator>();
                entity.HasKey(e => new { e.ProfileId, e.InternshipId });

                entity.Property(e => e.ProfileId).HasMaxLength(128);

                entity.Property(e => e.AppliedDate).HasColumnType("date");

                entity.Property(e => e.ProfileVersionId)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.HasOne(d => d.Internship)
                    .WithMany(p => p.ProfileInternships)
                    .HasForeignKey(d => d.InternshipId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProfileInternship_Internship");

                entity.HasOne(d => d.Profile)
                    .WithMany(p => p.ProfileInternships)
                    .HasForeignKey(d => d.ProfileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProfileInternships_Profiles");

                entity.HasOne(d => d.ProfileVersion)
                    .WithMany(p => p.ProfileInternships)
                    .HasForeignKey(d => d.ProfileVersionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProfileInternship_ProfileVersion");
            });

            modelBuilder.Entity<ProfileVacancy>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(128).ValueGeneratedOnAdd().HasValueGenerator<GuidValueGenerator>();
                entity.HasKey(e => new { e.VacancyId, e.ProfileId });

                entity.Property(e => e.ProfileId).HasMaxLength(128);

                entity.Property(e => e.AppliedDate).HasColumnType("date");

                entity.Property(e => e.ProfileVersionId)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.HasOne(d => d.Profile)
                    .WithMany(p => p.ProfileVacancies)
                    .HasForeignKey(d => d.ProfileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProfileVacancies_Profiles");

                entity.HasOne(d => d.ProfileVersion)
                    .WithMany(p => p.ProfileVacancies)
                    .HasForeignKey(d => d.ProfileVersionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProfileVacancy_ProfileVersion");

                entity.HasOne(d => d.Vacancy)
                    .WithMany(p => p.ProfileVacancies)
                    .HasForeignKey(d => d.VacancyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProfileVacancy_Vacancy");
            });

            modelBuilder.Entity<ProfileVersion>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(128).ValueGeneratedOnAdd().HasValueGenerator<GuidValueGenerator>();
                entity.Property(e => e.Id).HasMaxLength(128);

                entity.Property(e => e.Address).IsRequired();

                entity.Property(e => e.AvaliableStartDate).HasColumnType("date");

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.FullName).IsRequired();

                entity.Property(e => e.JoinedIn);

                entity.Property(e => e.JoinedUsAs);

                entity.Property(e => e.MobileNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ProfileId)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.HasOne(d => d.Profile)
                    .WithMany(p => p.ProfileVersions)
                    .HasForeignKey(d => d.ProfileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProfileVersion_Profile");
            });

            modelBuilder.Entity<Subscriber>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(128).ValueGeneratedOnAdd().HasValueGenerator<GuidValueGenerator>();

                entity.Property(e => e.Created).HasColumnType("date");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastNewsLetterDate).HasColumnType("datetime");

                entity.Property(e => e.Modified).HasColumnType("date");

                entity.Property(e => e.SubscriptionDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Vacancy>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK_Vacancy");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.NameAr).IsRequired();

                entity.Property(e => e.NameEn).IsRequired();
            });

            modelBuilder.Entity<TechnicalSkills>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(128).ValueGeneratedOnAdd().HasValueGenerator<GuidValueGenerator>();

                entity.Property(e => e.TechnicalSkill)
                    .IsRequired();

                entity.Property(e => e.YearsOfExperience)
                    .IsRequired();
            });

            modelBuilder.Entity<ProfileTechnicalSkill>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(128).ValueGeneratedOnAdd().HasValueGenerator<GuidValueGenerator>();
                entity.HasKey(e => new { e.ProfileVersionId, e.TechnicalSkillId })
                    .HasName("PK_ProfileTechnicalSkill");

                entity.Property(e => e.ProfileVersionId).HasMaxLength(128);

                entity.Property(e => e.TechnicalSkillId).HasMaxLength(128);

                entity.HasOne(d => d.TechnicalSkills)
                    .WithMany(p => p.ProfileTechnicalSkills)
                    .HasForeignKey(d => d.TechnicalSkillId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProfileTechnicalSkill_TechnicalSkill");

                entity.HasOne(d => d.ProfileVersion)
                    .WithMany(p => p.ProfileTechnicalSkills)
                    .HasForeignKey(d => d.ProfileVersionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProfileTechnicalSkill_ProfileVersion");
            });

            OnModelCreatingPartial(modelBuilder);

            //get entities implement IDelete
            var configureDeleteMethod = GetType().GetTypeInfo().DeclaredMethods.Single(m => m.Name == nameof(ConfigureDeleteFilter));
            var argsDelete = new object[] { modelBuilder };
            var deleteEntityTypes = modelBuilder.Model.GetEntityTypes().Where(t => typeof(IDelete).IsAssignableFrom(t.ClrType));
            foreach (var entityType in deleteEntityTypes)
                configureDeleteMethod.MakeGenericMethod(entityType.ClrType).Invoke(this, argsDelete);

        }
        private void ConfigureDeleteFilter<TEntity>(ModelBuilder modelBuilder)
       where TEntity : class, IDelete
        {
            modelBuilder.Entity<TEntity>()
                .HasQueryFilter(t => t.IsDeleted != true);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);


    }
}
