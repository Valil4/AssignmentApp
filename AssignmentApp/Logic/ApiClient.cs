using AssignmentApp.Models;
using Newtonsoft.Json;
using System.Text;

namespace AssignmentApp.Helpers;

public static class ApiClient
{
    public static async Task SendRequest(CardInfoModel cardInfo, string jws)
    {
        var host = "acstopay.online";

        var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jws);

        var url = $"https://{host}/api/testassignments/pan";
        var payloadJson = JsonConvert.SerializeObject(cardInfo);
        var content = new StringContent(payloadJson, Encoding.UTF8, "application/json");
        var response = await client.PostAsync(url, content);

        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!string.IsNullOrEmpty(responseContent) 
                && !responseContent.Contains("error", StringComparison.CurrentCultureIgnoreCase)) 
            {
                var responseModel = JsonConvert.DeserializeObject<ResponseModel>(responseContent);

                switch (responseModel.Status)
                {
                    case "Success":
                        Console.WriteLine("Successfully");
                        break;
                    case "Failed":
                        Console.WriteLine("Unsuccessfully");
                        break;
                    default:
                        break;
                }
            }
            else
            {
                var responseModel = JsonConvert.DeserializeObject<ResponseErrorModel>(responseContent);

                Console.WriteLine($"Error message: {responseModel.Error.Message}");
            }
        }
        else
            Console.WriteLine(string.Concat(response.StatusCode.ToString(), " error"));

        Console.WriteLine();
        Console.WriteLine("Press Enter to continue or Esc to exit");
    }
}
