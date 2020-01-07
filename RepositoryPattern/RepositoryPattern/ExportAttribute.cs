using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPattern
{
    /// <summary>
    /// Export attribute for strategy injection
    /// </summary>
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

    /// <summary>
    /// Instance strategy types
    /// </summary>
    public enum InstanceType
    {
        /// <summary>
        /// Single instance for all request
        /// </summary>
        SingleInstance,
        /// <summary>
        /// Instance per dependency
        /// </summary>
        InstancePerDependency,
        /// <summary>
        /// Instance for each request
        /// </summary>
        InstancePerRequest
    }
}
