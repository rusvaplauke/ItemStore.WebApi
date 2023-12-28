namespace ItemStore.WebApi.Exceptions
{
    public class JsonPlaceholderException : Exception
    {
        public JsonPlaceholderException(string statusCode) : base($"Problem with the external API. Exception: {statusCode}.")
        {
        }
    }
}
