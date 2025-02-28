using AssignmentApp.Enums;
using AssignmentApp.Extensions;
using AssignmentApp.Helpers;
using AssignmentApp.Models;
using Newtonsoft.Json;

class Program
{
    public static async Task Main(string[] args)
    {
        var host = "acstopay.online";
        var url = $"https://{host}/api/testassignments/pan";

        var keyId = "47e8fde35b164e888a57b6ff27ec020f";
        var sharedKey = "ac/1LUdrbivclAeP67iDKX2gPTTNmP0DQdF+0LBcPE/3NWwUqm62u5g6u+GE8uev5w/VMowYXN8ZM+gWPdOuzg==";

        do
        {
            //var pan = "4486441729154030";
            Console.WriteLine("Write card number:");

            var pan = Console.ReadLine();
            if (string.IsNullOrEmpty(pan) || !pan.IsValid())
            {
                Console.WriteLine("Wrong card number. Press Enter to continue or Esc to exit");
                continue;
            }

            var request = new RequestModel
            {
                CardInfo = new CardInfoModel { Pan = pan }
            };

            var jws = JwsHelper.CreateJws(keyId, sharedKey, request);

            try
            {
                var response = await ApiClient.SendRequest(url, request, jws);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    var jwtParts = responseContent.Split('.');
                    if (jwtParts.Length == 3)
                    {
                        var decodedPayload = JwsHelper.DecodeJwsElement(jwtParts[1]);
                        var respInfo = JsonConvert.DeserializeObject<ResponseModel>(decodedPayload);

                        var status = respInfo?.Status == Status.Success
                            ? "Successfully"
                            : "Unsuccessfully";
                        Console.Write(status);
                    }
                }
                else
                    Console.WriteLine("Something went wrong. Try again.");
            }
            catch
                Console.WriteLine("Something went wrong. Try again.");

            Console.WriteLine();
            Console.WriteLine("Press Enter to continue or Esc to exit");

        } while (Console.ReadKey().Key != ConsoleKey.Escape);
    }
}
