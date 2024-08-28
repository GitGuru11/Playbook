using MongoDB.Bson;
using MongoDB.Driver;
using Playbook.Data.Mongo;
using Playbook.Engine;
using Playbook.Business.Model;
using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Threading.Tasks;
using Playbook.Bussiness.Model;

public class WorkflowEngine
{
    private readonly IMongoDatabase _database;
    private QueryBuilder _queryBuilder;
    private IMongoCollection<BsonDocument> _collection;
    private List<System.Action> actions;
    public WorkflowEngine(IMongoDatabase database)
    {
        _database = database;
        _queryBuilder = new QueryBuilder();
    }
    public WorkflowEngine(DBInformation configure)
    {
        var db = new MongoHandler(configure.Mongo, $"mongodb://jack:jack123@mongo-fmapp-qa-{configure.Shard}.aviso.com:27016/?tls=true&authSource=admin");
        _collection = db.Database.GetCollection<BsonDocument>("leads_data");
        _queryBuilder = new QueryBuilder();
        actions = new List<System.Action>();
    }
    public void ExecuteWorkflow(WorkFlow workflow)
    {
        var entryNode = workflow.Nodes.FirstOrDefault(n => n.NodeType == "EntryNode");
        if (entryNode == null)
            throw new InvalidOperationException("No entry node found in the workflow.");

        ExecuteNode(entryNode, workflow.Nodes, new List<FilterDefinition<BsonDocument>>());
    }

    private void ExecuteNode(Node node, List<Node> allNodes, List<FilterDefinition<BsonDocument>> accumulatedFilters)
    {
        // Build query filters from the current node's query conditions
        if(node.Query != null)
        {
            var currentFilter = _queryBuilder.BuildFilters(node.Query);
            accumulatedFilters.Add(currentFilter);
        }

        // If the node is an ActionNode, execute the final action
        if (node.NodeType == "ActionNode")
        {
            var finalFilter = Builders<BsonDocument>.Filter.And(accumulatedFilters);
            actions.Add(() => PerformAction(node, finalFilter));
            Parallel.Invoke(actions.ToArray());
            return;
        }

        // Otherwise, find child nodes and execute them
        var childNodes = allNodes.Where(n => n.ParentNodeId == node.Id).OrderBy(n => n.Order).ToList();
        foreach (var childNode in childNodes)
        {
            ExecuteNode(childNode, allNodes, new List<FilterDefinition<BsonDocument>>(accumulatedFilters));
        }
    }


    private void PerformAction(Node actionNode, FilterDefinition<BsonDocument> filter)
    {
        // get the type of object and pass here
        // eg Leads, Accounts, Opportunites from playbook
        var results = _collection.Find(filter).Limit(5).ToList();

        // Execute the action based on the results
        // This can be sending emails, assigning tasks, etc.
        if (actionNode.ActionType == "email")
        {
            foreach (var result in results)
            {
                
            }
        }
    }
}