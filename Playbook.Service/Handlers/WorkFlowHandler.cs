using MongoDB.Driver.Linq;
using Playbook.Bussiness.Model;
using Playbook.Common.Instrumentation;
using Playbook.Engine;
using Query = Playbook.Bussiness.Model.Query;
using QueryCondition = Playbook.Bussiness.Model.QueryCondition;

namespace Playbook.Service.Handlers;

public class WorkFlowHandler
{
    public async Task<bool> AddUpdateNode(string playbookId, WorkFlow request)
    {
        var playbookNodeFactory = new PlaybookNodeFactory();
        var playbookFactory = new PlaybookFactory();
        var playbook = await playbookFactory.FindAsync(playbookId);
        if (playbook is { Enable: false, IsDeleted: false })
        {
            var nodeIds = await playbookNodeFactory.FindNodeIdsByPlaybookIdAsync(playbookId);
            var requestNodeIds = request.Nodes.Select(node => node.Id);
            var removeNodeIds = nodeIds.Where(id => !requestNodeIds.Contains(id));
            await Task.WhenAll(CreateTaskForRemoveExtraNodes(removeNodeIds));
            List<Task<bool>> tasks = new();
            foreach (var nodeRequest in request.Nodes)
            {
                PlaybookNode node = new()
                {
                    NodeId = string.IsNullOrEmpty(nodeRequest.Id) ? UuidFactory.GetUuid() : nodeRequest.Id,
                    NodeName = nodeRequest.NodeName,
                    ParentNodeId = nodeRequest.ParentNodeId,
                    NodeType = nodeRequest.NodeType,
                    ActionType = nodeRequest.ActionType,
                    Delay = nodeRequest.Delay,
                    DelayType = nodeRequest.DelayType,
                    Order = nodeRequest.Order,
                    PlaybookId = playbookId,
                    Version = nodeRequest.Version                
                };
                node.IsNew = string.IsNullOrEmpty(nodeRequest.Id);
                RequestToQuery(tasks, node.NodeId, node.NodeId, nodeRequest.Query);
                tasks.Add(playbookNodeFactory.SaveAsync(node));
            }

            await Task.WhenAll(tasks.ToArray());
            if (tasks.Any(task => task.IsFaulted))
                InstrumentationContext.Current.Error("AddUpdateNode", "Problem with adding or updating node.");
        }
        else
        {
            InstrumentationContext.Current.Error("AddUpdateNode",
                "For this operation worflow should be isn't enabled and isn't deleted.");
        }

        return true;
    }

    private async Task<IEnumerable<Task>> CreateTaskForRemoveExtraNodes(IEnumerable<string> removeNodeIds)
    {
        var nodeFactory = new PlaybookNodeFactory();
        var queryFactory = new QueryFactory();
        var conditionFactory = new QueryConditionFactory();
        IEnumerable<Task> tasks = new List<Task>();
        var nodes = await nodeFactory.FindNodesByNodeIdsdAsync(removeNodeIds);
        var queries = await queryFactory.FindQueriesByNodeIds(removeNodeIds);
        var conditions = await conditionFactory.FindQueryConditionsByQueryIds(queries.Select(queriy => queriy.QueryId));
        nodes.AsParallel().ForAll(node => tasks.Append(nodeFactory.DeleteAsync(node)));
        queries.AsParallel().ForAll(query => tasks.Append(queryFactory.DeleteAsync(query)));
        conditions.AsParallel().ForAll(condition => tasks.Append(conditionFactory.DeleteAsync(condition)));
        return tasks;
    }

    private string RequestToQuery(List<Task<bool>> tasks, string nodeId, string parentId, Engine.Query queryRequest)
    {
        var queryFactory = new QueryFactory();
        var queryConditionFactory = new QueryConditionFactory();
        Query query = new()
        {
            QueryId = string.IsNullOrEmpty(queryRequest.QueryId) ? UuidFactory.GetUuid() : queryRequest.QueryId,
            ParentId = parentId,
            NodeId = nodeId,
            IsAnd = queryRequest.IsAnd,
            IsNew = string.IsNullOrEmpty(queryRequest.QueryId)
        };
        foreach (var condition in queryRequest.Conditions)
        {
            QueryCondition queryCondition = new()
            {
                ConditionId = string.IsNullOrEmpty(condition.ConditionId)
                    ? UuidFactory.GetUuid()
                    : condition.ConditionId,
                Field = condition.Field,
                Value = condition.Value,
                Operator = condition.Operator,
                QueryId = query.QueryId,
                NestedQueryId = condition.NestedConditions is null
                    ? ""
                    : RequestToQuery(tasks, nodeId, query.QueryId, condition.NestedConditions),
                IsNew = string.IsNullOrEmpty(queryRequest.QueryId)
            };
            tasks.Add(queryConditionFactory.SaveAsync(queryCondition));
        }

        tasks.Add(queryFactory.SaveAsync(query));
        return query.QueryId;
    }

    // private Query ParseQuery(QueryRequest queryRequest)
    // {
    //     var query = new Query
    //     {
    //         IsAnd = queryRequest.IsAnd,
    //         Conditions = new List<QueryCondition>()
    //     };
    //
    //     foreach (var conditionRequest in queryRequest.Conditions)
    //     {
    //         var condition = new QueryCondition
    //         {
    //             Field = conditionRequest.Field,
    //             Operator = conditionRequest.Operator,
    //             Value = conditionRequest.Value
    //         };
    //
    //         // If there are nested conditions, recursively parse them
    //         if (conditionRequest.NestedConditions != null)
    //         {
    //             condition.NestedQuery = ParseQuery(conditionRequest.NestedConditions);
    //         }
    //
    //         query.Conditions.Add(condition);
    //     }
    //
    //     return query;
    // }
    public async Task<WorkFlow> GetPlaybookNodes(string playbookid)
    {
        var playbookNodeFactory = new PlaybookNodeFactory();

        var nodes = await playbookNodeFactory.FindNodesByPlaybookIdAsync(playbookid);
        List<Node> listNodes = new();

        foreach (var node in nodes)
        {
            Node responseNode = new()
            {
                Id = node.NodeId,
                NodeName = node.NodeName,
                ParentNodeId = node.ParentNodeId,
                NodeType = node.NodeType,
                ActionType = node.ActionType,
                Delay = node.Delay,
                DelayType = node.DelayType,
                Order = node.Order,
                PlaybookId = node.PlaybookId,
                Version = node.Version,
                Query = await FindQueriesByParentId(node.NodeId)
            };
            listNodes.Add(responseNode);
        }

        WorkFlow workFlow = new()
        {
            PlaybookId = playbookid,
            Id = UuidFactory.GetUuid(),
            Nodes = listNodes
        };

        return workFlow;
    }

    private async Task<Engine.Query> FindQueriesByParentId(string parentId)
    {
        var queryFactory = new QueryFactory();

        var query = await queryFactory.FindQueryByParentId(parentId);
        Engine.Query queryResponse = new()
        {
            QueryId = query.QueryId,
            IsAnd = query.IsAnd,
            Conditions = await FindConditions(query.QueryId)
        };

        return queryResponse;
    }

    private async Task<List<Engine.QueryCondition>> FindConditions(string queryId)
    {
        var conditionFactory = new QueryConditionFactory();
        var conditions = await conditionFactory.FindQueryConditionsByQueryId(queryId);

        List<Engine.QueryCondition> result = new();
        foreach (var condition in conditions)
            result.Add(new Engine.QueryCondition
            {
                ConditionId = condition.ConditionId,
                Field = condition.Field,
                Operator = condition.Operator,
                Value = condition.Value,
                NestedConditions = await FindQueriesByParentId(condition.QueryId)
            });

        return result;
    }
}