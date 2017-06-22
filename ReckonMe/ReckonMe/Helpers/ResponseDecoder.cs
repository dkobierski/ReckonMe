using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ReckonMe.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public class ResponseDecoder
    {
        /// <summary>
        /// Decodes the specified message.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public static async Task<T> Decode<T>(HttpResponseMessage message)
        {
            var json = new StreamReader(await message.Content.ReadAsStreamAsync()).ReadToEnd();

            return JsonConvert.DeserializeObject<T>(json);
        }
    }

}