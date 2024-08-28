using Playbook.Bussiness.Model;
using Playbook.Data.ClickHouse;

public class PlaybookBranch : DatabaseObject, IDisposable
{
    /// <summary>
    /// Gets or sets the PlaybookBranch Id.
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Gets or sets the PlaybookBranch Name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the PlaybookBranch ParentId.
    /// </summary>
    public string PlaybookdDefintionId { get; set; }

    /// <summary>
    /// Gets or sets the primary key.
    /// </summary>
    public override object PrimaryKey
    {
        get => Id;
        set => Id = $"{value}";
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PlaybookBranch"/> class.
    /// </summary>    
    public PlaybookBranch() : base()
    {
    }

    /// <summary>
    /// The dispose.
    /// </summary>
    public void Dispose()
    {
    }
}
