using RegistrationChallenge.Model.Service;

namespace RegistrationChallenge.Stub
{
    public class EncoderStub : IEncoder
    {
        public string Encode(string data)
        {
            // шифровать в хэш-код объекта
            return data.GetHashCode().ToString();
        }
    }
}
