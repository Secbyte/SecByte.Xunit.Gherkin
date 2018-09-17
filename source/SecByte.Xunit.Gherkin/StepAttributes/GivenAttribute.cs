namespace SecByte.Xunit.Gherkin
{

    public sealed class GivenAttribute : BaseStepDefinitionAttribute
    {
        public GivenAttribute(string pattern)
            : base("Given", pattern)
        {

        }
    }
}
