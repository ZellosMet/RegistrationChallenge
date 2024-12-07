namespace RegistrationChallenge.Model.Service
{
    // IEncoder - интерфейс шифрования данных
    public interface IEncoder
    {
        // Encode - кодирование строки data в строку
        string Encode(string data);
    }
}
