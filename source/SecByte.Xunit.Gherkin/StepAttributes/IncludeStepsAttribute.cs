using System;

namespace SecByte.Xunit.Gherkin
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public class IncludeStepsAttribute : Attribute
	{
		public IncludeStepsAttribute(Type fromType) => FromType = fromType;

		public Type FromType { get; }
    }
}
