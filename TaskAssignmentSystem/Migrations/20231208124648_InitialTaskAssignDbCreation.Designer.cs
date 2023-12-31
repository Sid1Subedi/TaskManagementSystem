﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaskAssignmentSystem.TaskAssignmentDbContext;

#nullable disable

namespace TaskAssignmentSystem.Migrations
{
    [DbContext(typeof(TaskAssignmentSystemDBContext))]
    [Migration("20231208124648_InitialTaskAssignDbCreation")]
    partial class InitialTaskAssignDbCreation
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TaskAssignmentSystem.Models.Employees.EmployeeModel", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeId"));

                    b.Property<int?>("BodyEmployeeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("EmployeeName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ErrCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ErrMsg")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("EmployeeId");

                    b.HasIndex("BodyEmployeeId");

                    b.ToTable("employees");
                });

            modelBuilder.Entity("TaskAssignmentSystem.Models.Tasks.TaskModel", b =>
                {
                    b.Property<int>("TaskId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TaskId"));

                    b.Property<int?>("BodyTaskId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<string>("ErrCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ErrMsg")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TaskDescription")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("TaskName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("TaskId");

                    b.HasIndex("BodyTaskId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("tasks");
                });

            modelBuilder.Entity("TaskAssignmentSystem.Models.Employees.EmployeeModel", b =>
                {
                    b.HasOne("TaskAssignmentSystem.Models.Employees.EmployeeModel", "Body")
                        .WithMany()
                        .HasForeignKey("BodyEmployeeId");

                    b.Navigation("Body");
                });

            modelBuilder.Entity("TaskAssignmentSystem.Models.Tasks.TaskModel", b =>
                {
                    b.HasOne("TaskAssignmentSystem.Models.Tasks.TaskModel", "Body")
                        .WithMany()
                        .HasForeignKey("BodyTaskId");

                    b.HasOne("TaskAssignmentSystem.Models.Employees.EmployeeModel", "EmployeeModel")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Body");

                    b.Navigation("EmployeeModel");
                });
#pragma warning restore 612, 618
        }
    }
}
