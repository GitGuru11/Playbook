using System.ComponentModel.DataAnnotations;

namespace Playbook.Service.Contracts
{
    public class PlaybookRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int ObjectTypeId { get; set; }
    }
}
