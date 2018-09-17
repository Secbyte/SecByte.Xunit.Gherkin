namespace SecByte.Xunit.Gherkin
{

    public sealed class ButAttribute : BaseStepDefinitionAttribute
    {
        public ButAttribute(string pattern)
            : base("But", pattern)
        {
        }
    }
}
