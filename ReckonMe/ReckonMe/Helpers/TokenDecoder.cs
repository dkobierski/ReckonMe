using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ReckonMe.Models.Security;

namespace ReckonMe.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDecodeToken
    {
        /// <summary>
        /// Decodes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        Task<Token> Decode(HttpResponseMessage message);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="ReckonMe.Helpers.IDecodeToken" />
    public class TokenDecoder : IDecodeToken
    {
        /// <summary>
        /// Decodes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public async Task<Token> Decode(HttpResponseMessage message)
        {
            var json = new StreamReader(await message.Content.ReadAsStreamAsync()).ReadToEnd();

            return JsonConvert.DeserializeObject<Token>(json);
        }
    }
}