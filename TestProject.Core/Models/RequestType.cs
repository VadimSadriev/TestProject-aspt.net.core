using System.Collections.Generic;

namespace TestProject.Core
{
    public class RequestType : BaseEntity
    {
        public string Name { get; set; }

        public string AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }

        public virtual ICollection<RequestTypeField> RequestTypeFields { get; set; }

        public RequestType()
        {
            RequestTypeFields = new List<RequestTypeField>();
        }
    }
}
