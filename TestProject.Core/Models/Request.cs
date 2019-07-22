using System;
using System.Collections.Generic;

namespace TestProject.Core
{
    public class Request : BaseEntity
    {
        public string Name { get; set; }
        public RequestStatus Status { get; set; }

        public DateTime DateCreated { get; set; }

        public long RequestTypeId { get; set; }
        public virtual RequestType RequestType { get; set; }

        public string AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }

        public virtual ICollection<RequestValue> RequestValues { get; set; }

        public Request()
        {
            RequestValues = new List<RequestValue>();
        }
    }
}
