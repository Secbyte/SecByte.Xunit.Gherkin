namespace SecByte.Xunit.Gherkin
{

    public sealed class AndAttribute : BaseStepDefinitionAttribute
    {
        public AndAttribute(string pattern)
            : base("And", pattern)
        {
        }
    }
}
