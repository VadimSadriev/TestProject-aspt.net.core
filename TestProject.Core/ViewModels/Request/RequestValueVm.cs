using System;

namespace TestProject.Core
{
    public class RequestValueVm
    {
        public long Id { get; set; }
        public string StringValue { get; set; }
        public int IntValue { get; set; }
        public string DateValue { get; set; }
        public string TimeValue { get; set; }
        public string FileValue { get; set; }
        public string FileName { get; set; }

        public RequestValueVm()
        {

        }

        public RequestValueVm(RequestValue x)
        {
            Id = x.Id;
            StringValue = x.StringValue;
            IntValue = x.IntValue;
            DateValue = x.DateValue.ToShortDateString();
            TimeValue = x.TimeValue.ToString();
            FileValue = x.FileValue;
            FileName = x.FileName;
        }
    }
}
