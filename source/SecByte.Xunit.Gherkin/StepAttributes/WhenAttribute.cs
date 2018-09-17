namespace SecByte.Xunit.Gherkin
{

    public sealed class WhenAttribute : BaseStepDefinitionAttribute
    {
        public WhenAttribute(string pattern)
            : base("When", pattern)
        {

        }
    }
}
