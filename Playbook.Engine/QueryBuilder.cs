using Amazon.Util;
using MongoDB.Bson;
using MongoDB.Driver;

using System.Net.NetworkInformation;

namespace Playbook.Engine
{
    public class QueryBuilder
    {
        public QueryBuilder()
        {
        }

        public FilterDefinition<BsonDocument> BuildFilters(Query query)
        {
            var filters = new List<FilterDefinition<BsonDocument>>();
            
            foreach (var condition in query.Conditions)
            {
                if (condition.NestedConditions != null)
                {
                    var tpFilters = new List<FilterDefinition<BsonDocument>>();
                    tpFilters.Add(CreateFilter(condition));
                    tpFilters.Add(BuildFilters(condition.NestedConditions));
                    filters.Add(Builders<BsonDocument>.Filter.And(tpFilters));
                }
                else
                {
                    var filter = CreateFilter(condition);
                    filters.Add(filter);
                }
            }
            return query.IsAnd == true
                ? Builders<BsonDocument>.Filter.And(filters)
                : Builders<BsonDocument>.Filter.Or(filters);
        }

        public FilterDefinition<BsonDocument> CreateFilter(QueryCondition condition)
        {
            switch (condition.Operator)
            {
                case "==":
                    return Builders<BsonDocument>.Filter.Eq(condition.Field, condition.Value);

                case "!=":
                    return Builders<BsonDocument>.Filter.Ne(condition.Field, condition.Value);

                case "<":
                    return Builders<BsonDocument>.Filter.Lt(condition.Field, condition.Value);

                case ">":
                    return Builders<BsonDocument>.Filter.Gt(condition.Field, condition.Value);

                case "<=":
                    return Builders<BsonDocument>.Filter.Lte(condition.Field, condition.Value);

                case ">=":
                    return Builders<BsonDocument>.Filter.Gte(condition.Field, condition.Value);

                case "TRUE":
                    return Builders<BsonDocument>.Filter.Eq(condition.Field, condition.Value);

                case "FALSE":
                    return Builders<BsonDocument>.Filter.Eq(condition.Field, condition.Value);

                case "CONTAINS":
                    return Builders<BsonDocument>.Filter.Regex(condition.Field, new BsonRegularExpression(condition.Value, "i"));

                case "DOES_NOT_CONTAIN":
                    return Builders<BsonDocument>.Filter.Not(Builders<BsonDocument>.Filter.Regex(condition.Field, new BsonRegularExpression(condition.Value, "i")));
                case "IS_BLANK":
                    return Builders<BsonDocument>.Filter.Eq(condition.Field, BsonNull.Value);
                case "IS_PRESENT":
                    return Builders<BsonDocument>.Filter.Ne(condition.Field, BsonNull.Value);
                case "BEFORE":
                    return Builders<BsonDocument>.Filter.Lt(condition.Field, condition.Value);
                case "AFTER":
                    return Builders<BsonDocument>.Filter.Gt(condition.Field, condition.Value);
                case "ON_OR_BEFORE":
                    return Builders<BsonDocument>.Filter.Lte(condition.Field, condition.Value);
                case "ON_OR_AFTER":
                    return Builders<BsonDocument>.Filter.Gte(condition.Field, condition.Value);
                case "WITHIN_NEXT":
                    return Builders<BsonDocument>.Filter.And(
                        Builders<BsonDocument>.Filter.Gte(condition.Field, DateTime.Now),
                        Builders<BsonDocument>.Filter.Lte(condition.Field, DateTime.Now.AddDays(Convert.ToInt32(condition.Value)))
                    );
                case "NOT_WITHIN_NEXT":
                    return Builders<BsonDocument>.Filter.Or(
                        Builders<BsonDocument>.Filter.Lt(condition.Field, DateTime.Now),
                        Builders<BsonDocument>.Filter.Gt(condition.Field, DateTime.Now.AddDays(Convert.ToInt32(condition.Value)))
                    );
                case "WITHIN_PAST":
                    return Builders<BsonDocument>.Filter.And(
                        Builders<BsonDocument>.Filter.Lte(condition.Field, DateTime.Now),
                        Builders<BsonDocument>.Filter.Gte(condition.Field, DateTime.Now.AddDays(-Convert.ToInt32(condition.Value)))
                    );
                case "NOT_WITHIN_PAST":
                    return Builders<BsonDocument>.Filter.Or(
                        Builders<BsonDocument>.Filter.Gt(condition.Field, DateTime.Now),
                        Builders<BsonDocument>.Filter.Lt(condition.Field, DateTime.Now.AddDays(-Convert.ToInt32(condition.Value)))
                    );
                case "TODAY":
                    var today = DateTime.Now.Date;
                    return Builders<BsonDocument>.Filter.And(
                        Builders<BsonDocument>.Filter.Gte(condition.Field, today),
                        Builders<BsonDocument>.Filter.Lt(condition.Field, today.AddDays(1))
                    );
                default:
                    throw new ArgumentException($"Unsupported operator: {condition.Operator}");

            }
        }

    }
}