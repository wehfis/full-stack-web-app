using AppServer.DAL.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServer.DAL.Repositories
{
    internal interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User GetById(Guid UserID);
        User GetByEmail(string UserEmail);
        void Insert(User user);
        void Delete(Guid userID);
        void Save();
    }
}
