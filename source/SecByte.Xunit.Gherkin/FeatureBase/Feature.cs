using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace SecByte.Xunit.Gherkin
{
    public abstract class Feature : StepContainer
    {
        internal ITestOutputHelper InternalOutput { get; set; }		

        [Scenario]
        internal async Task Scenario(string scenarioName)
        {
            var scenarioExecutor = new ScenarioExecutor(new FeatureFileRepository());
            await scenarioExecutor.ExecuteScenarioAsync(this, scenarioName);
        }

        [ScenarioOutline]
        internal async Task ScenarioOutline(
            string scenarioOutlineName, 
            string exampleName, 
            int exampleIndex)
        {
            var scenarioOutlineExecutor = new ScenarioOutlineExecutor(new FeatureFileRepository());
            await scenarioOutlineExecutor.ExecuteScenarioOutlineAsync(this, scenarioOutlineName, exampleName, exampleIndex);
        }

		protected internal virtual void OnStepFailed(string scenarioName, string stepText)
		{

		}

        protected internal virtual void OnScenarioComplete(string scenarioName)
        {

        }
    }
}
