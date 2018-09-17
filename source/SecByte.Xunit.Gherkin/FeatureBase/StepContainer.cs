using System.Collections.Generic;

namespace SecByte.Xunit.Gherkin
{
    public abstract class StepContainer
    {
		protected StepContainer()
		{
			ScenarioContext = new Dictionary<string, object>();
		}

		public Dictionary<string, object> ScenarioContext { get; internal set; }
    }
}
