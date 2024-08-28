using System;
using System.IO;
using System.Text.Json;
using Playbook.Engine;
using Playbook.Common.Instrumentation;
using Playbook.Bussiness.Model;
using System.Xml.Linq;

namespace Playbook.Service.Handlers
{
    public class DBConfigurationHandler
    {

        public async Task<DBInformation> ProcessGetDBInformationRequestAsync(string Name)
        {
            var sampleResponse2 = new WorkFlow
            {
                PlaybookId = "1",
                Nodes = new List<Playbook.Engine.Node>
                {
            new Playbook.Engine.Node
            {
                PlaybookId = "1",
                Id = "1",
                NodeName = "Entry Node",
                NodeType = "EntryNode",
                ParentNodeId = null, // Root node
                Query = new Playbook.Engine.Query
                {
                    IsAnd = true,
                    Conditions = new List<Playbook.Engine.QueryCondition>
                    {
                        new Playbook.Engine.QueryCondition
                        {
                            Field = "IsDeleted",
                            Operator = "==",
                            Value = "False",
                            NestedConditions = null
                        },
                        new Playbook.Engine.QueryCondition
                        {
                            Field = "LeadGrade",
                            Operator = "==",
                            Value = "N/A",
                            NestedConditions = new Playbook.Engine.Query
                            {
                                IsAnd = false,
                                Conditions = new List<Playbook.Engine.QueryCondition>
                                {
                                    new Playbook.Engine.QueryCondition
                                    {
                                        Field = "Email",
                                        Operator = "==",
                                        Value = "morris4hire@gmail.com",
                                        NestedConditions = null
                                    },
                                    new Playbook.Engine.QueryCondition
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
            new Playbook.Engine.Node
            {
                PlaybookId = "1",
                Id = "2",
                NodeName = "Branch Node 1",
                NodeType = "BranchNode",
                ParentNodeId = "1", // Child of Entry Node
                Query = new Playbook.Engine.Query
                {
                    IsAnd = true,
                    Conditions = new List<Playbook.Engine.QueryCondition>
                    {
                        new Playbook.Engine.QueryCondition
                        {
                            Field = "City",
                            Operator = "==",
                            Value = "N/A",
                            NestedConditions = null
                        }
                    }
                }
            },
            new Playbook.Engine.Node
            {
                PlaybookId = "1",
                Id = "3",
                NodeName = "Action Node 1",
                NodeType = "ActionNode",
                ParentNodeId = "2", // Child of Branch Node 1
                Query = null // Action nodes may not always have a query
            },
            new Playbook.Engine.Node
            {
                PlaybookId = "1",
                Id = "4",
                NodeName = "Branch Node 2",
                NodeType = "BranchNode",
                ParentNodeId = "1", // Another child of Entry Node
                Query = new Playbook.Engine.Query
                {
                    IsAnd = false,
                    Conditions = new List<Playbook.Engine.QueryCondition>
                    {
                        new Playbook.Engine.QueryCondition
                        {
                            Field = "FirstName",
                            Operator = "==",
                            Value = "Jamie",
                            NestedConditions = null
                        }
                    }
                }
            },
            new Playbook.Engine.Node
            {
                PlaybookId = "1",
                Id = "5",
                NodeName = "Action Node 2",
                NodeType = "ActionNode",
                ParentNodeId = "4", // Child of Branch Node 2
                Query = null
            },
            new Playbook.Engine.Node
            {
                PlaybookId = "1",
                Id = "6",
                NodeName = "Branch Node 3",
                NodeType = "BranchNode",
                ParentNodeId = "1", // Child of Entry Node
                Query = new Playbook.Engine.Query
                {
                    IsAnd = true,
                    Conditions = new List<Playbook.Engine.QueryCondition>
                    {
                        new Playbook.Engine.QueryCondition
                        {
                            Field = "City",
                            Operator = "==",
                            Value = "N/A",
                            NestedConditions = null
                        }
                    }
                }
            },
            new Playbook.Engine.Node
            {
                PlaybookId = "1",
                Id = "7",
                NodeName = "Action Node 3",
                NodeType = "ActionNode",
                ParentNodeId = "6", // Child of Branch Node 2
                Query = null
            },
            new Playbook.Engine.Node
            {
                PlaybookId = "1",
                Id = "8",
                NodeName = "Branch Node 4",
                NodeType = "BranchNode",
                ParentNodeId = "6", // Child of Entry Node
                Query = new Playbook.Engine.Query
                {
                    IsAnd = true,
                    Conditions = new List<Playbook.Engine.QueryCondition>
                    {
                        new Playbook.Engine.QueryCondition
                        {
                            Field = "City",
                            Operator = "==",
                            Value = "N/A1",
                            NestedConditions = null
                        }
                    }
                }
            },
            new Playbook.Engine.Node
            {
                PlaybookId = "1",
                Id = "9",
                NodeName = "Action Node 4",
                NodeType = "ActionNode",
                ParentNodeId = "8", // Child of Branch Node 2
                Query = null
            },
            new Playbook.Engine.Node
            {
                PlaybookId = "1",
                Id = "10",
                NodeName = "Branch Node 5",
                NodeType = "BranchNode",
                ParentNodeId = "6", // Child of Entry Node
                Query = new Playbook.Engine.Query
                {
                    IsAnd = true,
                    Conditions = new List<Playbook.Engine.QueryCondition>
                    {
                        new Playbook.Engine.QueryCondition
                        {
                            Field = "City",
                            Operator = "==",
                            Value = "N/A",
                            NestedConditions = null
                        }
                    }
                }
            },
            new Playbook.Engine.Node
            {
                PlaybookId = "1",
                Id = "11",
                NodeName = "Branch Node 6",
                NodeType = "BranchNode",
                ParentNodeId = "10", // Child of Entry Node
                Query = new Playbook.Engine.Query
                {
                    IsAnd = true,
                    Conditions = new List<Playbook.Engine.QueryCondition>
                    {
                        new Playbook.Engine.QueryCondition
                        {
                            Field = "LastName",
                            Operator = "==",
                            Value = "Morris",
                            NestedConditions = null
                        }
                    }
                }
            },
            new Playbook.Engine.Node
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

            // Define the path to the JSON file
            string filePath = "../Playbook.Bussiness/Model/leads.json";

            // Check if the file exists
            var DBInfos = new List<DBInformation>();
            var ConnectionInfo =  new DBInformation();
            if (File.Exists(filePath))
            {
                // Read the existing JSON array from the file
                string existingJson = File.ReadAllText(filePath);

                // Deserialize the JSON array to a list of DB objects
                DBInfos = JsonSerializer.Deserialize<List<DBInformation>>(existingJson);

                // Find the Record by name and update it
                ConnectionInfo = DBInfos.Find(item => item.Name == Name);
                if (ConnectionInfo != null)
                {
                    new WorkflowEngine(ConnectionInfo).ExecuteWorkflow(sampleResponse2);
                }
                else
                {
                    InstrumentationContext.Current.Error("ProcessUpdateDBInformationRequestAsync",
                        "Record not found");
                }
            }
            else
            {
                // If the file doesn't exist, start with an empty list
                InstrumentationContext.Current.Error("ProcessUpdateDBInformationRequestAsync",
                        "File not exist");
            }

            return ConnectionInfo;
        }
        public async Task<bool> ProcessCreateDBInformationRequestAsync(DBInformation request)
        {

            // Define the path to the JSON file
            string filePath = "../Playbook.Bussiness/Model/leads.json";

            // Check if the file exists
            List<DBInformation> DBInfos;
            if (File.Exists(filePath))
            {
                // Read the existing JSON array from the file
                string existingJson = File.ReadAllText(filePath);

                // Deserialize the JSON array to a list of Lead objects
                DBInfos = JsonSerializer.Deserialize<List<DBInformation>>(existingJson);
            }
            else
            {
                // If the file doesn't exist, start with an empty list
                DBInfos = new List<DBInformation>();
            }

            DBInfos.Add(new DBInformation
            {
                Name = request.Name,
                ClickHouse = request.ClickHouse,
                Mongo = request.Mongo,
                Shard = request.Shard
            });

            // Serialize the list to a JSON array
            var options = new JsonSerializerOptions
            {
                WriteIndented = true // Optional: for pretty-printing
            };

            string jsonArrayString = JsonSerializer.Serialize(DBInfos, options);

            File.WriteAllText(filePath, jsonArrayString);

            return true;
        }

        public async Task<List<DBInformation>> ProcessUpdateDBInformationRequestAsync(string UpdatedName, DBInformation request)
        {

            // Define the path to the JSON file
            string filePath = "../Playbook.Bussiness/Model/leads.json";

            // Check if the file exists
            var DBInfos = new List<DBInformation>();
            if (File.Exists(filePath))
            {
                // Read the existing JSON array from the file
                string existingJson = File.ReadAllText(filePath);

                // Deserialize the JSON array to a list of DB objects
                DBInfos = JsonSerializer.Deserialize<List<DBInformation>>(existingJson);

                // Find the Record by name and update it
                var InfoToUpdate = DBInfos.Find(item => item.Name == UpdatedName);
                if (InfoToUpdate != null)
                {
                    // Update the desired fields
                    InfoToUpdate.ClickHouse = request.ClickHouse;
                    InfoToUpdate.Mongo = request.Mongo;
                    InfoToUpdate.Shard = request.Shard;
                }
                else
                {
                    InstrumentationContext.Current.Error("ProcessUpdateDBInformationRequestAsync",
                        "Record not found");
                }
            }
            else
            {
                // If the file doesn't exist, start with an empty list
                InstrumentationContext.Current.Error("ProcessUpdateDBInformationRequestAsync",
                        "File not exist");
            }

            // Serialize the updated list back to a JSON array
            var options = new JsonSerializerOptions
            {
                WriteIndented = true // Optional: for pretty-printing
            };

            string updatedJson = JsonSerializer.Serialize(DBInfos, options);

            File.WriteAllText(filePath, updatedJson);

            return DBInfos;
        }

        public async Task<List<DBInformation>> ProcessDeleteDBInformationRequestAsync(string UpdatedName)
        {

            // Define the path to the JSON file
            string filePath = "../Playbook.Bussiness/Model/leads.json";

            // Check if the file exists
            var DBInfos = new List<DBInformation>();
            if (File.Exists(filePath))
            {
                // Read the existing JSON array from the file
                string existingJson = File.ReadAllText(filePath);

                // Deserialize the JSON array to a list of DB objects
                DBInfos = JsonSerializer.Deserialize<List<DBInformation>>(existingJson);

                // Find the Record by name and delete it
                var InfoToDelete = DBInfos.Find(item => item.Name == UpdatedName);
                if (InfoToDelete != null)
                {
                    DBInfos.Remove(InfoToDelete);
                }
                else
                {
                    InstrumentationContext.Current.Error("ProcessUpdateDBInformationRequestAsync",
                        "Record not found");
                }
            }
            else
            {
                // If the file doesn't exist, start with an empty list
                InstrumentationContext.Current.Error("ProcessUpdateDBInformationRequestAsync",
                        "File not exist");
            }

            // Serialize the updated list back to a JSON array
            var options = new JsonSerializerOptions
            {
                WriteIndented = true // Optional: for pretty-printing
            };

            string updatedJson = JsonSerializer.Serialize(DBInfos, options);

            File.WriteAllText(filePath, updatedJson);

            return DBInfos;
        }
    }
}
