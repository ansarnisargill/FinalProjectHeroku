﻿// <auto-generated />
using FinalProjectHeroku.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace FinalProject.Migrations
{
    [DbContext(typeof(FinalContext))]
    [Migration("20180323184950_fIRSTmIGRATION")]
    partial class fIRSTmIGRATION
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FinalProjectHeroku.Models.Appointment", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("PsychologistID");

                    b.Property<int>("Rating");

                    b.Property<int?>("ResultID");

                    b.Property<int?>("SessionID");

                    b.Property<bool>("Status");

                    b.Property<int?>("UserID");

                    b.HasKey("ID");

                    b.HasIndex("PsychologistID");

                    b.HasIndex("ResultID");

                    b.HasIndex("SessionID");

                    b.HasIndex("UserID");

                    b.ToTable("Appointment");
                });

            modelBuilder.Entity("FinalProjectHeroku.Models.History", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AppointmentID");

                    b.Property<string>("Description");

                    b.Property<int>("PsychologitsID");

                    b.Property<int>("UserID");

                    b.Property<int?>("psychologistID");

                    b.HasKey("ID");

                    b.HasIndex("AppointmentID");

                    b.HasIndex("UserID");

                    b.HasIndex("psychologistID");

                    b.ToTable("Histories");
                });

            modelBuilder.Entity("FinalProjectHeroku.Models.Psychologist", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CNIC");

                    b.Property<string>("Education");

                    b.Property<int>("Experience");

                    b.Property<DateTime>("JoinDate");

                    b.Property<int>("MobileNo");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.Property<int>("PsyTypeID");

                    b.Property<int>("RegistrationNo");

                    b.Property<string>("Sex");

                    b.Property<string>("Username");

                    b.HasKey("ID");

                    b.HasIndex("PsyTypeID");

                    b.ToTable("Psychologists");
                });

            modelBuilder.Entity("FinalProjectHeroku.Models.PsyType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int>("Rating");

                    b.HasKey("ID");

                    b.ToTable("PsyTypes");
                });

            modelBuilder.Entity("FinalProjectHeroku.Models.Request", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("PsychologitsID");

                    b.Property<int?>("ResultID");

                    b.Property<int?>("SessionID");

                    b.Property<int?>("UserID");

                    b.Property<int?>("psychologistID");

                    b.HasKey("ID");

                    b.HasIndex("ResultID");

                    b.HasIndex("SessionID");

                    b.HasIndex("UserID");

                    b.HasIndex("psychologistID");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("FinalProjectHeroku.Models.Result", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BirthOrder");

                    b.Property<string>("Education");

                    b.Property<string>("HistoryOfIllness");

                    b.Property<string>("Religion");

                    b.Property<bool>("SexuallyActive");

                    b.Property<int>("UserID");

                    b.HasKey("ID");

                    b.HasIndex("UserID");

                    b.ToTable("Results");
                });

            modelBuilder.Entity("FinalProjectHeroku.Models.Session", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("EndingTime");

                    b.Property<int>("PsychologistID");

                    b.Property<DateTime>("SessionDate");

                    b.Property<DateTime>("StartingTime");

                    b.Property<bool>("Status");

                    b.HasKey("ID");

                    b.HasIndex("PsychologistID");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("FinalProjectHeroku.Models.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("City");

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<string>("Email");

                    b.Property<bool>("IsMarried");

                    b.Property<DateTime>("JoinDate");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.Property<bool>("Sex");

                    b.HasKey("ID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FinalProjectHeroku.Models.Appointment", b =>
                {
                    b.HasOne("FinalProjectHeroku.Models.Psychologist", "psychologist")
                        .WithMany()
                        .HasForeignKey("PsychologistID");

                    b.HasOne("FinalProjectHeroku.Models.Result", "result")
                        .WithMany()
                        .HasForeignKey("ResultID");

                    b.HasOne("FinalProjectHeroku.Models.Session", "session")
                        .WithMany()
                        .HasForeignKey("SessionID");

                    b.HasOne("FinalProjectHeroku.Models.User", "user")
                        .WithMany()
                        .HasForeignKey("UserID");
                });

            modelBuilder.Entity("FinalProjectHeroku.Models.History", b =>
                {
                    b.HasOne("FinalProjectHeroku.Models.Appointment", "appointment")
                        .WithMany()
                        .HasForeignKey("AppointmentID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FinalProjectHeroku.Models.User", "user")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FinalProjectHeroku.Models.Psychologist", "psychologist")
                        .WithMany()
                        .HasForeignKey("psychologistID");
                });

            modelBuilder.Entity("FinalProjectHeroku.Models.Psychologist", b =>
                {
                    b.HasOne("FinalProjectHeroku.Models.PsyType", "psyType")
                        .WithMany()
                        .HasForeignKey("PsyTypeID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FinalProjectHeroku.Models.Request", b =>
                {
                    b.HasOne("FinalProjectHeroku.Models.Result", "result")
                        .WithMany()
                        .HasForeignKey("ResultID");

                    b.HasOne("FinalProjectHeroku.Models.Session", "session")
                        .WithMany()
                        .HasForeignKey("SessionID");

                    b.HasOne("FinalProjectHeroku.Models.User", "user")
                        .WithMany()
                        .HasForeignKey("UserID");

                    b.HasOne("FinalProjectHeroku.Models.Psychologist", "psychologist")
                        .WithMany()
                        .HasForeignKey("psychologistID");
                });

            modelBuilder.Entity("FinalProjectHeroku.Models.Result", b =>
                {
                    b.HasOne("FinalProjectHeroku.Models.User", "user")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FinalProjectHeroku.Models.Session", b =>
                {
                    b.HasOne("FinalProjectHeroku.Models.Psychologist", "psychologist")
                        .WithMany()
                        .HasForeignKey("PsychologistID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
