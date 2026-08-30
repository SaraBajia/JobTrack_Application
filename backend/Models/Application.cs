using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace JobTrack.Api.Models;
[Table("applications")]
public class Application {
 [Key][Column("id")] public int Id{get;set;}
 [Column("user_id")] public int UserId{get;set;}
 [Required,MaxLength(150)][Column("company")] public string Company{get;set;}=string.Empty;
 [Required,MaxLength(180)][Column("position")] public string Position{get;set;}=string.Empty;
 [MaxLength(50)][Column("type")] public string Type{get;set;}="Internship";
 [MaxLength(150)][Column("location")] public string? Location{get;set;}
 [MaxLength(50)][Column("status")] public string Status{get;set;}="Applied";
 [MaxLength(20)][Column("priority")] public string Priority{get;set;}="Medium";
 [Column("applied_date")] public DateTime AppliedDate{get;set;}=DateTime.UtcNow;
 [Column("interview_date")] public DateTime? InterviewDate{get;set;}
 [Column("notes")] public string? Notes{get;set;}
 public User? User{get;set;}
}
