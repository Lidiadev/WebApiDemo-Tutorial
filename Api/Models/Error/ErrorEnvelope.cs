namespace Api.Models.Error
{
    public class ErrorEnvelope
    {
        public ErrorModel Error { get; set; }

        public ErrorEnvelope()
        {
        }

        public ErrorEnvelope(ErrorModel error)
        {
            Error = error;
        }

        public ErrorEnvelope(int code, string message)
        {
            Error = new ErrorModel { Code = code, Message = message };
        }
    }
}