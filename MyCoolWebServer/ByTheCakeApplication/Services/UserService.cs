namespace MyCoolWebServer.ByTheCakeApplication.Services
{
    using Contracts;
    using Data;
    using Data.Models;
    using ViewModels.Account;
    using System;
    using System.Linq;

    public class UserService : IUserService
    {
        private ByTheCakeDbContext db;

        public UserService()
        {
            this.db = new ByTheCakeDbContext();
        }

        public bool Create(string username, string password)
        {
            if (this.db.Users.Any(u => u.Username == username))
            {
                return false;
            }

            var user = new User
            {
                Username = username,
                Password = password,
                RegistrationDate = DateTime.UtcNow
            };

            this.db.Users.Add(user);
            this.db.SaveChanges();

            return true;

        }

        public bool Exists(string username, string password)
        {
            return this.db.Users.Any(u => u.Username == username && u.Password == password);
        }

        public ProfileUserViewModel Get(string username)
        {
            var users = this.db
                .Users
                .Where(u => u.Username == username)
                .Select(u => new ProfileUserViewModel
                {
                    Username = u.Username,
                    RegistrationDate = u.RegistrationDate,
                    TotalOrders = u.Orders.Count
                })
                .FirstOrDefault();

            return users;
        }

        public int? GetUserId(string username)
        {
            var id = this.db.Users.FirstOrDefault(x => x.Username == username).Id;

            return id != 0 ? (int?)id : null;
        }
    }
}
