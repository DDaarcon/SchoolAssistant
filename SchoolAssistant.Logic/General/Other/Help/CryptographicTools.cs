using System.Security.Cryptography;
using System.Text;

namespace SchoolAssistant.Logic.General.Other.Help
{
    public interface ICryptographicTools : IDisposable
    {
        byte[] Key { get; set; }

        string Decode(string text);
        string Encode(string text);
        void GenerateKey();
    }

    [Injectable]
    public sealed class CryptographicTools : ICryptographicTools, IDisposable
    {
        public CryptographicTools()
        {
            _aes = Aes.Create();
            _aes.IV = Encoding.ASCII.GetBytes(IV_SAMPLE);
            _aes.Mode = CipherMode.CBC;

            _mainStream = new MemoryStream();
        }


        private readonly string IV_SAMPLE = "F5502320F8429037";

        private readonly Aes _aes;
        private readonly MemoryStream _mainStream;
        private CryptoStream? _crypto;

        private readonly byte[] _allowedBytes = Encoding.ASCII.GetBytes("qwertyuiopasdfghjklzxcvbnm1234567890QWERTYUIOPASDFGHJKLZXCVBNM!@#*&-=+_");


        public void GenerateKey()
        {
            _aes.GenerateKey();
            _aes.Key = _aes.Key
                .Select((b, index) => _allowedBytes.Contains(b) ? b : _allowedBytes[index % _allowedBytes.Length])
                .ToArray();
        }

        public byte[] Key
        {
            get
            {
                return _aes.Key;
            }
            set
            {
                _aes.Key = value;
            }
        }


        public string Encode(string text)
        {
            var textB = Encoding.UTF8.GetBytes(text);
            _mainStream.Write(textB, 0, textB.Length);

            if (_crypto is not null)
                throw new CryptographicProcessException("Tools were already used for decoding");

            _crypto = new CryptoStream(
                _mainStream,
                _aes.CreateEncryptor(Key, _aes.IV),
                CryptoStreamMode.Write);

            var bytes = _mainStream.ToArray();
            var parsed = Convert.ToBase64String(bytes);

            _crypto?.Close();
            return parsed;
        }

        public string Decode(string text)
        {
            var textB = Convert.FromBase64String(text);
            _mainStream.Write(textB, 0, textB.Length);

            if (_crypto is not null)
                throw new CryptographicProcessException("Tools were already used for encoding");

            _crypto = new CryptoStream(
                _mainStream,
                _aes.CreateDecryptor(Key, _aes.IV),
                CryptoStreamMode.Write);

            var bytes = _mainStream.ToArray();
            var parsed = Encoding.UTF8.GetString(bytes);

            _crypto?.Close();
            return parsed;
        }


        public void Dispose()
        {
            _aes.Dispose();
            _mainStream.Dispose();
            _crypto?.Dispose();
        }
        private byte[] UTF8toBytes(string text) => Encoding.UTF8.GetBytes(text);
    }
}
