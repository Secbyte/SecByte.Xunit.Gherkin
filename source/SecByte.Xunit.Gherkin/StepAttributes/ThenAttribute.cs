namespace SecByte.Xunit.Gherkin
{

    public sealed class ThenAttribute : BaseStepDefinitionAttribute
    {
        public ThenAttribute(string pattern)
            : base("Then", pattern)
        {

        }
    }
}
