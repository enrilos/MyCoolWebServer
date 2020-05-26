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
        public bool Create(string username, string password)
        {
            using (var db = new ByTheCakeDbContext())
            {
                if (db.Users.Any(u => u.Username == username))
                {
                    return false;
                }

                var user = new User
                {
                    Username = username,
                    Password = password,
                    RegistrationDate = DateTime.UtcNow
                };

                db.Users.Add(user);
                db.SaveChanges();

                return true;
            }
        }

        public bool Exists(string username, string password)
        {
            using (var db = new ByTheCakeDbContext())
            {
                return db.Users.Any(u => u.Username == username && u.Password == password);
            }
        }

        public ProfileUserViewModel Get(string username)
        {
            using (var db = new ByTheCakeDbContext())
            {
                var user = db
                    .Users
                    .Where(u => u.Username == username)
                    .Select(u => new ProfileUserViewModel
                    {
                        Username = u.Username,
                        RegistrationDate = u.RegistrationDate,
                        TotalOrders = u.Orders.Count
                    })
                    .FirstOrDefault();

                return user;
            }
        }
    }
}
