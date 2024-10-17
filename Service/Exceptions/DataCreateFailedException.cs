namespace Service.Exceptions
{
    public class DataCreateFailedException : Exception
    {
        public DataCreateFailedException() : base("DataCreatingFailed.") { }
    }
}
