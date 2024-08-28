using System;
using ClickHouse.Client.ADO;
using System.Data;
using System.Drawing;
using Playbook.Business.Model;
using Playbook.Common.Instrumentation;
using Playbook.Data.ClickHouse;
using System.Data.Common;
using ClickHouse.Client.ADO.Parameters;
using Playbook.Common.Tenant;

namespace Playbook.Bussiness.Model
{
    /// <summary>
    /// Factory base class for ObjecTypeFactory.
    /// </summary>
    public partial class ObjectTypeFactoryBase : DataObjectFactory<ObjectType>
    {

        public const string FIELD_NAME_OBJECTTYPEID = "ObjectTypeId";

        public const string FIELD_NAME_NAME = "Name";

        public const string FIELD_NAME_LABEL = "Label";

        public const string FIELD_NAME_ISDELETED = "IsDeleted";

        public const string FIELD_NAME_CREATEDAT = "CreatedAt";

        public const string FIELD_NAME_UPDATEDAT = "UpdatedAt";


        public const string PARAMETER_NAME_OBJECTTYPEID = "@OBJECTTYPEID";

        public const string PARAMETER_NAME_NAME = "@NAME";

        public const string PARAMETER_NAME_LABEL = "@LABEL";

        public const string PARAMETER_NAME_ISDELETED = "@ISDELTED";

        public const string PARAMETER_NAME_CREATEDAT = "@CREATEDAT";

        public const string PARAMETER_NAME_UPDATEDAT = "@UPDATEDAT";


        /// <summary>
        /// Gets the table name.
        /// </summary>
        public override string TableName
        {
            get
            {
                return "ObjectType ";
            }
        }

        /// <summary>
        /// Gets the primar key name.
        /// </summary>
        public override string PrimaryKeyName
        {
            get
            {
                return "ObjectTypeId";

            }
        }


        /// <summary>
        /// Gets the select all statement.
        /// </summary>
        protected override string SelectAllStatement
        {
            get
            {
                return string.Format("SELECT ObjectTypeId, Name, Label, IsDeleted, CreatedAt, UpdatedAt FROM {0}.ObjectType", TenantContext.Current.TenantName);
            }
        }

        /// <summary>
        /// Gets the insert statement.
        /// </summary>
        protected override string InsertStatement
        {
            get
            {
                return string.Format("INSERT INTO {0}.ObjectType (ObjectTypeId, Name, Label, IsDeleted, CreatedAt, UpdatedAt) VALUES (@OBJECTTYPEID, @NAME, @LABEL, @ISDELETED, @CREATEDAT, @UPDATEDAT)", TenantContext.Current.TenantName);
            }
        }

        /// <summary>
        /// Gets the update statement.
        /// </summary>
        protected override string UpdateStatement
        {
            get
            {
                return string.Format("ALTER TABLE {0}.ObjectType UPDATE Name = @NAME, Label = @LABEL, IsDeleted = @ISDELETED, UpdatedAt = @UPDATEDAT WHERE ObjectTypeId = @OBJECTTYPEID", TenantContext.Current.TenantName);
            }
        }

        /// <summary>
        /// Gets the delete statment.
        /// </summary>
        protected override string DeleteStatement
        {
            get
            {
                return "ALTER DELETE FROM persoObjectTypenprofile WHERE ObjectTypeId = @OBJECTTYPEID";
            }
        }



        /// <summary>
        /// Add sql parameters.
        /// </summary>
        /// <param name="dataHandler">The database handler.</param>
        /// <param name="valObjectTypeId">The valObjectTypeId .</param>
        public static void AddObjectTypeIdParameter(DatabaseHandler dataHandler, int valObjectTypeId)
        {
            var param = new ClickHouseDbParameter();
            param.DbType = DbType.Int32;
            param.Direction = ParameterDirection.Input;
            param.Size = 8;
            param.Value = valObjectTypeId;
            param.ParameterName = "OBJECTTYPEID";
            dataHandler.Command.Parameters.Add(param);
        }



        /// <summary>
        /// Add sql parameters.
        /// </summary>
        /// <param name="dataHandler">The database handler.</param>
        /// <param name="valName">The valName .</param>
        public static void AddNameParameter(DatabaseHandler dataHandler, string valName)
        {
            var param = new ClickHouseDbParameter();
            param.DbType = DbType.String;
            param.Direction = ParameterDirection.Input;
            param.Size = 1024;
            param.Value = valName;
            param.ParameterName = "NAME";
            dataHandler.Command.Parameters.Add(param);
        }

        /// <summary>
        /// Add sql parameters.
        /// </summary>
        /// <param name="dataHandler">The database handler.</param>
        /// <param name="valLabel">The valLabel .</param>
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
            var entity = databaseObject as ObjectType;
            if (mode == DataMode.Insert || mode == DataMode.Update)
            {
                AddObjectTypeIdParameter(databaseHandler, entity.ObjectTypeId);
                AddNameParameter(databaseHandler, entity.Name);
                AddLabelParameter(databaseHandler, entity.Label);
                AddIsDeletedParameter(databaseHandler, entity.IsDeleted);
                AddCreatedAtParameter(databaseHandler, entity.CreatedAt);
                AddUpdatedAtParameter(databaseHandler, entity.UpdatedAt);
            }
            else
            {
                AddObjectTypeIdParameter(databaseHandler, entity.ObjectTypeId);

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
        protected override ObjectType CreateObject(DbDataReader reader, bool populateObject)
        {
            var entity = new ObjectType();
            entity.ObjectTypeId = reader.GetFieldValue<int>(reader.GetOrdinal("ObjectTypeId"));
            entity.Name = reader.GetFieldValue<string>(reader.GetOrdinal("Name"));
            entity.Label = reader.GetFieldValue<string>(reader.GetOrdinal("Lable"));
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
        protected override async Task<ObjectType> CreateObjectAsync(DbDataReader reader, bool populateObject)
        {
            var entity = new ObjectType();
            entity.ObjectTypeId = await reader.GetFieldValueAsync<int>(reader.GetOrdinal("ObjectTypeId"));
            entity.Name = await reader.GetFieldValueAsync<string>(reader.GetOrdinal("Name"));
            entity.Label = await reader.GetFieldValueAsync<string>(reader.GetOrdinal("Label"));
            entity.IsDeleted = await reader.GetFieldValueAsync<bool>(reader.GetOrdinal("IsDeleted"));
            entity.CreatedAt = await reader.GetFieldValueAsync<DateTime>(reader.GetOrdinal("CreatedAt"));
            entity.UpdatedAt = await reader.GetFieldValueAsync<DateTime>(reader.GetOrdinal("UpdatedAt"));
            entity.IsNew = false;
            return entity;
        }
    }
}

