namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WebApp.RepositoryPatternImpl;

    internal sealed class Configuration : DbMigrationsConfiguration<WebApp.RepositoryPatternImpl.ApplicaionDatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WebApp.RepositoryPatternImpl.ApplicaionDatabaseContext context)
        {
            User user = new User()
            {
                Id = 1,
                Name = "First Name",
                LastName = "Last Name",
                Password = "1234"
            };

            context.Users.AddOrUpdate(user);
        }
    }
}
