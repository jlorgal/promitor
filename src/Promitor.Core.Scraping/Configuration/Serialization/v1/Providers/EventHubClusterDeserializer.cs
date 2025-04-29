using Microsoft.Extensions.Logging;
using Promitor.Core.Scraping.Configuration.Serialization.v1.Model.ResourceTypes;
using System.Collections.Generic;

namespace Promitor.Core.Scraping.Configuration.Serialization.v1.Providers
{
    public class EventHubClusterDeserializer : ResourceDeserializer<EventHubClusterResourceV1>
    {
        public EventHubClusterDeserializer(ILogger<EventHubClusterDeserializer> logger) : base(logger)
        {
            Map(resource => resource.ClusterName)
                .IsRequired();
        }
    }
}