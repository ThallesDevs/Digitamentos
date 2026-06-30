using System.Threading.Tasks;

namespace Digitamentos.Services
{
    public enum TextLength { Short, Medium, Long }
    public enum TextDifficulty { Basic, Medium, Hard }
    public enum TextMode { Random, RealText, Code }

    public interface ITextProvider
    {
        Task<string> GenerateTextAsync(TextMode mode, TextLength length, TextDifficulty difficulty, string language = "");
    }
}
