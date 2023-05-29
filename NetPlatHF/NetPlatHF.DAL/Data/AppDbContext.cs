using Microsoft.AspNetCore.Identity;
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


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new AppUserEntityConfiguration());  // identity user config


        // other config
        modelBuilder.Entity<ExerciseTemplate>()
            .ToTable("exercisetemplates");

        modelBuilder.Entity<ExerciseTemplate>()
            .Property(et => et.Name)
            .HasMaxLength(30)
        .IsRequired();

        modelBuilder.Entity<ExerciseTemplate>()
            .Property(et => et.Muscle)
        .HasMaxLength(30);

        modelBuilder.Entity<ExerciseTemplate>()
            .Property(et => et.Description)
            .HasMaxLength(50);

        modelBuilder.Entity<ExerciseTemplate>()
            .HasOne(t => t.Owner)
            .WithMany()
            .HasForeignKey(t => t.OwnerId)
            .IsRequired(false);
       
        modelBuilder.Entity<GroupTemplate>()
            .ToTable("grouptemplates");

        modelBuilder.Entity<GroupTemplate>()
            .Property(gt => gt.Name)
            .HasMaxLength(30)
            .IsRequired();

        modelBuilder.Entity<GroupTemplate>()
            .Property(gt => gt.Name)
            .HasMaxLength(50)
            .IsRequired();

        modelBuilder.Entity<GroupTemplate>()
            .HasOne(t => t.Owner)
            .WithMany()
            .HasForeignKey(t => t.OwnerId)
            .IsRequired(false);

        // many-to-many templates
        modelBuilder.Entity<GroupExerciseTemplate>()
            .ToTable("templates");

        modelBuilder.Entity<GroupExerciseTemplate>()
            .HasOne(t => t.Owner)
            .WithMany()
            .HasForeignKey(t => t.OwnerId)
            .IsRequired(false);

        modelBuilder.Entity<GroupTemplate>()
            .HasMany(gt => gt.Exercises)
            .WithMany(et => et.Groups)
            .UsingEntity<GroupExerciseTemplate>(
                l => l
                    .HasOne(ge => ge.Exercise)
                    .WithMany(et => et.GroupExercises)
                    .HasForeignKey(ge => ge.ExerciseId)
                    .OnDelete(DeleteBehavior.Cascade),  // ha torlodik egy exercise(template), akkor torlodik a hozza tartozo bejegyzes a templateben(groupexercisetemplate)
                r => r
                    .HasOne(ge => ge.Group)
                    .WithMany(gt => gt.GroupExercises)
                    .HasForeignKey(ge => ge.GroupId)
                    .OnDelete(DeleteBehavior.Cascade),  // ha torlodik egy group(template), akkor torlodik a hozza tartozo bejegyzes a templateben(groupexercisetemplate)
                j =>
                {
                    j.HasKey(ge => ge.Id);
                });

        // Alap gyakorlatok és csoportok feltöltése az adatbázisba
        SeedTemplates(modelBuilder);

    }

    




    public DbSet<ExerciseTemplate> ExerciseTemplates => Set<ExerciseTemplate>();
    public DbSet<GroupTemplate> GroupTemplates => Set<GroupTemplate>();
    public DbSet<GroupExerciseTemplate> Templates => Set<GroupExerciseTemplate>();




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
            new GroupExerciseTemplate { Id = 1, Weight = 10, Repetitions = 12,  GroupId = 1, ExerciseId = 1 },
            new GroupExerciseTemplate { Id = 2, Weight = 5, Repetitions = 10, GroupId = 1, ExerciseId = 2 },
            new GroupExerciseTemplate { Id = 3, Weight = 15, Repetitions = 8, GroupId = 1, ExerciseId = 4 },
            new GroupExerciseTemplate { Id = 4, Weight = 20, Repetitions = 10, GroupId = 1, ExerciseId = 6 },

            new GroupExerciseTemplate { Id = 5, Weight = 30, Repetitions = 16, GroupId = 2, ExerciseId = 7 },
            new GroupExerciseTemplate { Id = 6, Weight = 12, Repetitions = 6, GroupId = 2, ExerciseId = 10 },
            new GroupExerciseTemplate { Id = 7, Weight = 40, Repetitions = 14, GroupId = 2, ExerciseId = 13 },

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