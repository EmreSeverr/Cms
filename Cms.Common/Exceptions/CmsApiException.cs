namespace Cms.Common.Exceptions
{
    public class CmsApiException : Exception
    {
        public CmsApiException(string message) : base(message)
        {

        }

        public CmsApiException(Exception exception) : base(exception.Message, exception)
        {

        }
    }
}
