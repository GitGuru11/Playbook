using Playbook.Data.ClickHouse;

namespace Playbook.Bussiness.Model
{
    /// <summary>
    /// Base class for PlaybookDefinition.
    /// </summary>
    public class PlaybookDefinition : DatabaseObject, IDisposable
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the IsActive.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the VersionNumber.
        /// </summary>
        public int VersionNumber { get; set; }

        /// <summary>
        /// Gets or sets the ObjectTypeId.
        /// </summary>
        public int ObjectTypeId { get; set; }


        public override object PrimaryKey
        {
            get => Id;
            set => Id = $"{value}";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlaybookDefinition"/> class.
        /// </summary>    
        public PlaybookDefinition() : base()
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