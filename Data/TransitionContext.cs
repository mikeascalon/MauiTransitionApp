using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TransitionApp.Models;

namespace TransitionApp.Data
{
    public class TransitionContext:DbContext

    {  public DbSet<User> Users { get; set; }
        public DbSet<UserTask> Tasks { get; set; }
        public DbSet<Permission> Permissions { get; set; }

        public DbSet<TaskTemplate> TaskTemplates { get; set; }

        // Constructor accepting DbContextOptions
        public TransitionContext(DbContextOptions<TransitionContext> options) : base(options) { }

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure UserTask entity
            modelBuilder.Entity<UserTask>(entity =>
            {
                // Define primary key
                entity.HasKey(ut => ut.TaskId);

                // Define relationship: UserTask -> User (Many-to-One)
                entity.HasOne(ut => ut.User)
                      .WithMany(u => u.Tasks)
                      .HasForeignKey(ut => ut.UserId);

                // Ensure Description is included
                entity.Property(ut => ut.Description)
                      .HasMaxLength(1000); // Optional: Add length constraint
            });

            // Configure Permission entity
            modelBuilder.Entity<Permission>(entity =>
            {
                entity.HasKey(p => p.PermissionId); // Explicit primary key

                // Foreign key to User
                entity.HasOne(p => p.User)
                      .WithMany()
                      .HasForeignKey(p => p.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                // Foreign key to Supervisor
                entity.HasOne(p => p.Supervisor)
                      .WithMany()
                      .HasForeignKey(p => p.SupervisorId)
                      .OnDelete(DeleteBehavior.NoAction); // Prevent cascading delete for supervisors
            });


            modelBuilder.Entity<User>().HasData(new User
            {
                UserId = 1,
                Username = "TestUser",
                PasswordHash = "TestPassword",
                Email = "test@example.com",
                Role = "User",
                TaskTemplate = TaskTemplateType.ETS,
                CreatedAt = DateTime.UtcNow
            });


            // Seed data for TaskTemplates
            modelBuilder.Entity<TaskTemplate>().HasData(
                // ETS template tasks
                new TaskTemplate { TaskTemplateId = 1, TemplateType = TaskTemplateType.ETS, Name = "Individualized Initial Counseling", Description = "Soldiers complete their personal self-assessment and begin the development of their Individual Transition Plan to identify their unique needs of the transition process and post-transition goals.", Order = 1, MonthsLeft = 18 },
                new TaskTemplate { TaskTemplateId = 2, TemplateType = TaskTemplateType.ETS, Name = "Pre-Separation counseling", Description = "Pre-Separation counseling covers by-law information to include benefits, entitlements and resources for eligible transitioning Soldiers.", Order = 2, MonthsLeft = 12 },
                new TaskTemplate { TaskTemplateId = 3, TemplateType = TaskTemplateType.ETS, Name = "My Transition", Description = "An overview of the TAP curriculum and the importance of preparing for the transition. It covers topics like personal and family transition concerns, cultural differences, and the importance of communication", Order = 3, MonthsLeft = 12 },
                new TaskTemplate { TaskTemplateId = 4, TemplateType = TaskTemplateType.ETS, Name = "MOS Crosswalk", Description = "The MOC Crosswalk helps you identify your skills, experience, and abilities and begin to translate them into civilian terminology.", Order = 4, MonthsLeft = 12 },
                new TaskTemplate { TaskTemplateId = 5, TemplateType = TaskTemplateType.ETS, Name = "DOL Employment Fundamentals", Description = "This workshop provides an introduction to the essential tools and resources needed to evaluate career options, gain information for civilian employment, and understand the fundamentals of the employment process.", Order = 4, MonthsLeft = 12 },
                new TaskTemplate { TaskTemplateId = 6, TemplateType = TaskTemplateType.ETS, Name = "VA Benifits", Description = "one-day interactive briefing designed to enable transitioning Service members (TSMs) to make informed decisions regarding the use of VA benefits.", Order = 6, MonthsLeft = 12 },
                new TaskTemplate { TaskTemplateId = 7, TemplateType = TaskTemplateType.ETS, Name = "Capstone", Description = " a commander, or a commander's designee, verifies that the transitioning Service member has met the TAP Career Readiness Standards, has a viable Individual Transition Plan (ITP), and is prepared to transition to civilian life. ", Order = 7, MonthsLeft = 12 },




                // Retiring template tasks
                new TaskTemplate { TaskTemplateId = 8, TemplateType = TaskTemplateType.Retiring, Name = "Submit retirement application", Description = "Submit your application for retirement.", Order = 1, MonthsLeft = 24 },
                new TaskTemplate { TaskTemplateId = 9, TemplateType = TaskTemplateType.Retiring, Name = "Attend retirement seminar", Description = "Attend the mandatory retirement seminar.", Order = 2, MonthsLeft = 18 },
                new TaskTemplate { TaskTemplateId = 10, TemplateType = TaskTemplateType.Retiring, Name = "Finalize retirement plans", Description = "Finalize your retirement plans.", Order = 3, MonthsLeft = 12 },
                new TaskTemplate { TaskTemplateId = 11, TemplateType = TaskTemplateType.Retiring, Name = "Schedule final physical exam", Description = "Schedule your final physical exam.", Order = 4, MonthsLeft = 6 },

                // MedBoard template tasks (no timeline)
                new TaskTemplate { TaskTemplateId = 12, TemplateType = TaskTemplateType.MedBoard, Name = "Schedule initial medical evaluation", Description = "Schedule and complete the initial medical evaluation.", Order = 1, MonthsLeft = null },
                new TaskTemplate { TaskTemplateId = 13, TemplateType = TaskTemplateType.MedBoard, Name = "Meet with PEBLO", Description = "Meet with the Physical Evaluation Board Liaison Officer (PEBLO).", Order = 2, MonthsLeft = null }
            );
        }

        
       


    }
}
