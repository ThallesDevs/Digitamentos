using System;
using System.Text;
using System.Threading.Tasks;

namespace Digitamentos.Services
{
    public class RandomTextGenerator : ITextProvider
    {
        private readonly Random _random = new Random();

        public Task<string> GenerateTextAsync(TextMode mode, TextLength length, TextDifficulty difficulty, string language = "")
        {
            if (mode != TextMode.Random)
                throw new ArgumentException("Invalid mode for RandomTextGenerator");

            int wordCount = length switch
            {
                TextLength.Short => _random.Next(15, 31),
                TextLength.Medium => _random.Next(50, 81),
                TextLength.Long => _random.Next(120, 201),
                _ => 50
            };

            return Task.FromResult(GenerateRandomString(wordCount, difficulty));
        }

        private string GenerateRandomString(int wordCount, TextDifficulty difficulty)
        {
            var sb = new StringBuilder();
            string basicChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string mediumChars = basicChars + ",.:;!?^~´`!@#$%¨&*_-+=";
            string hardChars = mediumChars + "0123456789{}[]()<>/\\|";

            for (int i = 0; i < wordCount; i++)
            {
                int wordLength = _random.Next(3, 9); // 3 to 8 chars
                for (int j = 0; j < wordLength; j++)
                {
                    string charset = difficulty switch
                    {
                        TextDifficulty.Basic => basicChars,
                        TextDifficulty.Medium => _random.NextDouble() > 0.8 ? mediumChars : basicChars,
                        TextDifficulty.Hard => hardChars,
                        _ => basicChars
                    };
                    sb.Append(charset[_random.Next(charset.Length)]);
                }

                if (i < wordCount - 1)
                    sb.Append(' ');
            }

            return sb.ToString();
        }
    }
}
