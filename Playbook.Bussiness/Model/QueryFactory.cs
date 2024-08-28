using Playbook.Data.ClickHouse;

namespace Playbook.Bussiness.Model
{
    /// <summary>
    /// Factory base class for QueryFactory.
    /// </summary>
    public partial class QueryFactory : QueryFactoryBase
    {
        public async Task<Query> FindQueryByParentId(string parentId)
        {
            await using var dataHandler = new DatabaseHandler();
            dataHandler.Command.CommandText = $"{SelectAllStatement} WHERE ParentId=@PARENTID";
            AddParentIdParameter(dataHandler, parentId);
            return (await FindAsync(dataHandler)).First();
        }        
        
        public async Task<List<Query>> FindQueriesByNodeId(string nodeId)
        {
            await using var dataHandler = new DatabaseHandler();
            dataHandler.Command.CommandText = $"{SelectAllStatement} WHERE NodeId=@NODEID";
            AddNodeIdParameter(dataHandler, nodeId);
            return await FindAsync(dataHandler);
        }

        public async Task<List<Query>> FindQueriesByNodeIds(IEnumerable<string> nodeIds)
        {
            await using var dataHandler = new DatabaseHandler();
            dataHandler.Command.CommandText = $"{SelectAllStatement} WHERE NodeId IN ({nodeIds})";
            return await FindAsync(dataHandler);        
        }
    }
}