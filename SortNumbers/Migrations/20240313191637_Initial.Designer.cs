﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SortNumbers.Data;

#nullable disable

namespace SortNumbers.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240313191637_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.28")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("SortNumbers.Data.SortedNumber", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("SortResultId")
                        .HasColumnType("int");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SortResultId");

                    b.ToTable("SortedNumbers");
                });

            modelBuilder.Entity("SortNumbers.Data.SortResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("IsAscending")
                        .HasColumnType("bit");

                    b.Property<TimeSpan>("TimeTaken")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.ToTable("SortResults");
                });

            modelBuilder.Entity("SortNumbers.Data.SortedNumber", b =>
                {
                    b.HasOne("SortNumbers.Data.SortResult", "SortResult")
                        .WithMany("SortedNumbers")
                        .HasForeignKey("SortResultId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SortResult");
                });

            modelBuilder.Entity("SortNumbers.Data.SortResult", b =>
                {
                    b.Navigation("SortedNumbers");
                });
#pragma warning restore 612, 618
        }
    }
}
