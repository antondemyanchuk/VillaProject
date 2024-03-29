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
    [Migration("20240112204930_SeedVillaNumbersTable")]
    partial class SeedVillaNumbersTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("VillaProject_API.Models.Villa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Amenity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
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
                            CreateDate = new DateTime(2024, 1, 12, 22, 49, 29, 760, DateTimeKind.Local).AddTicks(3729),
                            Details = "Great villa",
                            ImageUrl = "",
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
                            CreateDate = new DateTime(2024, 1, 12, 22, 49, 29, 760, DateTimeKind.Local).AddTicks(3784),
                            Details = "Great villa",
                            ImageUrl = "",
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
                            CreateDate = new DateTime(2024, 1, 12, 22, 49, 29, 760, DateTimeKind.Local).AddTicks(3786),
                            Details = "Great villa",
                            ImageUrl = "",
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
                            CreateDate = new DateTime(2024, 1, 12, 22, 49, 29, 760, DateTimeKind.Local).AddTicks(3788),
                            Details = "Great villa",
                            ImageUrl = "",
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

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SpecialDetails")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("VillaNo");

                    b.ToTable("VillaNumbers");

                    b.HasData(
                        new
                        {
                            VillaNo = 101,
                            CreatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SpecialDetails = "Some details for first room in the first villa",
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            VillaNo = 102,
                            CreatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SpecialDetails = "Some details for the second room in the first villa",
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
