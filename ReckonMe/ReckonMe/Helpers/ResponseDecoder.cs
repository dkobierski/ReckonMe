using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ReckonMe.Helpers
{
    public class ResponseDecoder
    {
        public static async Task<T> Decode<T>(HttpResponseMessage message)
        {
            var json = new StreamReader(await message.Content.ReadAsStreamAsync()).ReadToEnd();

            return JsonConvert.DeserializeObject<T>(json);
        }
    }

}