using Microsoft.AspNetCore.Mvc;
using AppServer.Data;
using AppServer.Models.Domains;
using AppServer.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace AppServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeavyTaskController : Controller
    {
        private readonly HeavyTaskDbContext dbContext;
        public HeavyTaskController(HeavyTaskDbContext dbContext)
        { 
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllCards()
        {
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
                id = createdTask.id,
                name = createdTask.name,
                description = createdTask.description,
                result = createdTask.result,
                startedAt = createdTask.startedAt,
                finishedAt = createdTask.finishedAt,
                percentageDone = createdTask.percentageDone
            };

            dbContext.HeavyTasks.Add(taskDomain);
            await dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCard), new { id = taskDomain.id }, taskDomain);
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
