using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern
{
    public class ExportAttribute : Attribute
    {
        #region  Constructor

        public ExportAttribute()
        {
            this.InstanceType = InstanceType.SingleInstance;
        }

        public ExportAttribute(InstanceType instanceType)
        {
            this.InstanceType = instanceType;
        }

        #endregion

        #region Properties

        public InstanceType InstanceType { get; set; }

        #endregion
    }

    public enum InstanceType
    {
        SingleInstance,
        InstancePerDependency,
        InstancePerRequest
    }
}
