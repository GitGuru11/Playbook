namespace Playbook.Service.Contracts
{
    public class PlaybookRevision
    {
        public string Id { get; set; }
        public string PlaybookId { get; set; }
        public int Number { get; set; }
        public string Label { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
