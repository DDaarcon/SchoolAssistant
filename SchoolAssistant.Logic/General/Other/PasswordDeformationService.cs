using Microsoft.Extensions.Configuration;

namespace SchoolAssistant.Logic.General.Other
{
    public interface IPasswordDeformationService
    {
        char[] AllowedChars { get; }

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

        public char[] AllowedChars { get; private set; }

        public PasswordDeformationService(
            IConfiguration? config)
        {
            _config = config;

            var passwordDefSec = _config?.GetSection("PasswordDeformation");

            _increaseBy = passwordDefSec?.GetValue<int>("increaseCharBy") ?? 10;
            _shiftBytesBy = passwordDefSec?.GetValue<int>("shiftCharBytesBy") ?? 3;
            _decreaseBy = passwordDefSec?.GetValue<int>("decreaseCharBy") ?? 15;

            AllowedChars = GetAllowedChars();
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

            var increased = ForEach(text.ToCharArray(), (c, index) =>
            {
                int oldIdx = Array.IndexOf(AllowedChars, c);
                int newIdx = GetShiftedIndex(oldIdx, _increaseBy * multipl, AllowedChars.Length);
                return AllowedChars[newIdx];
            });

            var shifting = ForEach(increased, (c, index) =>
            {
                // setting bytes of [index] to [index + shiftCIndex] and reversing them
                var thisCBytes = BitConverter.GetBytes(c);
                int shiftCIndex = GetShiftedIndex(index, _shiftBytesBy * multipl, increased.Length);
                var shiftCBytes = BitConverter.GetBytes(increased[shiftCIndex]);

                return BitConverter.ToChar(new byte[]
                {
                    shiftCBytes[1],
                    shiftCBytes[0]
                });
            });

            var decreased = ForEach(text.ToCharArray(), (c, index) =>
            {
                int oldIdx = Array.IndexOf(AllowedChars, c);
                int newIdx = GetShiftedIndex(oldIdx, _decreaseBy * multipl * -1, AllowedChars.Length);
                return AllowedChars[newIdx];
            });

            return new string(decreased);
        }

        private char[] ForEach(char[] text, Func<char, int, char> forEach)
        {
            var chars = new char[text.Length];
            for (int i = 0; i < text.Length; i++)
                chars[i] = forEach.Invoke(text[i], i);
            return chars;
        }

        private int GetShiftedIndex(int index, int shift, int length)
        {
            int newIndex = (index + shift) % length;
            if (newIndex < 0) newIndex = length + newIndex;
            return newIndex;
        }



        private char[] GetAllowedChars()
        {
            return CharsInRange('0', '9')
                .Concat(CharsInRange('A', 'Z'))
                .Concat(CharsInRange('a', 'z'))
                .Concat(CharsInRange('_', '_')).ToArray();
        }

        private char[] CharsInRange(char from, char to)
        {
            var chars = new List<char>();
            while (from <= to)
                chars.Add(from++);
            return chars.ToArray();
        }
    }
}
