using System.Data;
using Playbook.Data.ClickHouse;
using System.Data.Common;
using ClickHouse.Client.ADO.Parameters;
using Playbook.Common.Tenant;

namespace Playbook.Bussiness.Model
{
    /// <summary>
    /// Factory base class for PlaybookRevisionFactory.
    /// </summary>
    public partial class PlaybookRevisionFactoryBase : DataObjectFactory<PlaybookRevision>
    {
        //Fields
        public const string FIELD_NAME_ID = "Id";

        public const string FIELD_NAME_PLAYBOOK_ID = "PlaybookId";

        public const string FIELD_NAME_NUMBER = "Number";

        public const string FIELD_NAME_LABEL = "Label";
        
        public const string FIELD_NAME_ISDELETED = "IsDeleted";

        public const string FIELD_NAME_CREATEDAT = "CreatedAt";

        public const string FIELD_NAME_UPDATEDAT = "UpdatedAt";
        
        // Parameters
        public const string PARAMETER_NAME_ID = "@ID";

        public const string PARAMETER_NAME_PLAYBOOK_ID = "@PLAYBOOKID";
        
        public const string PARAMETER_NAME_NUMBER = "@NUMBER";

        public const string PARAMETER_NAME_LABEL = "@LABEL";
        
        public const string PARAMETER_NAME_ISDELETED = "@ISDELETED";

        public const string PARAMETER_NAME_CREATEDAT = "@CREATEDAT";

        public const string PARAMETER_NAME_UPDATEDAT = "@UPDATEDAT";

        /// <summary>
        /// Gets the table name.
        /// </summary>
        public override string TableName => "PlaybookRevision ";

        /// <summary>
        /// Gets the primar key name.
        /// </summary>
        public override string PrimaryKeyName => "Id";

        /// <summary>
        /// Gets the select all statement.
        /// </summary>
        protected override string SelectAllStatement => $"SELECT Id, PlaybookId, Number, Label, IsDeleted, CreatedAt, UpdatedAt FROM {TenantContext.Current.TenantName}.PlaybookRevision";
        
        /// <summary>
        /// Gets the insert statement.
        /// </summary>
        protected override string InsertStatement => 
            $"INSERT INTO {TenantContext.Current.TenantName}.PlaybookRevision (Id, PlaybookId, Number, Label, IsDeleted, CreatedAt, UpdatedAt) VALUES (@ID, @PLAYBOOKID, @NUMBER, @LABEL, @ISDELETED, @CREATEDAT, @UPDATEDAT)";
        
        /// <summary>
        /// Gets the update statement.
        /// </summary>
        protected override string UpdateStatement => 
            $"ALTER TABLE {TenantContext.Current.TenantName}.PlaybookRevision UPDATE PlaybookId = @PLAYBOOKID, Number = @NUMBER, Label = @LABEL, IsDeleted = @ISDELETED, CreatedAt = @CREATEDAT, UpdatedAt = @UPDATEDAT WHERE Id = @ID";

        /// <summary>
        /// Gets the delete statment.
        /// </summary>
        protected override string DeleteStatement => $"ALTER TABLE {TenantContext.Current.TenantName}.Playbook FROM IsDeleted = true WHERE Id = @ID";
        
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
        /// <param name="valPlaybookId">The valPlaybookId.</param>
        public static void AddPlaybookIdParameter(DatabaseHandler dataHandler, string valPlaybookId)
        {
            var param = new ClickHouseDbParameter
            {
                DbType = DbType.String,
                Direction = ParameterDirection.Input,
                Size = 36,
                Value = valPlaybookId,
                ParameterName = "PLAYBOOKID"
            };
            dataHandler.Command.Parameters.Add(param);
        }

        /// <summary>
        /// Add sql parameters.
        /// </summary>
        /// <param name="dataHandler">The database handler.</param>
        /// <param name="valNumber">The valNumber.</param>
        public static void AddNumberParameter(DatabaseHandler dataHandler, int valNumber)
        {
            var param = new ClickHouseDbParameter
            {
                DbType = DbType.UInt32,
                Direction = ParameterDirection.Input,
                Size = 32,
                Value = valNumber,
                ParameterName = "NUMBER"
            };
            dataHandler.Command.Parameters.Add(param);
        }
        
        /// <summary>
        /// Add sql parameters.
        /// </summary>
        /// <param name="dataHandler">The database handler.</param>
        /// <param name="valLabel">The valLabel.</param>
        public static void AddLabelParameter(DatabaseHandler dataHandler, string valLabel)
        {
            var param = new ClickHouseDbParameter();
            param.DbType = DbType.String;
            param.Direction = ParameterDirection.Input;
            param.Size = 1024;
            param.Value = valLabel;
            param.ParameterName = "LABEL";
            dataHandler.Command.Parameters.Add(param);
        }

        /// <summary>
        /// Add sql parameters.
        /// </summary>
        /// <param name="dataHandler">The database handler.</param>
        /// <param name="valIsDeleted">The valIsDeleted .</param>
        public static void AddIsDeletedParameter(DatabaseHandler dataHandler, bool valIsDeleted)
        {
            var param = new ClickHouseDbParameter();
            param.DbType = DbType.Boolean;
            param.Direction = ParameterDirection.Input;
            param.Size = 4;
            param.Value = valIsDeleted;
            param.ParameterName = "ISDELETED";
            dataHandler.Command.Parameters.Add(param);
        }

        
        /// <summary>
        /// Add sql parameters.
        /// </summary>
        /// <param name="dataHandler">The database handler.</param>
        /// <param name="valCreatedAt">The valCreatedAt .</param>
        public static void AddCreatedAtParameter(DatabaseHandler dataHandler, DateTime valCreatedAt)
        {
            var param = new ClickHouseDbParameter();
            param.DbType = DbType.DateTime;
            param.Direction = ParameterDirection.Input;
            param.Size = 8;
            param.Value = valCreatedAt;
            param.ParameterName = "CREATEDAT";
            dataHandler.Command.Parameters.Add(param);
        }

        /// <summary>
        /// Add sql parameters.
        /// </summary>
        /// <param name="dataHandler">The database handler.</param>
        /// <param name="valUpdatedAt">The valUpdatedAt .</param>
        public static void AddUpdatedAtParameter(DatabaseHandler dataHandler, DateTime valUpdatedAt)
        {
            var param = new ClickHouseDbParameter();
            param.DbType = DbType.DateTime;
            param.Direction = ParameterDirection.Input;
            param.Size = 8;
            param.Value = valUpdatedAt;
            param.ParameterName = "UPDATEDAT";
            dataHandler.Command.Parameters.Add(param);
        }
        
        /// <summary>
        /// Add sql parameters.
        /// </summary>
        /// <param name="dataHandler">The database handler.</param>
        protected override void AppendSqlParameters(DatabaseHandler databaseHandler, DatabaseObject databaseObject, DataMode mode)
        {
            var entity = databaseObject as PlaybookRevision;
            if (mode == DataMode.Insert || mode == DataMode.Update)
            {
                AddIdParameter(databaseHandler, entity.Id);
                AddPlaybookIdParameter(databaseHandler, entity.PlaybookId);
                AddNumberParameter(databaseHandler, entity.Number);
                AddIsDeletedParameter(databaseHandler, entity.IsDeleted);
                AddCreatedAtParameter(databaseHandler, entity.CreatedAt);
                AddUpdatedAtParameter(databaseHandler, entity.UpdatedAt);
            }
            else
            {
                AddIdParameter(databaseHandler, entity.Id);
            }
        }
        
        /// <summary>
        /// The create object from the data reader provided.
        /// </summary>
        /// <param name="reader">
        /// The reader.
        /// </param>
        /// <param name="populateObject">
        /// The populate object.
        /// </param>
        /// <returns>
        /// A data Object
        /// </returns>
        protected override PlaybookRevision CreateObject(DbDataReader reader, bool populateObject)
        {
            var entity = new PlaybookRevision();
            entity.Id = reader.GetFieldValue<string>(reader.GetOrdinal("Id"));
            entity.PlaybookId = reader.GetFieldValue<string>(reader.GetOrdinal("Name"));
            entity.Number = reader.GetFieldValue<int>(reader.GetOrdinal("Number"));
            entity.IsDeleted = reader.GetFieldValue<bool>(reader.GetOrdinal("IsDeleted"));
            entity.CreatedAt = reader.GetFieldValue<DateTime>(reader.GetOrdinal("CreatedAt"));
            entity.UpdatedAt = reader.GetFieldValue<DateTime>(reader.GetOrdinal("UpdatedAt"));
            entity.IsNew = false;
            return entity;
        }

        /// <summary>
        /// The create object from the data reader provided.
        /// </summary>
        /// <param name="reader">
        /// The reader.
        /// </param>
        /// <param name="populateObject">
        /// The populate object.
        /// </param>
        /// <returns>
        /// A data Object
        /// </returns>
        protected override async Task<PlaybookRevision> CreateObjectAsync(DbDataReader reader, bool populateObject)
        {
            var entity = new PlaybookRevision();
            entity.Id = (await reader.GetFieldValueAsync<Guid>(reader.GetOrdinal("Id"))).ToString();
            entity.PlaybookId = (await reader.GetFieldValueAsync<Guid>(reader.GetOrdinal("PlaybookId"))).ToString();
            entity.Number = await reader.GetFieldValueAsync<int>(reader.GetOrdinal("Number"));
            entity.IsDeleted = await reader.GetFieldValueAsync<bool>(reader.GetOrdinal("IsDeleted"));
            entity.CreatedAt = await reader.GetFieldValueAsync<DateTime>(reader.GetOrdinal("CreatedAt"));
            entity.UpdatedAt = await reader.GetFieldValueAsync<DateTime>(reader.GetOrdinal("UpdatedAt"));
            entity.IsNew = false;
            return entity;
        }
    }
}

