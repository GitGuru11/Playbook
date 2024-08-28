namespace Playbook.Bussiness.PlaybookTree;

public class Query
{
    public string Operator { get; set; }
    public List<QueryValue> Values { get; set; }

    public Query()
    {
        this.Values = new List<QueryValue>();
    }
}