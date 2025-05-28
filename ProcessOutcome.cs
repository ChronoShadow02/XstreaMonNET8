namespace XstreaMonNET8
{
    public enum ProcessOutcome
    {
        Success,
        LicenceObjectRequiredError,
        LicenseKeyNotPassedError,
        GeneratorObjectRequiredError,
        GeneratorIdNotPassedError,
        AdditionalParametersJsonNonParsable,
        WebClientError,
        Indeterminate
    }
}
