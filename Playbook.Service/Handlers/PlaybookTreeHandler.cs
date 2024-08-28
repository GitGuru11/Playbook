/*using Playbook.Bussiness.Model;
using Playbook.Service.Contracts;
//using Playbook.Service.Contracts.PlaybookTree;

namespace Playbook.Service.Handlers
{
    public class PlaybookTreeHandler
    {
        public async Task<bool> ProcessCreateUpdatePlaybookTreeRequestAsync(string playbookId, PlaybookNodeRequest request)
        {
            var playbookNodeFactory = new PlaybookNodeFactory();
            var playbookFactory = new PlaybookFactory();
            var playbookBranchFactory = new PlaybookBranchFactory();
            var queryFactory  = new QueryFactory();
            var queryValueFactory = new QueryValueFactory();
            var playbookDefinitionFactory = new PlaybookDefinitionFactory();
            
            var playbook = await playbookFactory.FindAsync(playbookId);

            if (playbook is not null)
            {
                foreach (var node in request.Nodes)
                {
                    var entryNode = new PlaybookNode()
                    {
                        Id = string.IsNullOrEmpty(node.Id) ? UuidFactory.GetUuid() : node.Id,
                        ParentId = playbookId,
                        NodeType = node.Type,
                        Order = node.Order,
                        Name = node.Name,
                        IsNew = string.IsNullOrEmpty(node.Id)
                    };
                    
                    foreach (var branchItem in node.Action)
                    {
                        foreach (var queryitem in branchItem.Query)
                        {
                            var branchQuery = new Query()
                            {
                                Id = UuidFactory.GetUuid(),
                                ParentId = entryNode.Id,
                                Operator = queryitem.Operator,
                                IsNew = true
                            };

                            var listQueryValue = queryitem.Values.Select(queryValueItem => new QueryValue()
                            {
                                Id = UuidFactory.GetUuid(),
                                FieldId = queryValueItem.FieldId,
                                ParentId = branchQuery.Id,
                                Value = queryValueItem.Value,
                                Operator = queryValueItem.Operator,
                                Duration = queryValueItem.Duration,
                                DurationType = queryValueItem.DurationType,
                                IsNew = true
                            });

                            foreach (var item in listQueryValue)
                            {
                                await queryValueFactory.SaveAsync(item);
                            }
                            
                            await queryFactory.SaveAsync(branchQuery);
                        }
                    }

                    var def = new PlaybookDefinition()
                    {
                        Id = UuidFactory.GetUuid(),
                        ParentId = entryNode.Id,
                        ObjectTypeId = node.Definition.TypeId,
                        IsActive = true,
                        VersionNumber = await playbookDefinitionFactory.LastVersionNumberByParentIdAsync(entryNode.Id),
                        IsNew = true
                    };

                    await playbookNodeFactory.SaveAsync(entryNode);
                    await playbookDefinitionFactory.SaveAsync(def);
                }
            }

            return true;
        }

        public async Task<List<Playbook.Bussiness.PlaybookTree.PlaybookNode>> ProcessGetPlaybookTreeRequestAsync(
            string playbookId)
        {
            var nodeFactory = new PlaybookNodeFactory();
            
            var nodeIds = await nodeFactory.FindNodesIDsByPlaybookIdAsync(playbookId);
            var listNodes = new List<Playbook.Bussiness.PlaybookTree.PlaybookNode>();
            foreach (var nodeId in nodeIds)
            {
                var playbookNode = await GetPlaybookNode(nodeId);
                if (playbookNode is not null)
                {
                    listNodes.Add(playbookNode);
                }
            }

            return listNodes;
        }

        private static async Task<Bussiness.PlaybookTree.PlaybookNode?> GetPlaybookNode(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var nodeFactory = new PlaybookNodeFactory();
                var queryValueFactory = new QueryValueFactory();
                var queryFactory = new QueryFactory();
                var definitionFactory = new PlaybookDefinitionFactory();

                var node = await nodeFactory.FindNodeByIdAsync(id);

                if (node is not null)
                {
                    var parentNode = await GetPlaybookNode(node.ParentId);

                    var playbookNode = new Playbook.Bussiness.PlaybookTree.PlaybookNode()
                    {
                        Id = node.Id,
                        NodeType = node.NodeType,
                        Order = node.Order,
                        Name = node.Name,
                        Parent = parentNode
                    };

                    var queries = await queryFactory.FindQueriesByParentId(node.Id);
                    foreach (var queryItem in queries)
                    {
                        var queryValues = await queryValueFactory.FindQueryValuesByParentId(queryItem.Id);

                        var query = new Playbook.Bussiness.PlaybookTree.Query()
                        {
                            Operator = queryItem.Operator,
                            Values = queryValues.Select(value => new Playbook.Bussiness.PlaybookTree.QueryValue()
                            {
                                FieldId = value.FieldId,
                                Value = value.Value,
                                Operator = value.Operator,
                                Duration = value.Duration,
                                DurationType = value.DurationType,
                            }).ToList()
                        };
                        playbookNode.Queries.Add(query);
                    }

                    var definition = await definitionFactory.LastVersionByParentIdAsync(node.Id);
                    if (definition is not null)
                    {
                        playbookNode.Definition = new Playbook.Bussiness.PlaybookTree.PlaybookDefinition()
                        {
                            Id = definition.Id,
                            ObjectTypeId = definition.ObjectTypeId
                        };
                    }

                    return playbookNode;
                }
            }

            return default;
        }
    }
}*/