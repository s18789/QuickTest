﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QuickTest.Infrastructure.Data;

#nullable disable

namespace QuickTest.Infrastructure.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230820214903_nullable_DescAndTitle")]
    partial class nullable_DescAndTitle
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("QuickTest.Core.Entities.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("BuildingNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PropertyNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SchoolId")
                        .HasColumnType("int");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("SchoolId")
                        .IsUnique();

                    b.ToTable("Address");
                });

            modelBuilder.Entity("QuickTest.Core.Entities.Exam", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("AvailableFrom")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("AvailableTo")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaxPoints")
                        .HasColumnType("int");

                    b.Property<int>("TeacherId")
                        .HasColumnType("int");

                    b.Property<int>("Time")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("TeacherId");

                    b.ToTable("Exams");
                });

            modelBuilder.Entity("QuickTest.Core.Entities.ExamResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ExamId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("FinishExamTime")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("MailSend")
                        .HasColumnType("bit");

                    b.Property<double?>("Score")
                        .HasColumnType("float");

                    b.Property<DateTime?>("StartExamTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ExamId");

                    b.HasIndex("StudentId");

                    b.ToTable("ExamResults");
                });

            modelBuilder.Entity("QuickTest.Core.Entities.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("SchoolId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SchoolId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("QuickTest.Core.Entities.GroupTeacher", b =>
                {
                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<int>("TeacherId")
                        .HasColumnType("int");

                    b.HasKey("GroupId", "TeacherId");

                    b.HasIndex("TeacherId");

                    b.ToTable("GroupTeacher");
                });

            modelBuilder.Entity("QuickTest.Core.Entities.PredefinedAnswer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)");

                    b.Property<bool>("IsCorrect")
                        .HasColumnType("bit");

                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("PredefinedAnswers");
                });

            modelBuilder.Entity("QuickTest.Core.Entities.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ExamId")
                        .HasColumnType("int");

                    b.Property<double>("Points")
                        .HasColumnType("float");

                    b.Property<string>("QuestionContent")
                        .IsRequired()
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ExamId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("QuickTest.Core.Entities.School", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Schools");
                });

            modelBuilder.Entity("QuickTest.Core.Entities.SelectedStudentAnswer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool?>("IsSelected")
                        .HasColumnType("bit");

                    b.Property<int?>("PredefinedAnswerId")
                        .HasColumnType("int");

                    b.Property<int?>("StudentAnswerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PredefinedAnswerId");

                    b.HasIndex("StudentAnswerId");

                    b.ToTable("SelectedStudentAnswers");
                });

            modelBuilder.Entity("QuickTest.Core.Entities.StudentAnswer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ExamResultId")
                        .HasColumnType("int");

                    b.Property<double>("Points")
                        .HasColumnType("float");

                    b.Property<int?>("QuestionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ExamResultId");

                    b.HasIndex("QuestionId");

                    b.ToTable("StudentAnswers");
                });

            modelBuilder.Entity("QuickTest.Core.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserRoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserRoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("QuickTest.Core.Entities.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("QuickTest.Core.Entities.Admin", b =>
                {
                    b.HasBaseType("QuickTest.Core.Entities.User");

                    b.Property<int?>("SchoolId")
                        .HasColumnType("int");

                    b.HasIndex("SchoolId");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("QuickTest.Core.Entities.Student", b =>
                {
                    b.HasBaseType("QuickTest.Core.Entities.User");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<string>("Index")
                        .HasColumnType("nvarchar(max)");

                    b.HasIndex("GroupId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("QuickTest.Core.Entities.Teacher", b =>
                {
                    b.HasBaseType("QuickTest.Core.Entities.User");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("QuickTest.Core.Entities.Address", b =>
                {
                    b.HasOne("QuickTest.Core.Entities.School", "School")
                        .WithOne("Address")
                        .HasForeignKey("QuickTest.Core.Entities.Address", "SchoolId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("School");
                });

            modelBuilder.Entity("QuickTest.Core.Entities.Exam", b =>
                {
                    b.HasOne("QuickTest.Core.Entities.Teacher", "Teacher")
                        .WithMany("Exams")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("QuickTest.Core.Entities.ExamResult", b =>
                {
                    b.HasOne("QuickTest.Core.Entities.Exam", "Exam")
                        .WithMany("ExamResults")
                        .HasForeignKey("ExamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("QuickTest.Core.Entities.Student", "Student")
                        .WithMany("ExamResults")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exam");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("QuickTest.Core.Entities.Group", b =>
                {
                    b.HasOne("QuickTest.Core.Entities.School", "School")
                        .WithMany("Groups")
                        .HasForeignKey("SchoolId");

                    b.Navigation("School");
                });

            modelBuilder.Entity("QuickTest.Core.Entities.GroupTeacher", b =>
                {
                    b.HasOne("QuickTest.Core.Entities.Group", "Group")
                        .WithMany("GroupTeachers")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("QuickTest.Core.Entities.Teacher", "Teacher")
                        .WithMany("GroupTeachers")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("QuickTest.Core.Entities.PredefinedAnswer", b =>
                {
                    b.HasOne("QuickTest.Core.Entities.Question", "Question")
                        .WithMany("PredefinedAnswers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("QuickTest.Core.Entities.Question", b =>
                {
                    b.HasOne("QuickTest.Core.Entities.Exam", "Exam")
                        .WithMany("Questions")
                        .HasForeignKey("ExamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exam");
                });

            modelBuilder.Entity("QuickTest.Core.Entities.SelectedStudentAnswer", b =>
                {
                    b.HasOne("QuickTest.Core.Entities.PredefinedAnswer", "PredefinedAnswer")
                        .WithMany("SelectedStudentAnswers")
                        .HasForeignKey("PredefinedAnswerId");

                    b.HasOne("QuickTest.Core.Entities.StudentAnswer", "StudentAnswer")
                        .WithMany("SelectedStudentAnswers")
                        .HasForeignKey("StudentAnswerId");

                    b.Navigation("PredefinedAnswer");

                    b.Navigation("StudentAnswer");
                });

            modelBuilder.Entity("QuickTest.Core.Entities.StudentAnswer", b =>
                {
                    b.HasOne("QuickTest.Core.Entities.ExamResult", "ExamResult")
                        .WithMany("StudentAnswers")
                        .HasForeignKey("ExamResultId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("QuickTest.Core.Entities.Question", "Question")
                        .WithMany("StudentAnswers")
                        .HasForeignKey("QuestionId");

                    b.Navigation("ExamResult");

                    b.Navigation("Question");
                });

            modelBuilder.Entity("QuickTest.Core.Entities.User", b =>
                {
                    b.HasOne("QuickTest.Core.Entities.UserRole", "UserRole")
                        .WithMany("Users")
                        .HasForeignKey("UserRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserRole");
                });

            modelBuilder.Entity("QuickTest.Core.Entities.Admin", b =>
                {
                    b.HasOne("QuickTest.Core.Entities.User", null)
                        .WithOne()
                        .HasForeignKey("QuickTest.Core.Entities.Admin", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("QuickTest.Core.Entities.School", "AdministeredSchool")
                        .WithMany()
                        .HasForeignKey("SchoolId");

                    b.Navigation("AdministeredSchool");
                });

            modelBuilder.Entity("QuickTest.Core.Entities.Student", b =>
                {
                    b.HasOne("QuickTest.Core.Entities.Group", "Group")
                        .WithMany("Students")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("QuickTest.Core.Entities.User", null)
                        .WithOne()
                        .HasForeignKey("QuickTest.Core.Entities.Student", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Group");
                });

            modelBuilder.Entity("QuickTest.Core.Entities.Teacher", b =>
                {
                    b.HasOne("QuickTest.Core.Entities.User", null)
                        .WithOne()
                        .HasForeignKey("QuickTest.Core.Entities.Teacher", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("QuickTest.Core.Entities.Exam", b =>
                {
                    b.Navigation("ExamResults");

                    b.Navigation("Questions");
                });

            modelBuilder.Entity("QuickTest.Core.Entities.ExamResult", b =>
                {
                    b.Navigation("StudentAnswers");
                });

            modelBuilder.Entity("QuickTest.Core.Entities.Group", b =>
                {
                    b.Navigation("GroupTeachers");

                    b.Navigation("Students");
                });

            modelBuilder.Entity("QuickTest.Core.Entities.PredefinedAnswer", b =>
                {
                    b.Navigation("SelectedStudentAnswers");
                });

            modelBuilder.Entity("QuickTest.Core.Entities.Question", b =>
                {
                    b.Navigation("PredefinedAnswers");

                    b.Navigation("StudentAnswers");
                });

            modelBuilder.Entity("QuickTest.Core.Entities.School", b =>
                {
                    b.Navigation("Address")
                        .IsRequired();

                    b.Navigation("Groups");
                });

            modelBuilder.Entity("QuickTest.Core.Entities.StudentAnswer", b =>
                {
                    b.Navigation("SelectedStudentAnswers");
                });

            modelBuilder.Entity("QuickTest.Core.Entities.UserRole", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("QuickTest.Core.Entities.Student", b =>
                {
                    b.Navigation("ExamResults");
                });

            modelBuilder.Entity("QuickTest.Core.Entities.Teacher", b =>
                {
                    b.Navigation("Exams");

                    b.Navigation("GroupTeachers");
                });
#pragma warning restore 612, 618
        }
    }
}
