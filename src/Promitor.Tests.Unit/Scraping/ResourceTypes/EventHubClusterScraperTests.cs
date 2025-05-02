using System;
using Microsoft.Extensions.Logging.Abstractions;
using Promitor.Core.Contracts.ResourceTypes;
using Promitor.Core.Scraping.Configuration.Model;
using Promitor.Core.Scraping.Configuration.Model.Metrics;
using Promitor.Core.Scraping.ResourceTypes;
using Xunit;

namespace Promitor.Tests.Unit.Scraping.ResourceTypes
{
    public class EventHubClusterScraperTests
    {
        private readonly ScrapeDefinition<IAzureResourceDefinition> _scrapeDefinition;
        private readonly EventHubClusterScraper _scraper;

        public EventHubClusterScraperTests()
        {
            // Arrange
            var azureMetadata = new AzureMetadata
            {
                TenantId = "tenant-id",
                SubscriptionId = "subscription-id",
                ResourceGroupName = "resource-group-name"
            };

            var scraperConfiguration = new ScraperConfiguration(azureMetadata, NullLogger<EventHubClusterScraper>.Instance);
            _scraper = new EventHubClusterScraper(scraperConfiguration);

            _scrapeDefinition = new ScrapeDefinition<IAzureResourceDefinition>
            {
                ResourceGroupName = "promitor-resource-group"
            };
        }

        [Fact]
        public void BuildResourceUri_ValidResource_ReturnsCorrectUri()
        {
            // Arrange
            var clusterName = "promitor-eventhub-cluster";
            var resource = new EventHubClusterResourceDefinition
            {
                ClusterName = clusterName
            };
            var subscriptionId = "subscription-id";

            // Act
            var resourceUri = _scraper.GetResourceUri(subscriptionId, _scrapeDefinition, resource);

            // Assert
            var expectedUri = $"subscriptions/{subscriptionId}/resourceGroups/{_scrapeDefinition.ResourceGroupName}/providers/Microsoft.EventHub/clusters/{clusterName}";
            Assert.Equal(expectedUri, resourceUri);
        }

        [Fact]
        public void BuildResourceUri_NullResource_ThrowsArgumentException()
        {
            // Arrange
            EventHubClusterResourceDefinition resource = null;
            var subscriptionId = "subscription-id";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _scraper.GetResourceUri(subscriptionId, _scrapeDefinition, resource));
        }

        [Fact]
        public void BuildResourceUri_WrongResourceType_ThrowsArgumentException()
        {
            // Arrange
            var resource = new MockResourceDefinition();
            var subscriptionId = "subscription-id";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _scraper.GetResourceUri(subscriptionId, _scrapeDefinition, resource));
        }

        private class MockResourceDefinition : IAzureResourceDefinition
        {
        }
    }
}