using System;
using System.Collections.Generic;

namespace Playbook.Bussiness.Model
{
    /// <summary>
    /// Represents a Playbook Object with various attributes.
    /// </summary>
    public partial class PlaybookObject : BaseClass, IDisposable
    {
        /// <summary>
        /// Gets or sets the PlaybookObject ObjectTypeId.
        /// </summary>
        public int ObjectTypeId { get; set; }

        /// <summary>
        /// Gets or sets the PlaybookObject Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the PlaybookObject Description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the PlaybookObject Category.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the PlaybookObject IsDeleted status.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the primary key.
        /// </summary>
        public override object PrimaryKey
        {
            get => Id;
            set => Id = $"{value}";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlaybookObject"/> class.
        /// </summary>    
        public PlaybookObject() : base()
        {
        }

        /// <summary>
        /// The dispose method for cleanup.
        /// </summary>
        public void Dispose()
        {
        }
    }
}
