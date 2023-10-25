using AppServer.DAL.Data;
using AppServer.DAL.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace AppServer.DAL.Repositories.Concrete
{
    internal class UserRepository: IUserRepository
    {
        private readonly AppServerDbContext _dbContext;
        public UserRepository(AppServerDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public IEnumerable<User> GetAll()
        {
            return _dbContext.Users.ToList();
        }
        public User GetById(Guid UserID)
        {
            return _dbContext.Users.Find(UserID);
        }
        public User GetByEmail(string UserEmail)
        {
            return _dbContext.Users
                .Where(u => u.Email == UserEmail)
                .FirstOrDefault();
        }

        public void Insert(User user)
        {
            _dbContext.Users.Add(user);
        }
        public void Delete(Guid userID)
        {
            _dbContext.Remove(userID);
        }
        public async void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
