using RepositoryPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.RepositoryPatternImpl
{
    public class User : ILogicDeleteable
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}