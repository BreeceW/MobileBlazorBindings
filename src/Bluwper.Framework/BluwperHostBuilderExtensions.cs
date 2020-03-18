// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging.EventLog;
using System;
using System.Linq;

namespace Bluwper.Framework
{
    public static class BluwperHostBuilderExtensions
    {
        /// <summary>
        /// Registers <see cref="BluwperHostedService"/> in the DI container. Call this as part of configuring the
        /// host to enable Bluwper.
        /// </summary>
        /// <param name="hostBuilder"></param>
        /// <returns></returns>
        public static IHostBuilder AddBluwper(this IHostBuilder hostBuilder)
        {
            if (hostBuilder is null)
            {
                throw new ArgumentNullException(nameof(hostBuilder));
            }

            hostBuilder.ConfigureServices((hostContext, services) =>
            {
                // UWP patch
                var eventLog = services.FirstOrDefault(s => s.ImplementationType == typeof(EventLogLoggerProvider));
                if (eventLog != null)
                {
                    services.Remove(eventLog);
                }

                services.AddHostedService<BluwperHostedService>();
            });

            return hostBuilder;
        }
    }
}
