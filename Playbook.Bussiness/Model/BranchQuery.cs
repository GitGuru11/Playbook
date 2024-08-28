namespace Playbook.Bussiness.Model;

public class BranchQuery
{
    public string Id { get; set; }
    public string BranchId { get; set; }
    public string FieldId { get; set; }
    public string Duration { get; set; }
    public string OperatorType { get; set; }
    public string Operator { get; set; }
    public string ParentId { get; set; }
}