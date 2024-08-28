namespace Playbook.Service
{
    public static class Routes
    {
        /// <summary>
        /// Health check.
        /// </summary>
        public const string ServiceStatus = "api/service-status";

        /// <summary>
        /// Supported Objects
        /// </summary>
        public const string ObjectType = "api/{v:apiVersion}/playbook/object/type";

        /// <summary>
        /// List Playbook Objects with sorting, searching, and pagination.
        /// </summary>
        public const string PlaybookObjectsList = "api/{v:apiVersion}/playbook/objects";

        /// <summary>
        /// Create a new Playbook Object.
        /// </summary>
        public const string PlaybookObjectCreate = "api/{v:apiVersion}/playbook/object/create";

        /// <summary>
        /// Update an existing Playbook Object by ID.
        /// </summary>
        public const string PlaybookObjectUpdate = "api/{v:apiVersion}/playbook/object/update/{id}";

        /// <summary>
        /// Delete a Playbook Object by ID.
        /// </summary>
        public const string PlaybookObjectDelete = "api/{v:apiVersion}/playbook/object/delete/{id}";

        /// <summary>
        /// Get detailed information about a Playbook Object by ID.
        /// </summary>
        public const string PlaybookObjectDetail = "api/{v:apiVersion}/playbook/object/detail/{id}";

        /// <summary>
        /// List Existing Objects Field
        /// </summary>
        public const string ObjectsFieldList = "api/{v:apiVersion}/playbook/object/fields";

        /// <summary>
        /// Create Objects Field
        /// </summary>
        public const string ObjectsFieldCreate = "api/{v:apiVersion}/playbook/object/field/create";

        /// <summary>
        /// Update Objects Field
        /// </summary>
        public const string ObjectsFieldUpdate = "api/{v:apiVersion}/playbook/object/field/update";

        /// <summary>
        /// Delete field/
        /// </summary>
        public const string ObjectsFieldDelete = "api/{v:apiVersion}/playbook/object/field/delete";

        /// <summary>
        /// Detail field/
        /// </summary>
        public const string ObjectsFieldDetail = "api/{v:apiVersion}/playbook/object/field/detail";

        /// <summary>
        /// List Existing Playbooks
        /// </summary>
        public const string PlaybookList = "api/{v:apiVersion}/playbooks";

        /// <summary>
        /// Create Playbook
        /// </summary>
        public const string PlaybookCreate = "api/{v:apiVersion}/playbook/create";

        /// <summary>
        /// Update Playbook
        /// </summary>
        public const string PlaybookUpdate = "api/{v:apiVersion}/playbook/update";

        /// <summary>
        /// Delete Playbook
        /// </summary>
        public const string PlaybookDelete = "api/{v:apiVersion}/playbook/delete";

        /// <summary>
        /// Enable Playbook
        /// </summary>
        public const string PlaybookEnable = "api/{v:apiVersion}/playbook/enable";

        /// <summary>
        /// Playbook Version
        /// </summary>
        public const string PlaybookVersions = "api/{v:apiVersion}/playbooks/{id}/versions";

        /// <summary>
        /// Detail Playbook
        /// </summary>
        public const string PlaybookDetail = "api/{v:apiVersion}/playbooks/detail";

        // /// <summary>
        // /// Playbook Node
        // /// </summary>
        // public const string PlaybookNode = "api/{v:apiVersion}/playbook/playbooks/nodes";

        /// <summary>
        /// Playbook Node
        /// </summary>
        public const string PlaybookTree = "api/{v:apiVersion}/playbook/tree/{playbook_id}";
        
        /// <summary>
        /// Playbook Node
        /// </summary>
        public const string FieldTypes = "api/{v:apiVersion}/playbook/fieldtypes";

        /// <summary>
        /// Playbook Node
        /// </summary>
        public const string FieldTypeRules = "api/{v:apiVersion}/playbook/field/type/rules";

        /// <summary>
        /// Playbook Node
        /// </summary>
        public const string RulesByFieldType = "api/{v:apiVersion}/playbook/field/type/rules/{fieldtype}";

        /// <summary>
        /// Playbook Node
        /// </summary>
        public const string PlaybookNode = "api/{v:apiVersion}/playbook/tree/test/{playbookid}/nodes";

        public const string CredentialGet = "api/{v:apiVersion}/leads/db/credential/get";

        public const string CredentialAdd = "api/{v:apiVersion}/leads/db/credential/add";

        public const string CredentialUpdate = "api/{v:apiVersion}/leads/db/credential/update";

        public const string CredentialDelete = "api/{v:apiVersion}/leads/db/credential/delete";

    }
}