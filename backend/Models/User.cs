using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace JobTrack.Api.Models;
[Table("users")]
public class User {
 [Key][Column("id")] public int Id {get;set;}
 [Required,MaxLength(120)][Column("name")] public string Name {get;set;}=string.Empty;
 [Required,MaxLength(180)][Column("email")] public string Email {get;set;}=string.Empty;
 [Required,MaxLength(255)][Column("password_hash")] public string PasswordHash {get;set;}=string.Empty;
 public List<Application> Applications {get;set;}=new();
}
