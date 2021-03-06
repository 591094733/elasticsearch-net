﻿using System;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Cluster.NodesUsage
{
	public class NodesUsageApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, NodesUsageResponse, INodesUsageRequest, NodesUsageDescriptor, NodesUsageRequest>
	{
		public NodesUsageApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_nodes/usage";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.NodesUsage(),
			(client, f) => client.NodesUsageAsync(),
			(client, r) => client.NodesUsage(r),
			(client, r) => client.NodesUsageAsync(r)
		);

		protected override void ExpectResponse(NodesUsageResponse response)
		{
			response.ClusterName.Should().NotBeEmpty();

			response.NodeStatistics.Should().NotBeNull();
			response.NodeStatistics.Total.Should().Be(1);
			response.NodeStatistics.Successful.Should().Be(1);
			response.NodeStatistics.Failed.Should().Be(0);

			response.Nodes.Should().NotBeNull();
			response.Nodes.Should().HaveCount(1);

			response.Nodes.First().Value.Timestamp.Should().BeBefore(DateTimeOffset.UtcNow);
			response.Nodes.First().Value.Since.Should().BeBefore(DateTimeOffset.UtcNow);
			response.Nodes.First().Value.RestActions.Should().NotBeNull();
		}
	}
}
