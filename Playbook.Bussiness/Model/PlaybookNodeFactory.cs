using Playbook.Common.Tenant;
using Playbook.Data.ClickHouse;

namespace Playbook.Bussiness.Model
{
    /// <summary>
    /// Factory base class for PlaybookNodeFactory.
    /// </summary>
    public partial class PlaybookNodeFactory : PlaybookNodeFactoryBase
    {
        public async Task<List<PlaybookNode>> FindNodesByParentNodeIdAsync(string parentNodeId)
        {
            await using var dataHandler = new DatabaseHandler();
            dataHandler.Command.CommandText = $"{SelectAllStatement} WHERE ParentNodeId = @PARENTNODEID";
            AddParentNodeIdParameter(dataHandler, parentNodeId);
            return await FindAsync(dataHandler);
        }
        public async Task<List<String>> FindNodesIDsByParentNodeIdAsync(string parentNodeId)
        {
            await using var dataHandler = new DatabaseHandler();
            dataHandler.Command.CommandText = $"{SelectAllStatement} WHERE ParentNodeId = @PARENTNODEID";
            AddParentNodeIdParameter(dataHandler, parentNodeId);

            return (await FindAsync(dataHandler)).Select(x => x.NodeId).ToList();
        }
        
        public async Task<List<PlaybookNode>> FindNodesByPlaybookIdAsync(string playbookId)
        {
            await using var dataHandler = new DatabaseHandler();
            dataHandler.Command.CommandText = $"{SelectAllStatement} WHERE PlaybookId = @PLAYBOOKID";
            AddPlaybookIdParameter(dataHandler, playbookId);
            return await FindAsync(dataHandler);
        }
        public async Task<List<PlaybookNode>> FindNodesByNodeIdsdAsync(IEnumerable<string> nodeIds)
        {
            await using var dataHandler = new DatabaseHandler();
            dataHandler.Command.CommandText = $"{SelectAllStatement} WHERE NodeId IN ({nodeIds})";

            return await FindAsync(dataHandler);
        }
        
        public async Task<PlaybookNode?> FindNodeByNodeIdAsync(string nodeId)
        {
            await using var dataHandler = new DatabaseHandler();
            dataHandler.Command.CommandText = $"{SelectAllStatement} WHERE NodeId=@NODEID";
            AddNodeIdParameter(dataHandler, nodeId);
            return (await FindAsync(dataHandler)).FirstOrDefault();
        }

        public async Task<IEnumerable<string>?> FindNodeIdsByPlaybookIdAsync(string playbookId)
        {
            await using var dataHandler = new DatabaseHandler();
            dataHandler.Command.CommandText =
                $"SELECT NodeId FROM {{TenantContext.Current.TenantName}}.PlaybookNode WHERE PlaybookId = {playbookId}";
            var res = await dataHandler.ExecuteScalarAsync(new CancellationToken());
            return (IEnumerable<string>)res;
        }
    }
}