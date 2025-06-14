﻿// <auto-generated />
using System;
using BloodDonar.MVC.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BloodDonar.MVC.Migrations
{
    [DbContext(typeof(BloodDonorDbContext))]
    [Migration("20250611055317_BloodDonorDB")]
    partial class BloodDonorDB
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BloodDonar.MVC.Models.BloodDonor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("BloodGroup")
                        .HasColumnType("int");

                    b.Property<string>("ContactNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateOFBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastDonationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfilePicture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Weight")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("BloodDonors");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "123 Main St, Dhaka",
                            BloodGroup = 6,
                            ContactNumber = "01932878112",
                            DateOFBirth = new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "mahbuburrahman@example.com",
                            LastDonationDate = new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Mahbubur Rahman",
                            ProfilePicture = "profiles/mahbubur.jpg",
                            Weight = 70.5f
                        },
                        new
                        {
                            Id = 2,
                            Address = "9/A West Paikpara, Dhaka",
                            BloodGroup = 6,
                            ContactNumber = "01582878199",
                            DateOFBirth = new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "IbrahimKhalil@example.com",
                            LastDonationDate = new DateTime(2023, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Ibrahim Khalil",
                            ProfilePicture = "profiles/ibrahim.jpg",
                            Weight = 70.5f
                        });
                });

            modelBuilder.Entity("BloodDonar.MVC.Models.Donation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BloodDonationId")
                        .HasColumnType("int");

                    b.Property<int?>("BloodDonorId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DonationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("HospitalName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("BloodDonorId");

                    b.ToTable("Donations");
                });

            modelBuilder.Entity("BloodDonar.MVC.Models.Donation", b =>
                {
                    b.HasOne("BloodDonar.MVC.Models.BloodDonor", null)
                        .WithMany("Donation")
                        .HasForeignKey("BloodDonorId");
                });

            modelBuilder.Entity("BloodDonar.MVC.Models.BloodDonor", b =>
                {
                    b.Navigation("Donation");
                });
#pragma warning restore 612, 618
        }
    }
}
