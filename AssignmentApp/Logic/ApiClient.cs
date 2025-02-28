using AssignmentApp.Models;

namespace AssignmentApp.Helpers;

public static class ApiClient
{
    public static async Task<HttpResponseMessage> SendRequest(string url, RequestModel request, string jws)
    {
        var client = new HttpClient();

        var content = new StringContent(jws);
        var response = await client.PostAsync(url, content);

        return response;
    }
}
