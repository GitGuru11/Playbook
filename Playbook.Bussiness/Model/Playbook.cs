namespace Playbook.Bussiness.Model
{
    /// <summary>
    /// Base class for PlaybookEntity.
    /// </summary>
    public partial class Playbook : BaseClass, IDisposable
    {
        /// <summary>
        /// Gets or sets the Playbook Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Playbook Description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the Playbook ObjectTypeId.
        /// </summary>
        public int ObjectTypeId { get; set; }

        /// <summary>
        /// Gets or sets the Playbook Enable.
        /// </summary>
        public bool Enable { get; set; }

        /// <summary>
        /// Gets or sets the Playbook LastRun.
        /// </summary>
        public DateTime LastRun { get; set; }

        /// <summary>
        /// Gets or sets the Playbook IsDeleted.
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
        /// Initializes a new instance of the <see cref="Playbook"/> class.
        /// </summary>    
        public Playbook() : base()
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