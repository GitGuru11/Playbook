using Playbook.Data.ClickHouse;

namespace Playbook.Bussiness.Model;

public class QueryCondition : DatabaseObject, IDisposable
{
    public string ConditionId { get; set; }
    public string QueryId { get; set; }
    public string Field { get; set; }
    public string Value { get; set; }
    public string Operator { get; set; }
    public string NestedQueryId { get; set; }


    /// <summary>
    /// Gets or sets the primary key.
    /// </summary>
    public override object PrimaryKey
    {
        get => ConditionId;
        set => ConditionId = $"{value}";
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="QueryCondition"/> class.
    /// </summary>    
    public QueryCondition() : base()
    {
    }

    /// <summary>
    /// The dispose.
    /// </summary>
    public void Dispose()
    {
    }
}