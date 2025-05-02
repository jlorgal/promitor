using System.ComponentModel;
using Promitor.Core.Scraping.Configuration.Serialization;
using Promitor.Core.Scraping.Configuration.Serialization.v1.Model;
using Promitor.Core.Scraping.Configuration.Serialization.v1.Model.ResourceTypes;
using Promitor.Core.Scraping.Configuration.Serialization.v1.Providers;
using Xunit;

namespace Promitor.Tests.Unit.Serialization.v1.Providers
{
    [Category("Unit")]
    public class EventHubClusterDeserializerTests : ResourceDeserializerTest<EventHubClusterDeserializer>
    {
        private readonly EventHubClusterDeserializer _deserializer;

        public EventHubClusterDeserializerTests()
        {
            _deserializer = new EventHubClusterDeserializer(Logger);
        }

        [Fact]
        public void Deserialize_ClusterNameSupplied_SetsClusterName()
        {
            const string clusterName = "promitor-eventhub-cluster";
            YamlAssert.PropertySet<EventHubClusterResourceV1, AzureResourceDefinitionV1, string>(
                _deserializer,
                $"clusterName: {clusterName}",
                clusterName,
                r => r.ClusterName);
        }

        [Fact]
        public void Deserialize_ClusterNameNotSupplied_Null()
        {
            YamlAssert.PropertyNull<EventHubClusterResourceV1, AzureResourceDefinitionV1>(
                _deserializer,
                "resourceGroupName: promitor-group",
                r => r.ClusterName);
        }

        [Fact]
        public void Deserialize_ClusterNameNotSupplied_ReportsError()
        {
            // Arrange
            var node = YamlUtils.CreateYamlNode("resourceGroupName: promitor-resource-group");

            // Act / Assert
            YamlAssert.ReportsErrorForProperty(
                _deserializer,
                node,
                "clusterName");
        }

        protected override IDeserializer<AzureResourceDefinitionV1> CreateDeserializer()
        {
            return new EventHubClusterDeserializer(Logger);
        }
    }
}