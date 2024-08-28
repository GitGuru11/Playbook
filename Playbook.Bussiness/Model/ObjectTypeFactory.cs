using System;
using ClickHouse.Client.ADO;
using System.Data;
using System.Drawing;
using Playbook.Business.Model;
using Playbook.Common.Instrumentation;
using Playbook.Data.ClickHouse;
using System.Data.Common;
using Playbook.Common.Tenant;

namespace Playbook.Bussiness.Model
{
    /// <summary>
    /// Factory base class for ObjectTypeFactory.
    /// </summary>
    public partial class ObjectTypeFactory : ObjectTypeFactoryBase
    {
        public async Task<List<ObjectType>> FindAllActiveObjectType()
        {
            using (var dataHandler = new DatabaseHandler())
            {
                dataHandler.Command.CommandText = SelectAllStatement + " WHERE IsDeleted=@ISDELETED ORDER BY ObjectTypeId ";
                AddIsDeletedParameter(dataHandler, false);
                return await FindAsync(dataHandler);
            }
        }

    }
}

