namespace Stock.Exceptions;


public class NotFoundException : ControllerException
{
    public NotFoundException(string Message) : base(404, Message) {}
}
public class ControllerException : Exception
{
    public int StatusCode { get; set; }
    public ControllerException(int StatusCode, string Message) : base(Message) {
        this.StatusCode = StatusCode;
    }   
}