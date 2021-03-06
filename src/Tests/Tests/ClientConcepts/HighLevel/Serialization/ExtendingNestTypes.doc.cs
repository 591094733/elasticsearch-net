﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using System.Runtime.Serialization;
using Tests.ClientConcepts.HighLevel.Mapping;
using Tests.Core.Client;
using Tests.Domain;
using Tests.Framework;
using static Tests.Core.Serialization.SerializationTestHelper;

namespace Tests.ClientConcepts.HighLevel.Serialization
{
	/**[[extending-nest-types]]
	 * === Extending NEST types
	 *
	 * Sometimes you might want to provide a custom implementation of a type perhaps to work around an issue or because
	 * you're using a third-party plugin that extends the features of Elasticsearch, and NEST does not provide support out of the box.
	 */
	public class ExtendingNestTypes
	{
		private readonly IElasticClient _client = TestClient.DisabledStreaming;

		/* ==== Creating your own property mapping
		 *
		 * As an example, let's imagine we're using a third party plugin that provides support for additional data type
		 * for field mapping. We can implement a custom `IProperty` implementation so that we can use the field mapping
		 * type with NEST.
		 */
		public class MyPluginProperty : IProperty
		{
			IDictionary<string, object> IProperty.LocalMetadata { get; set; }
			public string Type { get; set; } = "my_plugin_property";
			public PropertyName Name { get; set; }

			public MyPluginProperty(string name, string language)
			{
				this.Name = name;
				this.Language = language;
				this.Numeric = true;
			}

			[PropertyName("language")]
			public string Language { get; set; }

			[PropertyName("numeric")]
			public bool Numeric { get; set; }
		}


		[U (Skip = "TODO: Does not work with utf8json")]
		public void InjectACustomIPropertyImplementation()
		{
			/**
			 * `PropertyNameAttribute` can be used to mark properties that should be serialized. Without this attribute,
			 * NEST won't pick up the property for serialization.
			 *
			 * Now that we have our own `IProperty` implementation we can add it to our propertes mapping when creating an index
			 */
			var createIndexResponse = _client.CreateIndex("myindex", c => c
				.Map<Project>(m => m
					.Properties(props => props
						.Custom(new MyPluginProperty("fieldName", "dutch"))
					)
				)
			);

			/**
			 * which will serialize to the following JSON request
			 */
			// json
			var expected = new
			{
				mappings = new
				{
					properties = new
					{
						fieldName = new
						{
							type = "my_plugin_property",
							language = "dutch",
							numeric = true
						}
					}
				}
			};

			/**
			 * Whilst NEST can _serialize_ our `my_plugin_property`, it does not know how to _deserialize_ it;
			 * We plan to make this more pluggable in the future
			 */
			// hide
			Expect(expected).FromRequest(createIndexResponse);
		}
	}
}
