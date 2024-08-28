namespace Playbook.Service.Contracts
{
    public class PlaybookNodes
    {
        public long NodeID { get; set; }
        public long VersionID { get; set; }
        public long? ParentNodeID { get; set; } 
        public string NodeType { get; set; }
        public int OrderIndex { get; set; }
        public float DelayDuration { get; set; } 
        public string ActionType { get; set; }
        public string ActionParameters { get; set; }
    }
}
