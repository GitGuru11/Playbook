using Playbook.Data.ClickHouse;

namespace Playbook.Bussiness.Model;

public abstract class BaseClass : DatabaseObject
{
    /// <summary>
    /// Gets or sets the id.
    /// </summary>
    public string Id { get; set; }
    
    /// <summary>
    /// Gets or sets the CreatedAt.
    /// </summary>
    public DateTime CreatedAt { get; set; }
        
    /// <summary>
    /// Gets or sets the UpdatedAt.
    /// </summary>
    public DateTime UpdatedAt { get; set; }
}