using System.Collections.Generic;

namespace TestProject.Core
{
    public class RequestTypeFieldVm
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public string Type { get; set; }

        public List<RequestValueVm> RequestValues { get; set; }

        public RequestValueVm RequestValue { get; set; }

        public RequestTypeFieldVm()
        {
            RequestValue = new RequestValueVm();
            RequestValues = new List<RequestValueVm>();
        }

        public RequestTypeFieldVm(RequestTypeField reqTypeField)
        {
            Id = reqTypeField.Id;
            Name = reqTypeField.Name;
            Type = reqTypeField.Type.ToString();
            RequestValues = new List<RequestValueVm>();
            RequestValue = new RequestValueVm();
        }
    }
}
