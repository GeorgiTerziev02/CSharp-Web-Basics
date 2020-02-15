using Andreys.Data;
using Andreys.Models;
using Andreys.ViewModels.Users;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Andreys.Services
{
    public class UserService : IUserService
    {
        private readonly AndreysDbContext db;

        public UserService(AndreysDbContext db)
        {
            this.db = db;
        }

        public void Create(RegisterInputViewModel model)
        {
            var user = new User
            {
                Role = SIS.MvcFramework.IdentityRole.User,
                Username = model.Username,
                Email = model.Email,
                Password = this.Hash(model.Password)
            };

            this.db.Users.Add(user);
            this.db.SaveChanges();
        }

        public string GetUserId(string username, string password)
        {
            var hashedPassword = this.Hash(password);

            return this.db.Users.Where(x => x.Username == username && x.Password == hashedPassword)
                .Select(x => x.Id)
                .FirstOrDefault();
        }

        private string Hash(string input)
        {
            if (input == null)
            {
                return null;
            }

            var crypt = new SHA256Managed();
            var hash = new StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(input));

            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }

            return hash.ToString();
        }
    }
}
