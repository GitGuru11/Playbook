using Playbook.Data.ClickHouse;
using Playbook.Common.Tenant;

namespace Playbook.Bussiness.Model
{
    /// <summary>
    /// Factory base class for PlaybookDefinitionFactory.
    /// </summary>
    public partial class PlaybookDefinitionFactory : PlaybookDefinitionFactoryBase
    {
        public async Task<List<PlaybookDefinition>> FindDefinitionsByParentIdAsync(string parentId)
        {
            await using var dataHandler = new DatabaseHandler();
            dataHandler.Command.CommandText = $"{SelectAllStatement} WHERE ParentId = '{parentId}'";
            return await FindAsync(dataHandler);
        }
        
        public async Task<int> LastVersionNumberByParentIdAsync(string parentId)
        {
            await using var dataHandler = new DatabaseHandler();
            dataHandler.Command.CommandText = $"SELECT MAX(VersionNumber) AS VersionNumber FROM {TenantContext.Current.TenantName}.PlaybookDefinition WHERE ParentId = '{parentId}'";
            int.TryParse(dataHandler.ExecuteScalar().ToString(), out int result);
            return result;
        }

        public async Task<PlaybookDefinition?> LastVersionByParentIdAsync(string parentId)
        {
            await using var dataHandler = new DatabaseHandler();
            dataHandler.Command.CommandText = $"{SelectAllStatement} WHERE ParentId = '{parentId}' AND VersionNumber = (SELECT MAX(VersionNumber) FROM data_app.PlaybookDefinition WHERE ParentId = '{parentId}')";
            return (await FindAsync(dataHandler)).FirstOrDefault();
        }
    }
}