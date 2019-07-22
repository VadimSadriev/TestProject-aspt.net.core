using System.Collections.Generic;
using System.Linq;

namespace TestProject.Core
{
    public class SendGridResponse
    {
        public List<SendGridResponseError> Errors { get; set; }
    }
}
