using System.Collections.Generic;

namespace TestProject.Core
{
    public class RequestTypeField : BaseEntity
    {
        public string Name { get; set; }

        public RequestFieldType Type { get; set; }
        
        public long RequestTypeId { get; set; }
        public virtual RequestType RequestType { get; set; }

        public virtual ICollection<RequestValue> RequestValues { get; set; }

        public RequestTypeField()
        {
            RequestValues = new List<RequestValue>();
        }
    }
}
