﻿using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Domain;
using Tests.Framework;
using static Tests.Framework.UrlTester;

namespace Tests.XPack.MachineLearning.PreviewDatafeed
{
	public class PreviewDatafeedUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await GET("/_ml/datafeeds/datafeed_id/_preview")
			.Fluent(c => c.PreviewDatafeed<Metric>("datafeed_id", p => p))
			.Request(c => c.PreviewDatafeed<Metric>(new PreviewDatafeedRequest("datafeed_id")))
			.FluentAsync(c => c.PreviewDatafeedAsync<Metric>("datafeed_id", p => p))
			.RequestAsync(c => c.PreviewDatafeedAsync<Metric>(new PreviewDatafeedRequest("datafeed_id")));
	}
}
