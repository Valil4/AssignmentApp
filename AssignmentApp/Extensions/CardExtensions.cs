using System.Text.RegularExpressions;

namespace AssignmentApp.Extensions;

public static class CardExtensions
{
    public static bool IsValid(this string pan) => Regex.IsMatch(pan, @"^[0-9]{16}$");
}
