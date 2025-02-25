namespace AssignmentApp.Models;

public class ResponseModel
{
    public long Id { get; set; }

    public CardInfoModel CardInfo { get; set; } = new();

    public string Status { get; set; } = "Failed";
}