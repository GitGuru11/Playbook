using Playbook.Data.ClickHouse;

namespace Playbook.Bussiness.Model;

public class PlaybookNode : DatabaseObject, IDisposable
{
    /// <summary>
    /// Gets or sets the NodeId.
    /// </summary>
    public string NodeId { get; set; }

    // <summary>
    /// Gets or sets the NodeName.
    /// </summary>
    public string NodeName { get; set; }

    /// <summary>
    /// Gets or sets the NodeType.
    /// </summary>
    public string NodeType { get; set; }

    /// <summary>
    /// Gets or sets the ActionType.
    /// </summary>
    public string? ActionType { get; set; }

    /// <summary>
    /// Gets or sets the ParentNodeId.
    /// </summary>
    public string? ParentNodeId { get; set; }

    /// <summary>
    /// Gets or sets the Delay.
    /// </summary>
    public long Delay { get; set; }
    
    /// <summary>
    /// Gets or sets the DelayType.
    /// </summary>
    public string DelayType { get; set; }
    
    /// <summary>
    /// Gets or sets the PlaybookId.
    /// </summary>
    public string PlaybookId { get; set; }

    /// <summary>
    /// Gets or sets the Version.
    /// </summary>
    public int Version { get; set; }
    
    /// <summary>
    /// Gets or sets the Order.
    /// </summary>
    public int Order { get; set; }

    /// <summary>
    /// Gets or sets the primary key.
    /// </summary>
    public override object PrimaryKey
    {
        get => NodeId;
        set => NodeId = $"{value}";
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PlaybookNode"/> class.
    /// </summary>    
    public PlaybookNode() : base()
    {
    }

    /// <summary>
    /// The dispose.
    /// </summary>
    public void Dispose()
    {
    }
}