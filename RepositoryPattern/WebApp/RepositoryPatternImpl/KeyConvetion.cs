using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace WebApp.RepositoryPatternImpl
{
    public class KeyConvetion : Convention
    {

        public KeyConvetion()
        {
            this.Properties<int>()
                .Where(p => p.Name.Equals("Id"))
                .Configure(p => p.IsKey());
        }
    }
}