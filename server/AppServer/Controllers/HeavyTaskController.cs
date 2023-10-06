using Microsoft.AspNetCore.Mvc;
using AppServer.Data;
using AppServer.Models.Domains;
using AppServer.Models.DTOs;

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
        public IActionResult GetAllCards()
        {
            var tasks = dbContext.HeavyTasks.ToList();

            return Ok(tasks);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetCard(Guid id)
        {
            var task = dbContext.HeavyTasks.Find(id);

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
        public IActionResult AddTaskCard([FromBody] HeavyTaskDTO createdTask)
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
            dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetCard), new { id = taskDomain.id }, taskDomain);
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult DeleteCard(Guid id)
        {
            var task = dbContext.HeavyTasks.Find(id);

            if (task == null)
            {
                return NotFound();
            }
            else
            {
                dbContext.HeavyTasks.Remove(task);
                dbContext.SaveChanges();
                return Ok(task);
            }
        }
    }
}
