using System;
namespace Playbook.Service.Contracts
{
    public class ObjectFieldRequest
    {
        public string Name { get; set; }
        public string Label { get; set; }
        public string ObjectTypeId { get; set; }
        public string FieldType { get; set; }
        public string OptionId { get; set; }
    }
}