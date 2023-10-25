using AppServer.DAL.Data;
using AppServer.DAL.Models.Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServer.DAL.Repositories.Concrete
{
    internal class HeavyTaskRepository : IHeavyTaskRepository
    {
        private readonly AppServerDbContext _dbContext;
        public HeavyTaskRepository(AppServerDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public IEnumerable<HeavyTask> GetAll()
        {
            return _dbContext.HeavyTasks.ToList();
        }
        public IEnumerable<HeavyTask> GetAllByConcreteUser(Guid UserID)
        {
            return _dbContext.HeavyTasks
                .Where(task => task.OwnerId == UserID)
                .ToList();
        }
        public HeavyTask GetById(Guid HeavyTaskID)
        {
            return _dbContext.HeavyTasks.Find(HeavyTaskID);
        }
        public void Insert(HeavyTask task)
        {
            _dbContext.HeavyTasks.Add(task);
        }
        public void Delete(Guid HeavyTaskID)
        {
            _dbContext.Remove(HeavyTaskID);
        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
