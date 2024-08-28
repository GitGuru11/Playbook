namespace Playbook.Bussiness.Model
{
    /// <summary>
    /// Base class for ObjectField.
    /// </summary>
    public partial class ObjectField : BaseClass, IDisposable
    {
        /// <summary>
        /// Gets or sets the ObjectField ObjectTypeId.
        /// </summary>
        public string ObjectTypeId { get; set; }
        /// <summary>
        /// Gets or sets the ObjectField Name.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the ObjectField Label.
        /// </summary>
        public string Label { get; set; }
        /// <summary>
        /// Gets or sets the ObjectField FieldType.
        /// </summary>
        public string FieldType { get; set; }
        /// <summary>
        /// <summary>
        /// Gets or sets the ObjectField FieldType.
        /// </summary>
        public string OptionId { get; set; }
        /// <summary>
        /// Gets or sets the ObjectField IsDeleted.
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
        /// Initializes a new instance of the <see cref="ObjectField"/> class.
        /// </summary>    
        public ObjectField() : base()
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