using AssignmentApp.Extensions;
using AssignmentApp.Helpers;
using AssignmentApp.Models;

class Program
{
    public static async Task Main(string[] args)
    {
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

            var cardInfo = new CardInfoModel { Pan = pan };
            var jws = JwsHelper.CreateJws(keyId, sharedKey, cardInfo);

            await ApiClient.SendRequest(cardInfo, jws);

        } while (Console.ReadKey().Key != ConsoleKey.Escape);
    }
}
