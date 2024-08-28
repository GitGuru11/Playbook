using Playbook.Data.ClickHouse;

namespace Playbook.Bussiness.Model
{
    public class Query : DatabaseObject, IDisposable
    {
        public string QueryId { get; set; }
        public string ParentId { get; set; }
        public string NodeId { get; set; }
        public bool IsAnd { get; set; }

        /// <summary>
        /// Gets or sets the primary key.
        /// </summary>
        public override object PrimaryKey
        {
            get => QueryId;
            set => QueryId = $"{value}";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Query"/> class.
        /// </summary>    
        public Query()
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

