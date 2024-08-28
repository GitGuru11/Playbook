
using Playbook.Data.ClickHouse;

namespace Playbook.Business.Model
{
    using System;




    /// <summary>
    /// An interface for IObjectType.
    /// </summary>
    public interface IObjectType : IDisposable
    {

        /// <summary>
        /// Gets or sets the HlogPersonId.
        /// </summary>
        int ObjectTypeId { get; set; }

        /// <summary>
        /// Gets or sets the AppPersonId.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the FirstName.
        /// </summary>
        string Label { get; set; }

        /// <summary>
        /// Gets or sets the IsDeleted.
        /// </summary>
        bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the CreatedDate.
        /// </summary>
        DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the ModifiedDate.
        /// </summary>
        DateTime UpdatedAt { get; set; }

    }


    /// <summary>
    /// Base class for PersonProfile.
    /// </summary>
    public partial class ObjectType : DatabaseObject, IObjectType
    {


        /// <summary>
        /// Gets or sets the HlogPersonId.
        /// </summary>
        public int ObjectTypeId { get; set; }

        /// <summary>
        /// Gets or sets the AppPersonId.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the FirstName.
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets the IsDeleted.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the CreatedDate.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the ModifiedDate.
        /// </summary>
        public DateTime UpdatedAt { get; set; }


        /// <summary>
        /// Gets or sets the primary key.
        /// </summary>
        public override object PrimaryKey
        {
            get
            {
                return ObjectTypeId;
            }
            set
            {
                this.ObjectTypeId = (int)value;
            }
        }



        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectType"/> class.
        /// </summary>    
        public ObjectType() : base()
        {
        }

        /// <summary>
        /// The dispose.
        /// </summary>
        public void Dispose()
        {
        }
    }



}
