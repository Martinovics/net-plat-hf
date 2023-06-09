﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetPlatHF.DAL.Data;

#nullable disable

namespace NetPlatHF.DAL.Migrations.IdentityAppDb
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230510124050_ApiKeyColumn")]
    partial class ApiKeyColumn
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("NetPlatHF.DAL.Data.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ApiKey")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("NetPlatHF.DAL.Entities.ExerciseTemplate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Muscle")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("OwnerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ExerciseTemplates");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "",
                            Muscle = "Back",
                            Name = "Pull up",
                            OwnerId = 0
                        },
                        new
                        {
                            Id = 2,
                            Description = "",
                            Muscle = "Back",
                            Name = "Pull down",
                            OwnerId = 0
                        },
                        new
                        {
                            Id = 3,
                            Description = "",
                            Muscle = "Back",
                            Name = "Row",
                            OwnerId = 0
                        },
                        new
                        {
                            Id = 4,
                            Description = "",
                            Muscle = "Chest",
                            Name = "Bench press",
                            OwnerId = 0
                        },
                        new
                        {
                            Id = 6,
                            Description = "Incline",
                            Muscle = "Chest",
                            Name = "Bench press",
                            OwnerId = 0
                        },
                        new
                        {
                            Id = 5,
                            Description = "",
                            Muscle = "Chest",
                            Name = "Chest fly",
                            OwnerId = 0
                        },
                        new
                        {
                            Id = 7,
                            Description = "Incline",
                            Muscle = "Biceps",
                            Name = "Curl",
                            OwnerId = 0
                        },
                        new
                        {
                            Id = 8,
                            Description = "Concentration",
                            Muscle = "Biceps",
                            Name = "Curl",
                            OwnerId = 0
                        },
                        new
                        {
                            Id = 9,
                            Description = "Reverse ez",
                            Muscle = "Biceps",
                            Name = "Curl",
                            OwnerId = 0
                        },
                        new
                        {
                            Id = 10,
                            Description = "",
                            Muscle = "Triceps",
                            Name = "JM press",
                            OwnerId = 0
                        },
                        new
                        {
                            Id = 11,
                            Description = "Cable",
                            Muscle = "Triceps",
                            Name = "Overhead extension",
                            OwnerId = 0
                        },
                        new
                        {
                            Id = 12,
                            Description = "Cable",
                            Muscle = "Triceps",
                            Name = "Push down",
                            OwnerId = 0
                        },
                        new
                        {
                            Id = 13,
                            Description = "",
                            Muscle = "Shoulders",
                            Name = "Overhead press",
                            OwnerId = 0
                        },
                        new
                        {
                            Id = 14,
                            Description = "",
                            Muscle = "Shoulders",
                            Name = "Lateral raise",
                            OwnerId = 0
                        },
                        new
                        {
                            Id = 15,
                            Description = "",
                            Muscle = "Shoulders",
                            Name = "Face pull",
                            OwnerId = 0
                        },
                        new
                        {
                            Id = 16,
                            Description = "Lower abs",
                            Muscle = "Abs",
                            Name = "Leg raise",
                            OwnerId = 0
                        },
                        new
                        {
                            Id = 17,
                            Description = "",
                            Muscle = "Abs",
                            Name = "Ab crunch",
                            OwnerId = 0
                        },
                        new
                        {
                            Id = 18,
                            Description = "Cable",
                            Muscle = "Abs",
                            Name = "Woodchopper",
                            OwnerId = 0
                        },
                        new
                        {
                            Id = 19,
                            Description = "",
                            Muscle = "Legs",
                            Name = "Squat",
                            OwnerId = 0
                        },
                        new
                        {
                            Id = 20,
                            Description = "",
                            Muscle = "Legs",
                            Name = "Leg extension",
                            OwnerId = 0
                        },
                        new
                        {
                            Id = 21,
                            Description = "",
                            Muscle = "Legs",
                            Name = "Calf raise",
                            OwnerId = 0
                        });
                });

            modelBuilder.Entity("NetPlatHF.DAL.Entities.GroupExerciseTemplate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ExerciseId")
                        .HasColumnType("int");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ExerciseId");

                    b.HasIndex("GroupId");

                    b.ToTable("GroupExerciseTemplate");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ExerciseId = 1,
                            GroupId = 1
                        },
                        new
                        {
                            Id = 2,
                            ExerciseId = 2,
                            GroupId = 1
                        },
                        new
                        {
                            Id = 3,
                            ExerciseId = 4,
                            GroupId = 1
                        },
                        new
                        {
                            Id = 4,
                            ExerciseId = 6,
                            GroupId = 1
                        },
                        new
                        {
                            Id = 5,
                            ExerciseId = 7,
                            GroupId = 2
                        },
                        new
                        {
                            Id = 6,
                            ExerciseId = 10,
                            GroupId = 2
                        },
                        new
                        {
                            Id = 7,
                            ExerciseId = 13,
                            GroupId = 2
                        },
                        new
                        {
                            Id = 8,
                            ExerciseId = 16,
                            GroupId = 3
                        },
                        new
                        {
                            Id = 9,
                            ExerciseId = 17,
                            GroupId = 3
                        },
                        new
                        {
                            Id = 10,
                            ExerciseId = 19,
                            GroupId = 3
                        },
                        new
                        {
                            Id = 11,
                            ExerciseId = 20,
                            GroupId = 3
                        });
                });

            modelBuilder.Entity("NetPlatHF.DAL.Entities.GroupTemplate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("OwnerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("GroupTemplates");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "",
                            Name = "Back and chest",
                            OwnerId = 0
                        },
                        new
                        {
                            Id = 2,
                            Description = "",
                            Name = "Arms",
                            OwnerId = 0
                        },
                        new
                        {
                            Id = 3,
                            Description = "",
                            Name = "Legs and abs",
                            OwnerId = 0
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("NetPlatHF.DAL.Data.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("NetPlatHF.DAL.Data.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NetPlatHF.DAL.Data.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("NetPlatHF.DAL.Data.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NetPlatHF.DAL.Entities.GroupExerciseTemplate", b =>
                {
                    b.HasOne("NetPlatHF.DAL.Entities.ExerciseTemplate", "Exercise")
                        .WithMany("GroupExercises")
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NetPlatHF.DAL.Entities.GroupTemplate", "Group")
                        .WithMany("GroupExercises")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exercise");

                    b.Navigation("Group");
                });

            modelBuilder.Entity("NetPlatHF.DAL.Entities.ExerciseTemplate", b =>
                {
                    b.Navigation("GroupExercises");
                });

            modelBuilder.Entity("NetPlatHF.DAL.Entities.GroupTemplate", b =>
                {
                    b.Navigation("GroupExercises");
                });
#pragma warning restore 612, 618
        }
    }
}
