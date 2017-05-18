using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ReckonMe.Models.Security;

namespace ReckonMe.Helpers
{
    public interface IDecodeToken
    {
        Task<Token> Decode(HttpResponseMessage message);
    }

    public class TokenDecoder : IDecodeToken
    {
        public async Task<Token> Decode(HttpResponseMessage message)
        {
            var json = new StreamReader(await message.Content.ReadAsStreamAsync()).ReadToEnd();

            return JsonConvert.DeserializeObject<Token>(json);
        }
    }
}