using AppServer.DAL.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServer.DAL.Repositories
{
    internal interface IHeavyTaskRepository
    {
        IEnumerable<HeavyTask> GetAll();
        IEnumerable<HeavyTask> GetAllByConcreteUser(Guid UserID);
        HeavyTask GetById(Guid HeavyTaskID);
        void Insert(HeavyTask task);
        void Delete(Guid HeavyTaskID);
        void Save();
    }
}
