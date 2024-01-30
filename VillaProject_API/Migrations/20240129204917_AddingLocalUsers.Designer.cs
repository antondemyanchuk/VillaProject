﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VillaProject_API.Data;

#nullable disable

namespace VillaProject_API.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240129204917_AddingLocalUsers")]
    partial class AddingLocalUsers
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("VillaProject_API.Models.LocalUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("LocalUsers");
                });

            modelBuilder.Entity("VillaProject_API.Models.Villa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Amenity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Details")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Occupancy")
                        .HasColumnType("int");

                    b.Property<double>("Rate")
                        .HasColumnType("float");

                    b.Property<int>("Sqft")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Villas");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amenity = "",
                            CreateDate = new DateTime(2024, 1, 29, 22, 49, 17, 462, DateTimeKind.Local).AddTicks(2608),
                            Details = "Great villa",
                            ImageUrl = "https://u.cubeupload.com/anton_demyanchuk/villa1.jpg ",
                            Name = "FirstVilla",
                            Occupancy = 4,
                            Rate = 300.0,
                            Sqft = 500,
                            UpdateDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2,
                            Amenity = "",
                            CreateDate = new DateTime(2024, 1, 29, 22, 49, 17, 462, DateTimeKind.Local).AddTicks(2662),
                            Details = "Great villa",
                            ImageUrl = "https://u.cubeupload.com/anton_demyanchuk/villa2.jpg",
                            Name = "SecondVilla",
                            Occupancy = 3,
                            Rate = 200.0,
                            Sqft = 300,
                            UpdateDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 3,
                            Amenity = "",
                            CreateDate = new DateTime(2024, 1, 29, 22, 49, 17, 462, DateTimeKind.Local).AddTicks(2665),
                            Details = "Great villa",
                            ImageUrl = "https://u.cubeupload.com/anton_demyanchuk/villa5.jpg",
                            Name = "ThirdVilla",
                            Occupancy = 5,
                            Rate = 500.0,
                            Sqft = 700,
                            UpdateDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 4,
                            Amenity = "",
                            CreateDate = new DateTime(2024, 1, 29, 22, 49, 17, 462, DateTimeKind.Local).AddTicks(2667),
                            Details = "Great villa",
                            ImageUrl = "https://u.cubeupload.com/anton_demyanchuk/villa4.jpg",
                            Name = "FourthVilla",
                            Occupancy = 4,
                            Rate = 330.0,
                            Sqft = 550,
                            UpdateDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("VillaProject_API.Models.VillaNumber", b =>
                {
                    b.Property<int>("VillaNo")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SpecialDetails")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("VillaId")
                        .HasColumnType("int");

                    b.HasKey("VillaNo");

                    b.HasIndex("VillaId");

                    b.ToTable("VillaNumbers");

                    b.HasData(
                        new
                        {
                            VillaNo = 101,
                            CreateDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SpecialDetails = "Some details for first room in the first villa",
                            UpdateDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            VillaNo = 102,
                            CreateDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SpecialDetails = "Some details for the second room in the first villa",
                            UpdateDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("VillaProject_API.Models.VillaNumber", b =>
                {
                    b.HasOne("VillaProject_API.Models.Villa", "Villa")
                        .WithMany()
                        .HasForeignKey("VillaId");

                    b.Navigation("Villa");
                });
#pragma warning restore 612, 618
        }
    }
}