﻿using Moq;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;
using SecByte.Xunit.Gherkin;

namespace UnitTests
{
    public sealed class ScenarioTests
    {
        private sealed class FeatureForCtorTest : Feature
        {
            [Then("something")]
            public void Then_Something()
            {

            }

            [When("something")]
            public void When_Something()
            {

            }

            [Given("something")]
            public void Given_Something()
            {

            }
        }

        [Fact]
        public async Task Execute_Invokes_All_StepMethods()
        {
            //arrange.
            var featureInstance = new FeatureWithStepMethodsToInvoke();

            var sut = new Scenario(String.Empty, new List<StepMethod>
            {
                StepMethod.FromStepMethodInfo(StepMethodInfo.FromMethodInfo(featureInstance.GetType().GetMethod(nameof(FeatureWithStepMethodsToInvoke.ScenarioStep1)), featureInstance), new Gherkin.Ast.Step(null, "Given", FeatureWithStepMethodsToInvoke.ScenarioStep1Text, null)),
                StepMethod.FromStepMethodInfo(StepMethodInfo.FromMethodInfo(featureInstance.GetType().GetMethod(nameof(FeatureWithStepMethodsToInvoke.ScenarioStep2)), featureInstance), new Gherkin.Ast.Step(null, "And", FeatureWithStepMethodsToInvoke.ScenarioStep2Text, null)),
                StepMethod.FromStepMethodInfo(StepMethodInfo.FromMethodInfo(featureInstance.GetType().GetMethod(nameof(FeatureWithStepMethodsToInvoke.ScenarioStep3)), featureInstance), new Gherkin.Ast.Step(null, "When", FeatureWithStepMethodsToInvoke.ScenarioStep3Text, null)),
                StepMethod.FromStepMethodInfo(StepMethodInfo.FromMethodInfo(featureInstance.GetType().GetMethod(nameof(FeatureWithStepMethodsToInvoke.ScenarioStep4)), featureInstance), new Gherkin.Ast.Step(null, "Then", FeatureWithStepMethodsToInvoke.ScenarioStep4Text, null))
            });

            var output = new Mock<IScenarioOutput>();

            //act.
            await sut.ExecuteAsync(featureInstance, output.Object);

            //assert.
            Assert.NotNull(featureInstance.CallStack);
            Assert.Equal(4, featureInstance.CallStack.Count);

            Assert.Equal(nameof(FeatureWithStepMethodsToInvoke.ScenarioStep1), featureInstance.CallStack[0]);
            output.Verify(o => o.StepPassed("Given " + FeatureWithStepMethodsToInvoke.ScenarioStep1Text), Times.Once);

            Assert.Equal(nameof(FeatureWithStepMethodsToInvoke.ScenarioStep2), featureInstance.CallStack[1]);
            output.Verify(o => o.StepPassed("And " + FeatureWithStepMethodsToInvoke.ScenarioStep2Text), Times.Once);

            Assert.Equal(nameof(FeatureWithStepMethodsToInvoke.ScenarioStep3), featureInstance.CallStack[2]);
            output.Verify(o => o.StepPassed("When " + FeatureWithStepMethodsToInvoke.ScenarioStep3Text), Times.Once);

            Assert.Equal(nameof(FeatureWithStepMethodsToInvoke.ScenarioStep4), featureInstance.CallStack[3]);
            output.Verify(o => o.StepPassed("Then " + FeatureWithStepMethodsToInvoke.ScenarioStep4Text), Times.Once);
        }

        private sealed class FeatureWithStepMethodsToInvoke : Feature
        {
            public List<string> CallStack { get; } = new List<string>();

            public const string ScenarioStep1Text = "I chose 12 as first number";

            [Given(ScenarioStep1Text)]
            public void ScenarioStep1()
            {
                CallStack.Add(nameof(ScenarioStep1));
            }

            [Given("Non matching given")]
            public void NonMatchingStep1()
            {
                CallStack.Add(nameof(NonMatchingStep1));
            }

            public const string ScenarioStep2Text = "I chose 15 as second number";

            [And(ScenarioStep2Text)]
            public void ScenarioStep2()
            {
                CallStack.Add(nameof(ScenarioStep2));
            }

            [And("Non matching and")]
            public void NonMatchingStep2()
            {
                CallStack.Add(nameof(NonMatchingStep2));
            }

            public const string ScenarioStep3Text = "I press add";

            [When(ScenarioStep3Text)]
            public void ScenarioStep3()
            {
                CallStack.Add(nameof(ScenarioStep3));
            }

            [When("Non matching when")]
            public void NonMatchingStep3()
            {
                CallStack.Add(nameof(NonMatchingStep3));
            }

            public const string ScenarioStep4Text = "the result should be 27 on the screen";

            [Then(ScenarioStep4Text)]
            public void ScenarioStep4()
            {
                CallStack.Add(nameof(ScenarioStep4));
            }

            [Then("Non matching then")]
            public void NonMatchingStep4()
            {
                CallStack.Add(nameof(NonMatchingStep4));
            }
        }

        [Fact]
        public async Task ExecuteAsync_Requires_Output()
        {
            //arrange.
            var sut = new Scenario(String.Empty, new List<StepMethod>());

            //act / assert.
            await Assert.ThrowsAsync<ArgumentNullException>(() => sut.ExecuteAsync(null, null));
        }

        [Fact]
        public async Task Execute_Invokes_Successfult_StepMethods_And_Skips_The_Rest()
        {
            //arrange.
            var featureInstance = new FeatureWithStepMethodsToInvoke_And_Throwing();

            var sut = new Scenario(String.Empty, new List<StepMethod>
            {
                StepMethod.FromStepMethodInfo(StepMethodInfo.FromMethodInfo(featureInstance.GetType().GetMethod(nameof(FeatureWithStepMethodsToInvoke_And_Throwing.ScenarioStep1)), featureInstance), new Gherkin.Ast.Step(null, "Given", FeatureWithStepMethodsToInvoke_And_Throwing.ScenarioStep1Text, null)),
                StepMethod.FromStepMethodInfo(StepMethodInfo.FromMethodInfo(featureInstance.GetType().GetMethod(nameof(FeatureWithStepMethodsToInvoke_And_Throwing.ScenarioStep2)), featureInstance), new Gherkin.Ast.Step(null, "And", FeatureWithStepMethodsToInvoke_And_Throwing.ScenarioStep2Text, null)),
                StepMethod.FromStepMethodInfo(StepMethodInfo.FromMethodInfo(featureInstance.GetType().GetMethod(nameof(FeatureWithStepMethodsToInvoke_And_Throwing.ScenarioStep3)), featureInstance), new Gherkin.Ast.Step(null, "When", FeatureWithStepMethodsToInvoke_And_Throwing.ScenarioStep3Text, null)),
                StepMethod.FromStepMethodInfo(StepMethodInfo.FromMethodInfo(featureInstance.GetType().GetMethod(nameof(FeatureWithStepMethodsToInvoke_And_Throwing.ScenarioStep4)), featureInstance), new Gherkin.Ast.Step(null, "Then", FeatureWithStepMethodsToInvoke_And_Throwing.ScenarioStep4Text, null))
            });

            var output = new Mock<IScenarioOutput>();

            //act.
            var exception = await Assert.ThrowsAsync<TargetInvocationException>(async () => await sut.ExecuteAsync(featureInstance, output.Object));
            Assert.IsType<InvalidOperationException>(exception.InnerException);

            //assert.
            Assert.NotNull(featureInstance.CallStack);
            Assert.Equal(2, featureInstance.CallStack.Count);

            Assert.Equal(nameof(FeatureWithStepMethodsToInvoke_And_Throwing.ScenarioStep1), featureInstance.CallStack[0]);
            output.Verify(o => o.StepPassed("Given " + FeatureWithStepMethodsToInvoke_And_Throwing.ScenarioStep1Text), Times.Once);

            Assert.Equal(nameof(FeatureWithStepMethodsToInvoke_And_Throwing.ScenarioStep2), featureInstance.CallStack[1]);
            output.Verify(o => o.StepFailed("And " + FeatureWithStepMethodsToInvoke_And_Throwing.ScenarioStep2Text), Times.Once);

            output.Verify(o => o.StepSkipped("When " + FeatureWithStepMethodsToInvoke_And_Throwing.ScenarioStep3Text), Times.Once);

            output.Verify(o => o.StepSkipped("Then " + FeatureWithStepMethodsToInvoke_And_Throwing.ScenarioStep4Text), Times.Once);
        }

        private sealed class FeatureWithStepMethodsToInvoke_And_Throwing : Feature
        {
            public List<string> CallStack { get; } = new List<string>();

            public const string ScenarioStep1Text = "I chose 12 as first number";

            [Given(ScenarioStep1Text)]
            public void ScenarioStep1()
            {
                CallStack.Add(nameof(ScenarioStep1));
            }

            [Given("Non matching given")]
            public void NonMatchingStep1()
            {
                CallStack.Add(nameof(NonMatchingStep1));
            }

            public const string ScenarioStep2Text = "I chose 15 as second number";

            [And(ScenarioStep2Text)]
            public void ScenarioStep2()
            {
                CallStack.Add(nameof(ScenarioStep2));

                throw new InvalidOperationException("Throwing to stop execution of next steps");
            }

            [And("Non matching and")]
            public void NonMatchingStep2()
            {
                CallStack.Add(nameof(NonMatchingStep2));
            }

            public const string ScenarioStep3Text = "I press add";

            [When(ScenarioStep3Text)]
            public void ScenarioStep3()
            {
                CallStack.Add(nameof(ScenarioStep3));
            }

            [When("Non matching when")]
            public void NonMatchingStep3()
            {
                CallStack.Add(nameof(NonMatchingStep3));
            }

            public const string ScenarioStep4Text = "the result should be 27 on the screen";

            [Then(ScenarioStep4Text)]
            public void ScenarioStep4()
            {
                CallStack.Add(nameof(ScenarioStep4));
            }

            [Then("Non matching then")]
            public void NonMatchingStep4()
            {
                CallStack.Add(nameof(NonMatchingStep4));
            }
        }
    }
}
