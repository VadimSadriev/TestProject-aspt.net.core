using System;

namespace TestProject.Core
{
    public class RequestListVm
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string DateCreated { get; set; }
        public string DisplayStatus { get; set; }
        public string Creator { get; set; }

        public RequestListVm()
        {

        }

        public RequestListVm(Request req)
        {
            Id = req.Id;
            Name = req.Name;
            DateCreated = req.DateCreated.ToShortDateString();
            DisplayStatus = req.Status != RequestStatus.Unknown ? req.Status.GetRuName() : null;
            Type = req.RequestType.Name;
            Creator = req.AppUser.UserName;
        }
    }
}
