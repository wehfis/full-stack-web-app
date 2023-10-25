using AppServer.DAL.Data;
using AppServer.DAL.Models.Domains;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServer.BLL.Calculations
{
    public class DataCalculation
    {
        private AppServerDbContext _dbContext;
        private HeavyTask _heavyTask;

        public DataCalculation(AppServerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task StartCalculation(HeavyTask heavyTask)
        {
            HeavyTask _heavyTask = new HeavyTask
            {
                Id = heavyTask.Id,
                Name = heavyTask.Name,
                Description = heavyTask.Description,
                StartedAt = heavyTask.StartedAt,
                OwnerId = heavyTask.OwnerId,
                PercentageDone = heavyTask.PercentageDone
            };
            string binaryDataResult = "";
            int dataLength = _heavyTask.Description.Length;
            string taskDescription = _heavyTask.Description;
            uint _percentageDone = _heavyTask.PercentageDone;
            for (int i = 0; i < dataLength; i++)
            {
                await SimulateLongCalculationStep();

                _percentageDone = (uint)((i + 1) / (double)dataLength * 100);
                _heavyTask.PercentageDone = _percentageDone;
                
                binaryDataResult += taskDescription[i].ToString();
                _heavyTask.Result = binaryDataResult;

                _dbContext.HeavyTasks.Update(_heavyTask);
                await _dbContext.SaveChangesAsync();
            }
            if (_heavyTask.PercentageDone == 100)
            {
                _heavyTask.FinishedAt = DateTime.Now;
            }
            _dbContext.HeavyTasks.Update(_heavyTask);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<uint> GetPercentage(HeavyTask heavyTask)
        {
            return heavyTask.PercentageDone;
        }

        private async Task SimulateLongCalculationStep()
        {
            await Task.Delay(1000);
        }
    }

}
