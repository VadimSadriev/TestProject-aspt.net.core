using System.Collections.Generic;
using System.Linq;

namespace TestProject.Core
{
    public class SendEmailResponse
    {
        public bool Success => !(Errors?.Count() > 0);
        public List<string> Errors { get; set; }
    }
}