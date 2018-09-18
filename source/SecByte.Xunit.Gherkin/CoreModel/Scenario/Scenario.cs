using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace SecByte.Xunit.Gherkin
{
	internal sealed class Scenario
	{
		private readonly string _scenarioName;
		private readonly ReadOnlyCollection<StepMethod> _steps;

		public Scenario(string name, IEnumerable<StepMethod> stepMethods)
		{
			_scenarioName = name;
			_steps = stepMethods != null
				? new ReadOnlyCollection<StepMethod>(stepMethods.ToList())
				: throw new ArgumentNullException(nameof(stepMethods));
		}

		public string Name => _scenarioName;

		public async Task ExecuteAsync(Feature feature, IScenarioOutput scenarioOutput)
        {
            if (scenarioOutput == null)
                throw new ArgumentNullException(nameof(scenarioOutput));			

            var step = _steps.GetEnumerator();
            while(step.MoveNext())
            {
                try
                {
                    await step.Current.ExecuteAsync(feature.ScenarioContext);
                    scenarioOutput.StepPassed($"{step.Current.Kind} {step.Current.StepText}");
                }
                catch
                {
					try
					{
						feature.OnStepFailed(_scenarioName, step.Current.StepText);
					}
					catch(Exception ex)
					{
						feature.InternalOutput.WriteLine(ex.ToString());
					}

                    scenarioOutput.StepFailed($"{step.Current.Kind} {step.Current.StepText}");

                    while(step.MoveNext())
                    {
                        scenarioOutput.StepSkipped($"{step.Current.Kind} {step.Current.StepText}");
                    }

                    throw;
                }
            }
        }
    }
}
