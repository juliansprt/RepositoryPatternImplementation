using RepositoryPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.RepositoryPatternImpl
{
    [Export(InstanceType.SingleInstance)]
    public class UserService : IUserService
    {
        private IRepository<User> Users;
        public UserService(IRepository<User> users)
        {
            this.Users = users;
        }

        public User GetByName(string name)
        {
            return Users.FirstOrDefault(p => p.Name == name);
        }
    }
}