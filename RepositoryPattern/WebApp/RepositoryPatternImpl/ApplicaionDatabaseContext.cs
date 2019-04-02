using RepositoryPattern;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace WebApp.RepositoryPatternImpl
{

    [Export(InstanceType.SingleInstance)]
    public class ApplicationDatabaseStandaloneContextCreator : IApplicationDatabaseStandaloneContextCreator
    {
        public IApplicationDatabaseStandaloneContext Create()
        {
            return new ApplicationDatabaseStandaloneContext();
        }
    }

    public abstract class ApplicationDatabaseBaseContext : DbContext
    {
        public ApplicationDatabaseBaseContext() : base("ConectionString")
        {
        }
        public virtual void ExecuteNonQuery(string query, params object[] parameters)
        {
            this.Database.CommandTimeout = this.Database.Connection.ConnectionTimeout;
            this.Database.ExecuteSqlCommand(query, parameters);
        }
        public virtual IEnumerable<T> SqlQuery<T>(string query, params object[] parameters)
        {
            this.Database.CommandTimeout = this.Database.Connection.ConnectionTimeout;
            return this.Database.SqlQuery<T>(query, parameters);
        }
    }

    public class ApplicationDatabaseStandaloneContext : ApplicationDatabaseBaseContext, IApplicationDatabaseStandaloneContext
    {


    }

    [Export(InstanceType.InstancePerRequest)]
    public class ApplicaionDatabaseContext : ApplicationDatabaseBaseContext, IApplicationDatabaseContext
    {

        public ApplicaionDatabaseContext() { }

        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add<KeyConvetion>();
            modelBuilder.Properties<string>()
                .Configure(p => p.HasMaxLength(200).HasColumnType("nvarchar"));
        }

        public void Save()
        {
            try
            {
                this.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                //foreach (var eve in e.EntityValidationErrors)
                //{
                //    WriteMessage(string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                //        eve.Entry.Entity.GetType().Name, eve.Entry.State));
                //    foreach (var ve in eve.ValidationErrors)
                //        WriteMessage(string.Format("- Property: \"{0}\", Error: \"{1}\"",
                //            ve.PropertyName, ve.ErrorMessage));
                //}
                throw;
            }
        }

        public void BeginTransaction()
        {
            this.Database.BeginTransaction();
        }

        public void RollBack()
        {
            this.Database.CurrentTransaction.Rollback();
        }

        public void Commit()
        {
            this.Database.CurrentTransaction.Commit();
        }

        public void DisableValidation()
        {
            this.Configuration.ValidateOnSaveEnabled = false;
        }
    }
}