using RegistrationChallenge.Model.Service;
using System.Security.Cryptography;
using System.Text;

namespace RegistrationChallenge.Model.Crypto
{
    //Имплементация кодирования в MD5
    public class EncodingToMD5 : IEncoder
    {
        public string Encode(string data)
        {
            byte[] hash = Encoding.ASCII.GetBytes(data);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] hashenc = md5.ComputeHash(hash);
            string result = "";
            foreach (var b in hashenc)            
                result += b.ToString("x2");
            
            return result;
        }
    }
}
