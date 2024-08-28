using System;


namespace Playbook.Engine
{
    public class WorkFlow
    {
        public string Id { get; set; }
        public string PlaybookId { get; set; }
        public List<Node> Nodes { get; set; }
    }

    public class Node
    {
        public string Id { get; set; }
        public int Version { get; set; }
        public string NodeName { get; set; }
        public string NodeType { get; set; } // E.g., "EntryNode", "BranchNode", "DelayNode", "ActionNode"
        public string? ActionType { get; set; }
        public long Delay { get; set; }
        public string DelayType { get; set; }
        public int Order { get; set; }
        public string? ParentNodeId { get; set; } // Nullable, as root nodes won't have a parent
        public string PlaybookId { get; set; }
        public Query Query { get; set; }
    }

    public class Query
    {
        public string QueryId { get; set; }
        public bool IsAnd { get; set; } // true for AND, false for OR
        public List<QueryCondition> Conditions { get; set; } // List of conditions or nested queries
    }

    public class QueryCondition
    {
        public string ConditionId { get; set; }
        public string Field { get; set; } // The field to apply the condition on, e.g., "status", "amount"
        public string Operator { get; set; } // The operator to apply, e.g., "==", "!="
        public string Value { get; set; } // The value to compare against
        public Query? NestedConditions { get; set; } // For nested conditions
    }

}

