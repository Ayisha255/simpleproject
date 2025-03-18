using Microsoft.EntityFrameworkCore;
using GymManagementSystem.Models;

namespace GymManagementSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }

       
        public DbSet<GymClass> GymClasses { get; set; }
        public DbSet<ClassEnrollment> ClassEnrollments { get; set; }

        public DbSet<MembershipPlan> MembershipPlans { get; set; }


        public DbSet<UserMembershipPlan> UserMembershipPlans { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MembershipPlan>()
               .Property(mp => mp.Cost)
               .HasColumnType("decimal(18,2)");


            modelBuilder.Entity<UserMembershipPlan>()
                .HasOne(up => up.User)
                .WithMany(u => u.UserMembershipPlans)
                .HasForeignKey(up => up.UserId);

            modelBuilder.Entity<UserMembershipPlan>()
                .HasOne(up => up.MembershipPlan)
                .WithMany(mp => mp.UserMembershipPlans)
                .HasForeignKey(up => up.MembershipPlanId);


            // Define foreign key relationships
            modelBuilder.Entity<ClassEnrollment>()
                .HasOne(ce => ce.User)
                .WithMany()
                .HasForeignKey(ce => ce.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Handle delete behavior if needed

            modelBuilder.Entity<ClassEnrollment>()
                .HasOne(ce => ce.GymClass)
                .WithMany()
                .HasForeignKey(ce => ce.ClassId)
                .OnDelete(DeleteBehavior.Cascade); // Handle delete behavior if needed

           
        }
    }
}
