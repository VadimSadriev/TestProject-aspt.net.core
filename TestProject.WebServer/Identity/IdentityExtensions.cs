using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestProject.WebServer
{
    public static class IdentityExtensions
    {
        public static string AggregateErrors(this IEnumerable<IdentityError> errors)
        {
            return errors?.ToList()
                          .Select(f => f.Description)
                          .Aggregate((a, b) => $"{a}{Environment.NewLine}{b}");
        }
    }
}
