using Microsoft.EntityFrameworkCore;using JobTrack.Api.Models;
namespace JobTrack.Api.Data;
public class JobTrackDbContext:DbContext{
 public JobTrackDbContext(DbContextOptions<JobTrackDbContext> o):base(o){}
 public DbSet<Application> Applications=>Set<Application>(); public DbSet<User> Users=>Set<User>();
 protected override void OnModelCreating(ModelBuilder m){
  m.Entity<User>().HasIndex(x=>x.Email).IsUnique();
  m.Entity<Application>().HasOne(x=>x.User).WithMany(x=>x.Applications).HasForeignKey(x=>x.UserId).OnDelete(DeleteBehavior.Cascade);
 }
}
