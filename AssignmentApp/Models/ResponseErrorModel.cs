namespace AssignmentApp.Models;

public class ResponseErrorModel
{
    public ErrorModel Error { get; set; }
}

public class ErrorModel
{
    public Code Code { get; set; }

    public string Message { get; set; }
}

public enum Code
{
    BadRequest,
    Success,
}