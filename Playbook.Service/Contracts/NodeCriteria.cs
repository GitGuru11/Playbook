namespace Playbook.Service.Contracts
{
    public class NodeCriteria
    {
        public long CriteriaID { get; set; }
        public long NodeID { get; set; }
        public string Operator { get; set; }
        public int FieldID { get; set; }
        public string Value { get; set; }
        public string ComparisonOperator { get; set; }
    }
}
