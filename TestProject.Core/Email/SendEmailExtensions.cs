using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject.Core
{
    /* NOTE: These Methods are actually overkill cuz u can just call
     services.AddTransient<IEmailSender, SendGridEmailSender(or whatever impl)> in startup class*/
    /// <summary>
    /// extensions for send email classes
    /// </summary>
    public static class SendEmailExtensions
    {
        /// <summary>
        /// Adds implementation of <see cref="IEmailSender"/> as dependency injection
        /// </summary>
        /// <typeparam name="TImplementation">Implementation of IEmailSender</typeparam>
        /// <param name="services">Collection of services</param>
        /// <param name="implementation">register implementation as di</param>
        public static IServiceCollection AddEmailSender<TImplementation>(this IServiceCollection services) where TImplementation :  class, IEmailSender
        {
            
            services.AddTransient<IEmailSender, TImplementation>();

            return services;
        }

        /// <summary>
        /// add implementation of <see cref="IEmailTemplateSender"/> as di
        /// </summary>
        /// <typeparam name="TImplementation">implementation of <see cref="IEmailTemplateSender"/></typeparam>
        /// <param name="services">Collection of services</param>
        public static IServiceCollection AddEmailTemplateSender<TImplementation>(this IServiceCollection services) where TImplementation : class, IEmailTemplateSender
        {
            
            services.AddTransient<IEmailTemplateSender, TImplementation>();

            return services;
        }
    }
}
