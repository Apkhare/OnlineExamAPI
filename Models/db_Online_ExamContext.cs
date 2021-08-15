using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace PrjSearchStudent.Models
{
    public partial class db_Online_ExamContext : DbContext
    {
        public db_Online_ExamContext()
        {
        }

        public db_Online_ExamContext(DbContextOptions<db_Online_ExamContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblExamDetail> TblExamDetails { get; set; }
        public virtual DbSet<TblLevel> TblLevels { get; set; }
        public virtual DbSet<TblQuestionDetail> TblQuestionDetails { get; set; }
        public virtual DbSet<TblReport> TblReports { get; set; }
        public virtual DbSet<TblTechnology> TblTechnologies { get; set; }
        public virtual DbSet<TblUserDetail> TblUserDetails { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Server=.;Database=db_Online_Exam;Trusted_Connection=True;");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<TblExamDetail>(entity =>
            {
                entity.HasKey(e => e.FileId)
                    .HasName("PK__tbl_Exam__0FFFC996D9CC1235");

                entity.ToTable("tbl_Exam_Details");

                entity.Property(e => e.FileId).HasColumnName("File_Id");

                entity.Property(e => e.LevelId)
                    .HasMaxLength(30)
                    .HasColumnName("Level_Id");

                entity.Property(e => e.TechnologyId).HasColumnName("Technology_Id");

                entity.HasOne(d => d.Level)
                    .WithMany(p => p.TblExamDetails)
                    .HasForeignKey(d => d.LevelId)
                    .HasConstraintName("FK__tbl_Exam___Level__3F466844");

                entity.HasOne(d => d.Technology)
                    .WithMany(p => p.TblExamDetails)
                    .HasForeignKey(d => d.TechnologyId)
                    .HasConstraintName("FK__tbl_Exam___Techn__3E52440B");
            });

            modelBuilder.Entity<TblLevel>(entity =>
            {
                entity.HasKey(e => e.LevelId)
                    .HasName("PK__tbl_Leve__C4322E003A2AA1D7");

                entity.ToTable("tbl_Levels");

                entity.Property(e => e.LevelId)
                    .HasMaxLength(30)
                    .HasColumnName("Level_Id");

                entity.Property(e => e.LevelNumber).HasColumnName("Level_Number");
            });

            modelBuilder.Entity<TblQuestionDetail>(entity =>
            {
                entity.HasKey(e => e.QuestionId)
                    .HasName("PK__tbl_Ques__B0B2E4E6BD42C1A6");

                entity.ToTable("tbl_Question_Details");

                entity.Property(e => e.QuestionId).HasColumnName("Question_Id");

                entity.Property(e => e.CorrectAnswer)
                    .HasMaxLength(255)
                    .HasColumnName("Correct_Answer");

                entity.Property(e => e.FileId).HasColumnName("File_Id");

                entity.Property(e => e.Option1).HasMaxLength(255);

                entity.Property(e => e.Option2).HasMaxLength(255);

                entity.Property(e => e.Option3).HasMaxLength(255);

                entity.Property(e => e.Option4).HasMaxLength(255);

                entity.Property(e => e.Question).HasMaxLength(255);

                entity.HasOne(d => d.File)
                    .WithMany(p => p.TblQuestionDetails)
                    .HasForeignKey(d => d.FileId)
                    .HasConstraintName("FK__tbl_Quest__File___5535A963");
            });

            modelBuilder.Entity<TblReport>(entity =>
            {
                entity.HasKey(e => e.ReportId)
                    .HasName("PK__tbl_Repo__30FA9DD17F813419");

                entity.ToTable("tbl_Report");

                entity.Property(e => e.ReportId).HasColumnName("Report_Id");

                entity.Property(e => e.LevelId)
                    .HasMaxLength(30)
                    .HasColumnName("Level_Id");

                entity.Property(e => e.MarksObtained).HasColumnName("Marks_Obtained");

                entity.Property(e => e.SubmissionDate)
                    .HasColumnType("date")
                    .HasColumnName("Submission_Date")
                    .HasDefaultValueSql("(CONVERT([date],getdate()))");

                entity.Property(e => e.SubmissionTime)
                    .HasColumnName("Submission_Time")
                    .HasDefaultValueSql("(CONVERT([time],getdate()))");

                entity.Property(e => e.TechnologyId).HasColumnName("Technology_Id");

                entity.Property(e => e.UserId).HasColumnName("User_Id");

                entity.HasOne(d => d.Level)
                    .WithMany(p => p.TblReports)
                    .HasForeignKey(d => d.LevelId)
                    .HasConstraintName("FK__tbl_Repor__Level__5EBF139D");

                entity.HasOne(d => d.Technology)
                    .WithMany(p => p.TblReports)
                    .HasForeignKey(d => d.TechnologyId)
                    .HasConstraintName("FK__tbl_Repor__Techn__5DCAEF64");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TblReports)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__tbl_Repor__User___5CD6CB2B");
            });

            modelBuilder.Entity<TblTechnology>(entity =>
            {
                entity.HasKey(e => e.TechnologyId)
                    .HasName("PK__tbl_Tech__AA289BE6DE946FA5");

                entity.ToTable("tbl_Technology");

                entity.Property(e => e.TechnologyId).HasColumnName("Technology_Id");

                entity.Property(e => e.TechnologyName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("Technology_Name");
            });

            modelBuilder.Entity<TblUserDetail>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__tbl_User__206D9170E29942F5");

                entity.ToTable("tbl_User_Details");

                entity.Property(e => e.UserId).HasColumnName("User_Id");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.DateOfBirth)
                    .HasColumnType("date")
                    .HasColumnName("Date_Of_Birth");

                entity.Property(e => e.Email).HasMaxLength(40);

                entity.Property(e => e.MobileNumber)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("Mobile_Number");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Qualification)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Role).HasDefaultValueSql("((0))");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("User_Name");

                entity.Property(e => e.YearOfPassing).HasColumnName("Year_Of_Passing");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
