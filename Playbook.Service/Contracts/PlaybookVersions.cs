namespace Playbook.Service.Contracts
{
    public class PlaybookVersions
    {
        public long VersionID { get; set; }
        public long PlaybookID { get; set; }
        public int VersionNumber { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
