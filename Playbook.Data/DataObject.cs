
namespace Playbook.Data.ClickHouse
{
    using System;

    /// <summary>
    /// The data object is a base of all objects that are intended for use with the database.
    /// </summary>
    public abstract class DatabaseObject
    {

        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether IsModified.
        /// </summary>
        public virtual bool IsModified { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether IsNew.
        /// </summary>
        public virtual bool IsNew { get; set; }


        /// <summary>
        /// Gets or sets PrimaryKey.
        /// </summary>
        public abstract object PrimaryKey { get; set; }

        #endregion

    }
}