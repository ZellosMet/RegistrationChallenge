using RegistrationChallenge.Model.Service;

namespace RegistrationChallenge.Model.Crypto
{
    //Имплементация кодирования в BCrypt
    public class EncodingToBCrypt : IEncoder
    {
        public string Encode(string data)
        { 
            //!!!!Проблема в кодировании, каждый раз при кодировании возвращается новый результат!!!!
            string bcript = BCrypt.Net.BCrypt.HashPassword(data);
            return bcript;        
        }
    }
}
