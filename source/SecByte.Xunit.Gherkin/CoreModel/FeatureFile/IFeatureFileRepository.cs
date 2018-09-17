namespace SecByte.Xunit.Gherkin
{
    internal interface IFeatureFileRepository
    {
        FeatureFile GetByFilePath(string filePath);
    }
}
