﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetPlatHF.DAL.Data;
using NetPlatHF.DAL.Entities;
using System.Reflection.Emit;

namespace NetPlatHF.DAL.Data;

public class AppDbContext : IdentityDbContext<AppUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        // identity user config
        builder.ApplyConfiguration(new AppUserEntityConfiguration());




        // other config
        builder.Entity<ExerciseTemplate>()
            .Property(et => et.Name)
            .HasMaxLength(30)
        .IsRequired();

        builder.Entity<ExerciseTemplate>()
            .Property(et => et.Muscle)
        .HasMaxLength(30);

        builder.Entity<ExerciseTemplate>()
            .Property(et => et.Description)
            .HasMaxLength(50);

        builder.Entity<ExerciseTemplate>()
            .HasOne(t => t.Owner)
            .WithMany()
            .HasForeignKey(t => t.OwnerId)
            .IsRequired(false);
            //.OnDelete(DeleteBehavior.Cascade);  // cascady cycle hiba


        builder.Entity<GroupTemplate>()
            .Property(gt => gt.Name)
            .HasMaxLength(30)
            .IsRequired();

        builder.Entity<GroupTemplate>()
            .Property(gt => gt.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Entity<GroupTemplate>()
            .HasOne(t => t.Owner)
            .WithMany()
            .HasForeignKey(t => t.OwnerId)
            .IsRequired(false);
            //.OnDelete(DeleteBehavior.Cascade);  // cascady cycle hiba


        // many-to-many templates
        builder.Entity<GroupTemplate>()
            .HasMany(gt => gt.Exercises)
            .WithMany(et => et.Groups)
            .UsingEntity<GroupExerciseTemplate>(
                l => l
                    .HasOne(ge => ge.Exercise)
                    .WithMany(et => et.GroupExercises)
                    .HasForeignKey(ge => ge.ExerciseId)
                    .OnDelete(DeleteBehavior.Cascade),
                r => r
                    .HasOne(ge => ge.Group)
                    .WithMany(gt => gt.GroupExercises)
                    .HasForeignKey(ge => ge.GroupId),
                j =>
                {
                    j.HasKey(ge => ge.Id);
                });

        // Alap gyakorlatok és csoportok feltöltése az adatbázisba
        SeedTemplates(builder);

    }

    




    public DbSet<ExerciseTemplate> ExerciseTemplates => Set<ExerciseTemplate>();
    public DbSet<GroupTemplate> GroupTemplates => Set<GroupTemplate>();




    private void SeedTemplates(ModelBuilder builder)
    {
        builder.Entity<ExerciseTemplate>().HasData(
            new ExerciseTemplate("Pull up") { Muscle = "Back", Id = 1 },
            new ExerciseTemplate("Pull down") { Muscle = "Back", Id = 2 },
            new ExerciseTemplate("Row") { Muscle = "Back", Id = 3 },

            new ExerciseTemplate("Bench press") { Muscle = "Chest", Id = 4 },
            new ExerciseTemplate("Bench press") { Muscle = "Chest", Description = "Incline", Id = 6 },
            new ExerciseTemplate("Chest fly") { Muscle = "Chest", Id = 5 },

            new ExerciseTemplate("Curl") { Muscle = "Biceps", Description = "Incline", Id = 7 },
            new ExerciseTemplate("Curl") { Muscle = "Biceps", Description = "Concentration", Id = 8 },
            new ExerciseTemplate("Curl") { Muscle = "Biceps", Description = "Reverse ez", Id = 9 },

            new ExerciseTemplate("JM press") { Muscle = "Triceps", Id = 10 },
            new ExerciseTemplate("Overhead extension") { Muscle = "Triceps", Description = "Cable", Id = 11 },
            new ExerciseTemplate("Push down") { Muscle = "Triceps", Description = "Cable", Id = 12 },

            new ExerciseTemplate("Overhead press") { Muscle = "Shoulders", Id = 13 },
            new ExerciseTemplate("Lateral raise") { Muscle = "Shoulders", Id = 14 },
            new ExerciseTemplate("Face pull") { Muscle = "Shoulders", Id = 15 },

            new ExerciseTemplate("Leg raise") { Muscle = "Abs", Description = "Lower abs", Id = 16 },
            new ExerciseTemplate("Ab crunch") { Muscle = "Abs", Id = 17 },
            new ExerciseTemplate("Woodchopper") { Muscle = "Abs", Description = "Cable", Id = 18 },

            new ExerciseTemplate("Squat") { Muscle = "Legs", Id = 19 },
            new ExerciseTemplate("Leg extension") { Muscle = "Legs", Id = 20 },
            new ExerciseTemplate("Calf raise") { Muscle = "Legs", Id = 21 }
        );


        builder.Entity<GroupTemplate>().HasData(
            new GroupTemplate("Back and chest") { Id = 1 },
            new GroupTemplate("Arms") { Id = 2 },
            new GroupTemplate("Legs and abs") { Id = 3 }
        );


        builder.Entity<GroupExerciseTemplate>().HasData(
            new GroupExerciseTemplate { Id = 1, GroupId = 1, ExerciseId = 1 },
            new GroupExerciseTemplate { Id = 2, GroupId = 1, ExerciseId = 2 },
            new GroupExerciseTemplate { Id = 3, GroupId = 1, ExerciseId = 4 },
            new GroupExerciseTemplate { Id = 4, GroupId = 1, ExerciseId = 6 },

            new GroupExerciseTemplate { Id = 5, GroupId = 2, ExerciseId = 7 },
            new GroupExerciseTemplate { Id = 6, GroupId = 2, ExerciseId = 10 },
            new GroupExerciseTemplate { Id = 7, GroupId = 2, ExerciseId = 13 },

            new GroupExerciseTemplate { Id = 8, GroupId = 3, ExerciseId = 16 },
            new GroupExerciseTemplate { Id = 9, GroupId = 3, ExerciseId = 17 },
            new GroupExerciseTemplate { Id = 10, GroupId = 3, ExerciseId = 19 },
            new GroupExerciseTemplate { Id = 11, GroupId = 3, ExerciseId = 20 }
        );

    }
}




internal class AppUserEntityConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.Property(x => x.ApiKey).HasMaxLength(32);  // guid (36) without - (32)
    }
}