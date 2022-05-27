using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAssistant.Logic.General.Other
{
    public interface IPasswordDeformationService
    {
        string GetDeformed(string password);
        string GetReadable(string deformed);
    }

    [Injectable]
    public class PasswordDeformationService : IPasswordDeformationService
    {
        private readonly IConfiguration? _config;

        private readonly int _increaseBy;
        private readonly int _shiftBytesBy;
        private readonly int _decreaseBy;

        public PasswordDeformationService(
            IConfiguration? config)
        {
            _config = config;

            var passwordDefSec = _config?.GetSection("PasswordDeformation");

            _increaseBy = passwordDefSec?.GetValue<int>("increaseCharBy") ?? 10;
            _shiftBytesBy = passwordDefSec?.GetValue<int>("shiftCharBytesBy") ?? 3;
            _decreaseBy = passwordDefSec?.GetValue<int>("decreaseCharBy") ?? 15;
        }

        public string GetDeformed(string password)
        {
            return TransformString(password, false);
        }

        public string GetReadable(string deformed)
        {
            return TransformString(deformed, true);
        }

        private string TransformString(string text, bool rev)
        {
            int multipl = rev ? -1 : 1;

            var increased = ForEach(text.ToCharArray(), (c, index) => (char)(c + (char)_increaseBy * multipl));

            var shifting = ForEach(increased, (c, index) =>
            {
                // setting bytes of [index] to [index + shiftCIndex] and reversing them
                var thisCBytes = BitConverter.GetBytes(c);
                int shiftCIndex = (index + _shiftBytesBy * multipl) % increased.Length;
                if (shiftCIndex < 0) shiftCIndex = increased.Length + shiftCIndex;
                var shiftCBytes = BitConverter.GetBytes(increased[shiftCIndex]);

                return BitConverter.ToChar(new byte[]
                {
                    shiftCBytes[1],
                    shiftCBytes[0]
                });
            });

            var decreased = ForEach(text.ToCharArray(), (c, index) => (char)(c - (char)_decreaseBy * multipl));

            return new string(decreased);
        }

        private char[] ForEach(char[] text, Func<char,int, char> forEach)
        {
            var chars = new char[text.Length];
            for (int i = 0; i < text.Length; i++)
                chars[i] = forEach.Invoke(text[i], i);
            return chars;
        }
    }
}
