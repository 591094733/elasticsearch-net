﻿using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public class ClearCachedRealmsResponse : ResponseBase
	{
		[DataMember(Name ="cluster_name")]
		public string ClusterName { get; internal set; }

		[DataMember(Name ="nodes")]
		public IReadOnlyDictionary<string, SecurityNode> Nodes { get; internal set; } = EmptyReadOnly<string, SecurityNode>.Dictionary;
	}
}
