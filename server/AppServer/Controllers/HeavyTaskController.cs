﻿using Microsoft.AspNetCore.Mvc;
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
            var tasks = await dbContext.HeavyTasks.ToListAsync();

            return Ok(tasks);
        }

        [HttpGet]
        [Route("GetSpecUser/{userId:Guid}")]
        public async Task<ActionResult> GetAllCards(Guid userId)
        {
            var tasks = await dbContext.HeavyTasks
                .Where(task => task.OwnerId == userId)
                .ToListAsync();

            return Ok(tasks);
        }

        [HttpGet]
        [Route("{Id:Guid}")]
        public async Task<ActionResult> GetCard(Guid Id)
        {
            var task = await dbContext.HeavyTasks.FindAsync(Id);

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
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == createdTask.OwnerId);
            var taskDomain = new HeavyTask
            {
                Id = createdTask.Id,
                Name = createdTask.Name,
                Description = createdTask.Description,
                StartedAt = DateTime.Now,
                OwnerId = createdTask.OwnerId
            };

            dbContext.HeavyTasks.Add(taskDomain);
            await dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCard),new {Id = createdTask.Id }, taskDomain);
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
