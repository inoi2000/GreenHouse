﻿// <auto-generated />
using System;
using GreenHouse.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GreenHouse.Data.EntityFramework.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240104191142_AddAdmins")]
    partial class AddAdmins
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GreenHouse.Domain.Entities.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("GreenHouse.Domain.Entities.Admin", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("GreenHouse.Domain.Entities.Appartment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Bail")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("CityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfGuests")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfRooms")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfSlippingPlaces")
                        .HasColumnType("int");

                    b.Property<string>("Photos")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("RulesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Square")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("RulesId");

                    b.ToTable("Appartments");
                });

            modelBuilder.Entity("GreenHouse.Domain.Entities.City", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("GreenHouse.Domain.Entities.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AppartmentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateTimeEntry")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateTimeExit")
                        .HasColumnType("datetime2");

                    b.Property<string>("Wishes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("AppartmentId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("GreenHouse.Domain.Entities.RulesList", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsChildrenAllowed")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPartyAllowed")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPetsAllowed")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSmokingAllowed")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Rules");
                });

            modelBuilder.Entity("GreenHouse.Domain.Entities.Appartment", b =>
                {
                    b.HasOne("GreenHouse.Domain.Entities.City", null)
                        .WithMany("Appartments")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GreenHouse.Domain.Entities.RulesList", "Rules")
                        .WithMany()
                        .HasForeignKey("RulesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Rules");
                });

            modelBuilder.Entity("GreenHouse.Domain.Entities.Order", b =>
                {
                    b.HasOne("GreenHouse.Domain.Entities.Account", "Account")
                        .WithMany("Orders")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GreenHouse.Domain.Entities.Appartment", "Appartment")
                        .WithMany("Orders")
                        .HasForeignKey("AppartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Appartment");
                });

            modelBuilder.Entity("GreenHouse.Domain.Entities.Account", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("GreenHouse.Domain.Entities.Appartment", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("GreenHouse.Domain.Entities.City", b =>
                {
                    b.Navigation("Appartments");
                });
#pragma warning restore 612, 618
        }
    }
}
