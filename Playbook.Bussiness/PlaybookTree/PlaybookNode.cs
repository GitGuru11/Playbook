namespace Playbook.Bussiness.PlaybookTree;

public class PlaybookNode
{
    public string Id { get; set; }
    public string NodeType { get; set; }
    public int Order { get; set; }
    public string Name { get; set; }
    
    public PlaybookNode? Parent { get; set; }
    public List<Query> Queries { get; set; } =  new();
    public PlaybookDefinition Definition { get; set; }
    
}