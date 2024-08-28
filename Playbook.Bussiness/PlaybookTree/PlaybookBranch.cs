namespace Playbook.Bussiness.PlaybookTree;

public class PlaybookBranch
{
    public string Id { get; set; }
    public string Name { get; set; }
    public List<Query> Query { get; set; }
    public string QueryType { get; set; }
    public List<string> ValidationErrors { get; set; }

    public PlaybookBranch()
    {
        this.Query = new List<Query>();
        this.ValidationErrors = new List<string>();
    }
}
