using System;
using Playbook.Business.Model;
using Playbook.Bussiness.Model;
using Playbook.Common.Instrumentation;
using Playbook.Data.ClickHouse;
using Playbook.Service.Contracts;
using ObjectType = Playbook.Business.Model.ObjectType;

namespace Playbook.Service.Handlers
{
    public class ObjectConfigurationHandler
    {
        public ObjectConfigurationHandler()
        {
        }

        public async Task<List<Playbook.Service.Contracts.ObjectTypeResponse>> ProcessGetSupportedObjectTypeRequestAsync()
        {
            var list = await new ObjectTypeFactory().FindAllActiveObjectType();
            var response = new List<Playbook.Service.Contracts.ObjectTypeResponse>();
            foreach (var item in list)
            {
                response.Add(new Contracts.ObjectTypeResponse { Id = item.ObjectTypeId, Name = item.Name, Label = item.Label });
            }

            return response;
        }

        public async Task<int> ProcessAddSupportedObjectTypeRequestAsync()
        {
            var factory = new ObjectTypeFactory();
            var obj = new ObjectType();
            obj.ObjectTypeId = 1;//UuidFactory.GetUuid();
            obj.Name = "Type1";
            obj.Label = "Type1";
            obj.IsDeleted = true;
            obj.CreatedAt = DateTime.UtcNow;
            obj.UpdatedAt = DateTime.UtcNow;
            obj.IsNew = true;

            if (!await factory.SaveAsync(obj))
            {
                InstrumentationContext.Current.Error("ProcessAddSupportedObjectTypeRequestAsync", "Problem addig supported object type.");
            }
            return obj.ObjectTypeId;
        }

        public async Task<int> ProcessUpdateSupportedObjectTypeRequestAsync(int id)
        {
            var factory = new ObjectTypeFactory();
            var obj = new ObjectType();
            obj.ObjectTypeId = id;
            obj.Name = "Type2";
            obj.Label = "Type2";
            obj.IsDeleted = true;
            obj.CreatedAt = DateTime.UtcNow;
            obj.UpdatedAt = DateTime.UtcNow;
            obj.IsNew = false;

            if (!await factory.SaveAsync(obj))
            {
                InstrumentationContext.Current.Error("ProcessAddSupportedObjectTypeRequestAsync", "Problem addig supported object type.");
            }
            return obj.ObjectTypeId;
        }
    }
}


