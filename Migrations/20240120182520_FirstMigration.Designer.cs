﻿// <auto-generated />
using System;
using CSharpExamBledar.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CSharpExamBledar.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20240120182520_FirstMigration")]
    partial class FirstMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("CSharpExamBledar.Models.Project", b =>
                {
                    b.Property<int>("ProjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime(6)");

                    b.Property<double>("Goal")
                        .HasColumnType("double");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ProjectId");

                    b.HasIndex("UserId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("CSharpExamBledar.Models.Support", b =>
                {
                    b.Property<int>("SupportId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("ProjectId")
                        .HasColumnType("int");

                    b.Property<double>("SupportAmount")
                        .HasColumnType("double");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("SupportId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("UserId");

                    b.ToTable("Supports");
                });

            modelBuilder.Entity("CSharpExamBledar.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CSharpExamBledar.Models.Project", b =>
                {
                    b.HasOne("CSharpExamBledar.Models.User", "Creator")
                        .WithMany("Projects")
                        .HasForeignKey("UserId");

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("CSharpExamBledar.Models.Support", b =>
                {
                    b.HasOne("CSharpExamBledar.Models.Project", "Project")
                        .WithMany("ListSupport")
                        .HasForeignKey("ProjectId");

                    b.HasOne("CSharpExamBledar.Models.User", "Supporter")
                        .WithMany("Supports")
                        .HasForeignKey("UserId");

                    b.Navigation("Project");

                    b.Navigation("Supporter");
                });

            modelBuilder.Entity("CSharpExamBledar.Models.Project", b =>
                {
                    b.Navigation("ListSupport");
                });

            modelBuilder.Entity("CSharpExamBledar.Models.User", b =>
                {
                    b.Navigation("Projects");

                    b.Navigation("Supports");
                });
#pragma warning restore 612, 618
        }
    }
}
