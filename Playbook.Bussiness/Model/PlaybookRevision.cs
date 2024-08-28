namespace Playbook.Bussiness.Model
{
    /// <summary>
    /// Base class for PlaybookRevision.
    /// </summary>
    public class PlaybookRevision : BaseClass, IDisposable
    {
        /// <summary>
        /// Gets or sets the Playbook id.
        /// </summary>
        public string PlaybookId { get; set; }
        
        /// <summary>
        /// Gets or sets the Revision Number.
        /// </summary>
        public int Number { get; set; }
        
        /// <summary>
        /// Gets or sets the Revision IsDeleted.
        /// </summary>
        public bool IsDeleted { get; set; }

        public override object PrimaryKey
        {
            get => Id;
            set => Id = $"{value}";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlaybookRevision"/> class.
        /// </summary>    
        public PlaybookRevision() : base()
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