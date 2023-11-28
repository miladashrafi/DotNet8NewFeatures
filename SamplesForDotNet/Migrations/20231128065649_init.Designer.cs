﻿// <auto-generated />
using System;
using DotNet8NewFeatures.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.SqlServer.Types;

#nullable disable

namespace DotNet8NewFeatures.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20231128065649_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DotNet8NewFeatures.Models.TreeNode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.Property<SqlHierarchyId>("Path")
                        .HasColumnType("hierarchyid");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Departments");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Root",
                            Path = Microsoft.SqlServer.Types.SqlHierarchyId.Parse("/")
                        },
                        new
                        {
                            Id = 2,
                            Name = "Technology",
                            ParentId = 1,
                            Path = Microsoft.SqlServer.Types.SqlHierarchyId.Parse("/1/")
                        },
                        new
                        {
                            Id = 3,
                            Name = "HR",
                            ParentId = 1,
                            Path = Microsoft.SqlServer.Types.SqlHierarchyId.Parse("/2/")
                        },
                        new
                        {
                            Id = 4,
                            Name = "Domestic-Flight",
                            ParentId = 2,
                            Path = Microsoft.SqlServer.Types.SqlHierarchyId.Parse("/1/2/")
                        });
                });

            modelBuilder.Entity("DotNet8NewFeatures.Models.TreeNode", b =>
                {
                    b.HasOne("DotNet8NewFeatures.Models.TreeNode", "Parent")
                        .WithMany("SubCategories")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("DotNet8NewFeatures.Models.TreeNode", b =>
                {
                    b.Navigation("SubCategories");
                });
#pragma warning restore 612, 618
        }
    }
}
