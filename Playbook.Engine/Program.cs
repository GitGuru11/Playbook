using Playbook.Engine;

namespace Playbook.Engine
{

    using MongoDB.Bson;
    using MongoDB.Driver;
}

public class Program
{
    public static void Main()
    {
        var sampleResponse = new WorkFlow
        {
            PlaybookId = "1",
            Nodes = new List<Node>
                {
                    new Node
                    {
                        PlaybookId = "1",
                        Id = "1",
                        NodeName = "Entry Node",
                        NodeType = "EntryNode",
                        ParentNodeId = null, // Root node
                        Query = new Query
                        {
                            IsAnd = true,
                            Conditions = new List<QueryCondition>
                            {
                                new QueryCondition
                                {
                                    Field = "IsDeleted",
                                    Operator = "==",
                                    Value = "False",
                                    NestedConditions = null
                                },
                                new QueryCondition
                                {
                                    Field = "LeadGrade",
                                    Operator = "==",
                                    Value = "N/A",
                                    NestedConditions = new Query
                                    {
                                        IsAnd = false,
                                        Conditions = new List<QueryCondition>
                                        {
                                            new QueryCondition
                                            {
                                                Field = "Company",
                                                Operator = "==",
                                                Value = "App Golf",
                                                NestedConditions = null
                                            },
                                            new QueryCondition
                                            {
                                                Field = "Email",
                                                Operator = "==",
                                                Value = "phantonminds@hotmail.com",
                                                NestedConditions = null
                                            }                                         
                                        }
                                    }
                                }
                            }
                        }
                    },
                    new Node
                    {
                        PlaybookId = "1",
                        Id = "2",
                        NodeName = "Branch Node 1",
                        NodeType = "BranchNode",
                        ParentNodeId = "1", // Child of Entry Node
                        Query = new Query
                        {
                            IsAnd = true,
                            Conditions = new List<QueryCondition>
                            {
                                new QueryCondition
                                {
                                    Field = "City",
                                    Operator = "==",
                                    Value = "N/A",
                                    NestedConditions = null
                                }
                            }
                        }
                    },
                    new Node
                    {
                        PlaybookId = "1",
                        Id = "3",
                        NodeName = "Action Node 1",
                        NodeType = "ActionNode",
                        ParentNodeId = "2", // Child of Branch Node 1
                        Query = null // Action nodes may not always have a query
                    },
                    new Node
                    {
                        PlaybookId = "1",
                        Id = "4",
                        NodeName = "Branch Node 2",
                        NodeType = "BranchNode",
                        ParentNodeId = "1", // Another child of Entry Node
                        Query = new Query
                        {
                            IsAnd = false,
                            Conditions = new List<QueryCondition>
                            {
                                new QueryCondition
                                {
                                    Field = "Region",
                                    Operator = "==",
                                    Value = "Americas",
                                    NestedConditions = null
                                }
                            }
                        }
                    },
                    new Node
                    {
                        PlaybookId = "1",
                        Id = "5",
                        NodeName = "Action Node 2",
                        NodeType = "ActionNode",
                        ParentNodeId = "4", // Child of Branch Node 2
                        Query = null
                    }
                }
        };

        var sampleResponse2 = new WorkFlow
        {
            PlaybookId = "1",
            Nodes = new List<Node>
        {
            new Node
            {
                PlaybookId = "1",
                Id = "1",
                NodeName = "Entry Node",
                NodeType = "EntryNode",
                ParentNodeId = null, // Root node
                Query = new Query
                {
                    IsAnd = true,
                    Conditions = new List<QueryCondition>
                    {
                        new QueryCondition
                        {
                            Field = "IsDeleted",
                            Operator = "==",
                            Value = "False",
                            NestedConditions = null
                        },
                        new QueryCondition
                        {
                            Field = "LeadGrade",
                            Operator = "==",
                            Value = "N/A",
                            NestedConditions = new Query
                            {
                                IsAnd = false,
                                Conditions = new List<QueryCondition>
                                {
                                    new QueryCondition
                                    {
                                        Field = "Email",
                                        Operator = "==",
                                        Value = "morris4hire@gmail.com",
                                        NestedConditions = null
                                    },
                                    new QueryCondition
                                    {
                                        Field = "Company",
                                        Operator = "==",
                                        Value = "App Golf",
                                        NestedConditions = null
                                    }
                                }
                            }
                        }
                    }
                }
            },
            new Node
            {
                PlaybookId = "1",
                Id = "2",
                NodeName = "Branch Node 1",
                NodeType = "BranchNode",
                ParentNodeId = "1", // Child of Entry Node
                Query = new Query
                {
                    IsAnd = true,
                    Conditions = new List<QueryCondition>
                    {
                        new QueryCondition
                        {
                            Field = "City",
                            Operator = "==",
                            Value = "N/A",
                            NestedConditions = null
                        }
                    }
                }
            },
            new Node
            {
                PlaybookId = "1",
                Id = "3",
                NodeName = "Action Node 1",
                NodeType = "ActionNode",
                ParentNodeId = "2", // Child of Branch Node 1
                Query = null // Action nodes may not always have a query
            },
            new Node
            {
                PlaybookId = "1",
                Id = "4",
                NodeName = "Branch Node 2",
                NodeType = "BranchNode",
                ParentNodeId = "1", // Another child of Entry Node
                Query = new Query
                {
                    IsAnd = false,
                    Conditions = new List<QueryCondition>
                    {
                        new QueryCondition
                        {
                            Field = "FirstName",
                            Operator = "==",
                            Value = "Jamie",
                            NestedConditions = null
                        }
                    }
                }
            },
            new Node
            {
                PlaybookId = "1",
                Id = "5",
                NodeName = "Action Node 2",
                NodeType = "ActionNode",
                ParentNodeId = "4", // Child of Branch Node 2
                Query = null
            },
            new Node
            {
                PlaybookId = "1",
                Id = "6",
                NodeName = "Branch Node 3",
                NodeType = "BranchNode",
                ParentNodeId = "1", // Child of Entry Node
                Query = new Query
                {
                    IsAnd = true,
                    Conditions = new List<QueryCondition>
                    {
                        new QueryCondition
                        {
                            Field = "City",
                            Operator = "==",
                            Value = "N/A",
                            NestedConditions = null
                        }
                    }
                }
            },
            new Node
            {
                PlaybookId = "1",
                Id = "7",
                NodeName = "Action Node 3",
                NodeType = "ActionNode",
                ParentNodeId = "6", // Child of Branch Node 2
                Query = null
            },
            new Node
            {
                PlaybookId = "1",
                Id = "8",
                NodeName = "Branch Node 4",
                NodeType = "BranchNode",
                ParentNodeId = "6", // Child of Entry Node
                Query = new Query
                {
                    IsAnd = true,
                    Conditions = new List<QueryCondition>
                    {
                        new QueryCondition
                        {
                            Field = "City",
                            Operator = "==",
                            Value = "N/A1",
                            NestedConditions = null
                        }
                    }
                }
            },
            new Node
            {
                PlaybookId = "1",
                Id = "9",
                NodeName = "Action Node 4",
                NodeType = "ActionNode",
                ParentNodeId = "8", // Child of Branch Node 2
                Query = null
            },
            new Node
            {
                PlaybookId = "1",
                Id = "10",
                NodeName = "Branch Node 5",
                NodeType = "BranchNode",
                ParentNodeId = "6", // Child of Entry Node
                Query = new Query
                {
                    IsAnd = true,
                    Conditions = new List<QueryCondition>
                    {
                        new QueryCondition
                        {
                            Field = "City",
                            Operator = "==",
                            Value = "N/A",
                            NestedConditions = null
                        }
                    }
                }
            },
            new Node
            {
                PlaybookId = "1",
                Id = "11",
                NodeName = "Branch Node 6",
                NodeType = "BranchNode",
                ParentNodeId = "10", // Child of Entry Node
                Query = new Query
                {
                    IsAnd = true,
                    Conditions = new List<QueryCondition>
                    {
                        new QueryCondition
                        {
                            Field = "LastName",
                            Operator = "==",
                            Value = "Morris",
                            NestedConditions = null
                        }
                    }
                }
            },
            new Node
            {
                PlaybookId = "1",
                Id = "12",
                NodeName = "Action Node 5",
                NodeType = "ActionNode",
                ParentNodeId = "11", // Child of Branch Node 2
                Query = null
            },
        }
        };
    }
}
/*public class Program
{
    public static void Main()
    {
        var queryParser = new QueryParser();
        var queryBuilder = new QueryBuilder();
        var actionFactory = new ActionFactory();
        var mongoClient = new MongoClient("your_connection_string");
        var database = mongoClient.GetDatabase("your_database");
        var collection = database.GetCollection<BsonDocument>("your_collection");
        var dataService = new DataService(collection, queryBuilder);

        var executionEngine = new ExecutionEngine(queryParser, actionFactory, dataService);

        // Example PlaybookDefinition
        var playbook = new PlaybookDefinition
        {
            Id = "1",
            Name = "Sample Playbook",
            Version = "1.0",
            Branches = new List<PlaybookBranch>
            {
                new PlaybookBranch
                {
                    Nodes = new List<PlaybookNode>
                    {
                        new PlaybookNode
                        {
                            Type = NodeType.ActionNode,
                            // Example nested conditions
                            Children = new List<PlaybookNode>()
                        }
                    }
                }
            }
        };

        executionEngine.ExecutePlaybook(playbook);
    }
}
}*/