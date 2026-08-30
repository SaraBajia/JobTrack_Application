using Microsoft.AspNetCore.Authorization;using Microsoft.AspNetCore.Mvc;using Microsoft.EntityFrameworkCore;using JobTrack.Api.Data;using JobTrack.Api.Models;using System.Security.Claims;
namespace JobTrack.Api.Controllers;
[Authorize][ApiController][Route("api/applications")]
public class ApplicationsController:ControllerBase{readonly JobTrackDbContext db;public ApplicationsController(JobTrackDbContext d)=>db=d;int UserId()=>int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
 [HttpGet]public async Task<IActionResult> Get()=>Ok(await db.Applications.Where(x=>x.UserId==UserId()).OrderByDescending(x=>x.AppliedDate).ToListAsync());
 [HttpGet("{id:int}")]public async Task<IActionResult> Get(int id){var x=await db.Applications.FirstOrDefaultAsync(x=>x.Id==id&&x.UserId==UserId());return x==null?NotFound():Ok(x);}
 [HttpPost]public async Task<IActionResult> Post(Application x){x.Id=0;x.UserId=UserId();if(x.AppliedDate==default)x.AppliedDate=DateTime.UtcNow;db.Applications.Add(x);await db.SaveChangesAsync();return CreatedAtAction(nameof(Get),new{id=x.Id},x);}
 [HttpPut("{id:int}")]public async Task<IActionResult> Put(int id,Application input){var x=await db.Applications.FirstOrDefaultAsync(a=>a.Id==id&&a.UserId==UserId());if(x==null)return NotFound();x.Company=input.Company;x.Position=input.Position;x.Type=input.Type;x.Location=input.Location;x.Status=input.Status;x.Priority=input.Priority;x.AppliedDate=input.AppliedDate;x.InterviewDate=input.InterviewDate;x.Notes=input.Notes;await db.SaveChangesAsync();return Ok(x);}
 [HttpDelete("{id:int}")]public async Task<IActionResult> Delete(int id){var x=await db.Applications.FirstOrDefaultAsync(a=>a.Id==id&&a.UserId==UserId());if(x==null)return NotFound();db.Applications.Remove(x);await db.SaveChangesAsync();return NoContent();}
}
