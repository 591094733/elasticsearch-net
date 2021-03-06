﻿using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	public class ReindexRethrottleResponse : ResponseBase
	{
		[DataMember(Name ="nodes")]
		[JsonFormatter(typeof(VerbatimInterfaceReadOnlyDictionaryKeysFormatter<string, ReindexNode>))]
		public IReadOnlyDictionary<string, ReindexNode> Nodes { get; internal set; } = EmptyReadOnly<string, ReindexNode>.Dictionary;
	}
}
