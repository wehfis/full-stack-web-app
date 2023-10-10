using Microsoft.AspNetCore.Mvc;
using AppServer.Data;
using AppServer.Models.Domains;
using AppServer.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace AppServer.Controllers
{
    [Route("task/[controller]")]
    [ApiController]
    [Authorize]
    public class HeavyTaskController : Controller
    {
        private readonly AppServerDbContext dbContext;
        public HeavyTaskController(AppServerDbContext dbContext)
        { 
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllCards()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var tasks = await dbContext.HeavyTasks.ToListAsync();

            return Ok(tasks);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<ActionResult> GetCard(Guid id)
        {
            var task = await dbContext.HeavyTasks.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(task);
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddTaskCard([FromBody] HeavyTaskDTO createdTask)
        {
            var taskDomain = new HeavyTask
            {
                Id = createdTask.Id,
                Name = createdTask.Name,
                Description = createdTask.Description,
                StartedAt = DateTime.Now
            };

            dbContext.HeavyTasks.Add(taskDomain);
            await dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCard), new { Id = taskDomain.Id }, taskDomain);
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<ActionResult> DeleteCard(Guid id)
        {
            var task = await dbContext.HeavyTasks.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }
            else
            {
                dbContext.HeavyTasks.Remove(task);
                await dbContext.SaveChangesAsync();
                return Ok(task);
            }
        }
    }
}
