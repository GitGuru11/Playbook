using System;

namespace Playbook.Service.Contracts
{
    /// <summary>
    /// Represents a playbook object within the system.
    /// </summary>
    public class PlaybookObjectRequest
    {
        public string Name { get; set; } // Name of the Playbook Object.
        public string Description { get; set; } // Description of the Playbook Object.
        public int ObjectTypeId { get; set; } // Type of the object, represented by its ID.
        public string Category { get; set; } // Category of the Playbook Object.

    }
}
