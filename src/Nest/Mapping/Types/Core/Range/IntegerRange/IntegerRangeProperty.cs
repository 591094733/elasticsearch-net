using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	/// <summary>
	/// A range of signed 32-bit integers with a minimum value of -231 and maximum of 231-1.
	/// </summary>
	[InterfaceDataContract]
	public interface IIntegerRangeProperty : IRangeProperty { }

	/// <inheritdoc />
	public class IntegerRangeProperty : RangePropertyBase, IIntegerRangeProperty
	{
		public IntegerRangeProperty() : base(RangeType.IntegerRange) { }
	}

	/// <inheritdoc />
	public class IntegerRangePropertyDescriptor<T>
		: RangePropertyDescriptorBase<IntegerRangePropertyDescriptor<T>, IIntegerRangeProperty, T>, IIntegerRangeProperty
		where T : class
	{
		public IntegerRangePropertyDescriptor() : base(RangeType.IntegerRange) { }
	}
}
