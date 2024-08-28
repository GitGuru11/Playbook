using System.Data;
using System.Data.Common;
using Playbook.Data.ClickHouse;
using ClickHouse.Client.ADO.Parameters;
using Playbook.Common.Tenant;

namespace Playbook.Bussiness.Model
{
    /// <summary>
    /// Factory base class for PlaybookObjectFactory.
    /// </summary>
    public partial class PlaybookObjectFactoryBase : DataObjectFactory<PlaybookObject>
    {
        // Fields
        public const string FIELD_NAME_ID = "Id";
        public const string FIELD_NAME_OBJECTTYPEID = "ObjectTypeId";
        public const string FIELD_NAME_NAME = "Name";
        public const string FIELD_NAME_DESCRIPTION = "Description";
        public const string FIELD_NAME_CATEGORY = "Category";
        public const string FIELD_NAME_ISDELETED = "IsDeleted";
        public const string FIELD_NAME_CREATEDAT = "CreatedAt";
        public const string FIELD_NAME_UPDATEDAT = "UpdatedAt";

        // Parameters
        public const string PARAMETER_NAME_ID = "@ID";
        public const string PARAMETER_NAME_OBJECTTYPEID = "@OBJECTTYPEID";
        public const string PARAMETER_NAME_NAME = "@NAME";
        public const string PARAMETER_NAME_DESCRIPTION = "@DESCRIPTION";
        public const string PARAMETER_NAME_CATEGORY = "@CATEGORY";
        public const string PARAMETER_NAME_ISDELETED = "@ISDELETED";
        public const string PARAMETER_NAME_CREATEDAT = "@CREATEDAT";
        public const string PARAMETER_NAME_UPDATEDAT = "@UPDATEDAT";

        /// <summary>
        /// Gets the table name.
        /// </summary>
        public override string TableName
        {
            get { return "PlaybookObject"; }
        }

        /// <summary>
        /// Gets the primary key name.
        /// </summary>
        public override string PrimaryKeyName
        {
            get { return "Id"; }
        }

        /// <summary>
        /// Gets the select all statement.
        /// </summary>
        protected override string SelectAllStatement =>
            $"SELECT Id, ObjectTypeId, Name, Description, Category, IsDeleted, CreatedAt, UpdatedAt FROM {TenantContext.Current.TenantName}.PlaybookObject";

        /// <summary>
        /// Gets the insert statement.
        /// </summary>
        protected override string InsertStatement =>
            $"INSERT INTO {TenantContext.Current.TenantName}.PlaybookObject (Id, ObjectTypeId, Name, Description, Category, IsDeleted, CreatedAt, UpdatedAt) VALUES (@ID, @OBJECTTYPEID, @NAME, @DESCRIPTION, @CATEGORY, @ISDELETED, @CREATEDAT, @UPDATEDAT)";

        /// <summary>
        /// Gets the update statement.
        /// </summary>
        protected override string UpdateStatement =>
            $"ALTER TABLE {TenantContext.Current.TenantName}.PlaybookObject UPDATE ObjectTypeId = @OBJECTTYPEID, Name = @NAME, Description = @DESCRIPTION, Category = @CATEGORY, IsDeleted = @ISDELETED, CreatedAt = @CREATEDAT, UpdatedAt = @UPDATEDAT WHERE Id = @ID";

        /// <summary>
        /// Gets the delete statement.
        /// </summary>
        protected override string DeleteStatement =>
            $"ALTER TABLE {TenantContext.Current.TenantName}.PlaybookObject UPDATE IsDeleted = true WHERE Id = @ID";

        /// <summary>
        /// Add sql parameters.
        /// </summary>
        /// <param name="dataHandler">The database handler.</param>
        /// <param name="valId">The valId.</param>
        public static void AddIdParameter(DatabaseHandler dataHandler, string valId)
        {
            var param = new ClickHouseDbParameter
            {
                DbType = DbType.String,
                Direction = ParameterDirection.Input,
                Size = 36,
                Value = valId,
                ParameterName = "ID"
            };
            dataHandler.Command.Parameters.Add(param);
        }

        /// <summary>
        /// Add sql parameters.
        /// </summary>
        /// <param name="dataHandler">The database handler.</param>
        /// <param name="valObjectTypeId">The valObjectTypeId.</param>
        public static void AddObjectTypeIdParameter(DatabaseHandler dataHandler, int valObjectTypeId)
        {
            var param = new ClickHouseDbParameter
            {
                DbType = DbType.Int32,
                Direction = ParameterDirection.Input,
                Size = 36,
                Value = valObjectTypeId,
                IsNullable = true,
                ParameterName = "OBJECTTYPEID"
            };
            dataHandler.Command.Parameters.Add(param);
        }

        /// <summary>
        /// Add sql parameters.
        /// </summary>
        /// <param name="dataHandler">The database handler.</param>
        /// <param name="valName">The valName.</param>
        public static void AddNameParameter(DatabaseHandler dataHandler, string valName)
        {
            var param = new ClickHouseDbParameter
            {
                DbType = DbType.String,
                Direction = ParameterDirection.Input,
                Size = 1024,
                Value = valName,
                ParameterName = "NAME"
            };
            dataHandler.Command.Parameters.Add(param);
        }

        /// <summary>
        /// Add sql parameters.
        /// </summary>
        /// <param name="dataHandler">The database handler.</param>
        /// <param name="valDescription">The valDescription.</param>
        public static void AddDescriptionParameter(DatabaseHandler dataHandler, string valDescription)
        {
            var param = new ClickHouseDbParameter
            {
                DbType = DbType.String,
                Direction = ParameterDirection.Input,
                Size = 1024,
                Value = valDescription,
                ParameterName = "DESCRIPTION"
            };
            dataHandler.Command.Parameters.Add(param);
        }


        /// <summary>
        /// Add sql parameters.
        /// </summary>
        /// <param name="dataHandler">The database handler.</param>
        /// <param name="valCategory">The valCategory.</param>
        public static void AddCategoryParameter(DatabaseHandler dataHandler, string valCategory)
        {
            var param = new ClickHouseDbParameter
            {
                DbType = DbType.String,
                Direction = ParameterDirection.Input,
                Size = 256,
                Value = valCategory,
                IsNullable = true,
                ParameterName = "CATEGORY"
            };
            dataHandler.Command.Parameters.Add(param);
        }

        /// <summary>
        /// Add sql parameters.
        /// </summary>
        /// <param name="dataHandler">The database handler.</param>
        /// <param name="valIsDeleted">The valIsDeleted.</param>
        public static void AddIsDeletedParameter(DatabaseHandler dataHandler, bool valIsDeleted)
        {
            var param = new ClickHouseDbParameter
            {
                DbType = DbType.Boolean,
                Direction = ParameterDirection.Input,
                Size = 4,
                Value = valIsDeleted,
                ParameterName = "ISDELETED"
            };
            dataHandler.Command.Parameters.Add(param);
        }

        /// <summary>
        /// Add sql parameters.
        /// </summary>
        /// <param name="dataHandler">The database handler.</param>
        /// <param name="valCreatedAt">The valCreatedAt.</param>
        public static void AddCreatedAtParameter(DatabaseHandler dataHandler, DateTime valCreatedAt)
        {
            var param = new ClickHouseDbParameter
            {
                DbType = DbType.DateTime,
                Direction = ParameterDirection.Input,
                Size = 8,
                Value = valCreatedAt,
                IsNullable = true,
                ParameterName = "CREATEDAT"
            };
            dataHandler.Command.Parameters.Add(param);
        }

        /// <summary>
        /// Add sql parameters.
        /// </summary>
        /// <param name="dataHandler">The database handler.</param>
        /// <param name="valUpdatedAt">The valUpdatedAt.</param>
        public static void AddUpdatedAtParameter(DatabaseHandler dataHandler, DateTime valUpdatedAt)
        {
            var param = new ClickHouseDbParameter
            {
                DbType = DbType.DateTime,
                Direction = ParameterDirection.Input,
                Size = 8,
                Value = valUpdatedAt,
                ParameterName = "UPDATEDAT"
            };
            dataHandler.Command.Parameters.Add(param);
        }

        /// <summary>
        /// Add sql parameters.
        /// </summary>
        /// <param name="dataHandler">The database handler.</param>
        protected override void AppendSqlParameters(DatabaseHandler databaseHandler, DatabaseObject databaseObject,
            DataMode mode)
        {
            var entity = databaseObject as PlaybookObject;
            AddIdParameter(databaseHandler, entity.Id);
            AddObjectTypeIdParameter(databaseHandler, entity.ObjectTypeId);
            AddNameParameter(databaseHandler, entity.Name);
            AddDescriptionParameter(databaseHandler, entity.Description);
            AddCategoryParameter(databaseHandler, entity.Category);
            AddIsDeletedParameter(databaseHandler, entity.IsDeleted);
            AddCreatedAtParameter(databaseHandler, entity.CreatedAt);
            AddUpdatedAtParameter(databaseHandler, entity.UpdatedAt);
        }

        /// <summary>
        /// Create object from the data reader provided.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="populateObject">The populate object.</param>
        /// <returns>A data object.</returns>
        protected override PlaybookObject CreateObject(DbDataReader reader, bool populateObject)
        {
            var entity = new PlaybookObject
            {
                Id = reader.GetFieldValue<string>(reader.GetOrdinal("Id")),
                ObjectTypeId = reader.GetFieldValue<int>(reader.GetOrdinal("ObjectTypeId")),
                Name = reader.GetFieldValue<string>(reader.GetOrdinal("Name")),
                Description = reader.GetFieldValue<string>(reader.GetOrdinal("Description")),
                Category = reader.GetFieldValue<string>(reader.GetOrdinal("Category")),
                IsDeleted = reader.GetFieldValue<bool>(reader.GetOrdinal("IsDeleted")),
                CreatedAt = reader.GetFieldValue<DateTime>(reader.GetOrdinal("CreatedAt")),
                UpdatedAt = reader.GetFieldValue<DateTime>(reader.GetOrdinal("UpdatedAt")),
                IsNew = false
            };

            return entity;
        }

        /// <summary>
        /// Create object asynchronously from the data reader provided.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="populateObject">The populate object.</param>
        /// <returns>A data object.</returns>
        protected override async Task<PlaybookObject> CreateObjectAsync(DbDataReader reader, bool populateObject)
        {
            // Make sure to check if the reader is async-capable
            var entity = new PlaybookObject
            {
                Id = (await reader.GetFieldValueAsync<Guid>(reader.GetOrdinal("Id"))).ToString(),
                ObjectTypeId = (await reader.GetFieldValueAsync<int>(reader.GetOrdinal("ObjectTypeId"))),
                Name = await reader.GetFieldValueAsync<string>(reader.GetOrdinal("Name")),
                Description = await reader.GetFieldValueAsync<string>(reader.GetOrdinal("Description")),
                Category = await reader.GetFieldValueAsync<string>(reader.GetOrdinal("Category")),
                IsDeleted = await reader.GetFieldValueAsync<bool>(reader.GetOrdinal("IsDeleted")),
                CreatedAt = await reader.GetFieldValueAsync<DateTime>(reader.GetOrdinal("CreatedAt")),
                UpdatedAt = await reader.GetFieldValueAsync<DateTime>(reader.GetOrdinal("UpdatedAt")),
                IsNew = false
            };

            return entity;
        }
    }
}
