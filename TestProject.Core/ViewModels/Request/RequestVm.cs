using System.Collections.Generic;

namespace TestProject.Core
{
    public class RequestVm
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string DisplayStatus { get; set; }

        public RequestTypeVm RequestType { get; set; }

        public RequestVm()
        {
        }

        public RequestVm(Request request)
        {
            Id = request.Id;
            Name = request.Name;
            Status = request.Status.ToString();
            DisplayStatus = request.Status != RequestStatus.Unknown ? request.Status.GetRuName() : null;
        }
    }
}
