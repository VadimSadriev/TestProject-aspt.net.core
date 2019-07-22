using System.Collections.Generic;
using System.Linq;

namespace TestProject.Core
{
    public class RequestTypeVm
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }

        public List<RequestTypeFieldVm> RequestTypeFields { get; set; }

        public RequestTypeVm()
        {
            RequestTypeFields = new List<RequestTypeFieldVm>();
        }

        public RequestTypeVm(RequestType reqType)
        {
            Id = reqType.Id;
            Name = reqType.Name;
            RequestTypeFields = new List<RequestTypeFieldVm>();

            if (reqType.RequestTypeFields.Any())
            {
                RequestTypeFields.AddRange(reqType.RequestTypeFields
                                          .Where(x => !x.IsDeleted && x.Type != RequestFieldType.Unknown)
                                          .Select(x => new RequestTypeFieldVm(x))
                                          .OrderBy(x => x.Type)
                                          .ToList());
            }
        }
    }
}
