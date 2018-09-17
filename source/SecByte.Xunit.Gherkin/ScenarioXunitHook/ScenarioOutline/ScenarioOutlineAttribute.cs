using System;
using Xunit;
using Xunit.Sdk;

namespace SecByte.Xunit.Gherkin
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    [XunitTestCaseDiscoverer("SecByte.Xunit.Gherkin.ScenarioOutlineDiscoverer", "SecByte.Xunit.Gherkin")]
    internal sealed class ScenarioOutlineAttribute : FactAttribute
    { }
}
