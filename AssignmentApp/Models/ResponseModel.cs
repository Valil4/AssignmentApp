using AssignmentApp.Enums;

namespace AssignmentApp.Models;

public class ResponseModel
{
    public string Id { get; set; }

    public CardInfoModel CardInfo { get; set; } = new();

    public Status Status { get; set; } = Status.Failed;
}