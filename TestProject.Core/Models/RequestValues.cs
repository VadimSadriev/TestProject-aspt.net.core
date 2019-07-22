using System;

namespace TestProject.Core
{
    public class RequestValue : BaseEntity
    {
        public string StringValue { get; set; }
        public int IntValue { get; set; }
        public DateTime DateValue { get; set; }
        public TimeSpan TimeValue { get; set; }
        public string FileValue { get; set; }
        public string FileName { get; set; }

        public long RequestTypeFieldId { get; set; }
        public virtual RequestTypeField RequestTypeField { get; set; }

        public long RequestId { get; set; }
        public virtual Request Request { get; set; }
    }
}
