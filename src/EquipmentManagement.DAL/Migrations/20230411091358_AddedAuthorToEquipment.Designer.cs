﻿// <auto-generated />
using System;
using EquipmentManagement.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EquipmentManagement.DAL.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230411091358_AddedAuthorToEquipment")]
    partial class AddedAuthorToEquipment
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("EquipmentManagement.Application.Models.JournalRecord", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset?>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("EventName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Journal");
                });

            modelBuilder.Entity("EquipmentManagement.Domain.Models.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Patronymic")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("EquipmentManagement.Domain.Models.Equipment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Article")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("SerialNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("TypeId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("TypeId");

                    b.ToTable("Equipments");
                });

            modelBuilder.Entity("EquipmentManagement.Domain.Models.EquipmentRecord", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("EmployeeId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("EquipmentId")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("Modified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ModifyAuthor")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("StatusId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("EquipmentId");

                    b.HasIndex("StatusId");

                    b.ToTable("EquipmentRecords");
                });

            modelBuilder.Entity("EquipmentManagement.Domain.Models.EquipmentType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("EquipmentTypes");
                });

            modelBuilder.Entity("EquipmentManagement.Domain.Models.Image", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("EquipmentId")
                        .HasColumnType("uuid");

                    b.Property<string>("FullImagePath")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ThumbImagePath")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("EquipmentId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("EquipmentManagement.Domain.Models.Status", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Statuses");
                });

            modelBuilder.Entity("EquipmentManagement.Domain.Models.Equipment", b =>
                {
                    b.HasOne("EquipmentManagement.Domain.Models.EquipmentType", "Type")
                        .WithMany("Equipments")
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Type");
                });

            modelBuilder.Entity("EquipmentManagement.Domain.Models.EquipmentRecord", b =>
                {
                    b.HasOne("EquipmentManagement.Domain.Models.Employee", "Employee")
                        .WithMany("Records")
                        .HasForeignKey("EmployeeId");

                    b.HasOne("EquipmentManagement.Domain.Models.Equipment", "Equipment")
                        .WithMany("Records")
                        .HasForeignKey("EquipmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EquipmentManagement.Domain.Models.Status", "Status")
                        .WithMany("Records")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Equipment");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("EquipmentManagement.Domain.Models.Image", b =>
                {
                    b.HasOne("EquipmentManagement.Domain.Models.Equipment", "Equipment")
                        .WithMany("Images")
                        .HasForeignKey("EquipmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Equipment");
                });

            modelBuilder.Entity("EquipmentManagement.Domain.Models.Employee", b =>
                {
                    b.Navigation("Records");
                });

            modelBuilder.Entity("EquipmentManagement.Domain.Models.Equipment", b =>
                {
                    b.Navigation("Images");

                    b.Navigation("Records");
                });

            modelBuilder.Entity("EquipmentManagement.Domain.Models.EquipmentType", b =>
                {
                    b.Navigation("Equipments");
                });

            modelBuilder.Entity("EquipmentManagement.Domain.Models.Status", b =>
                {
                    b.Navigation("Records");
                });
#pragma warning restore 612, 618
        }
    }
}