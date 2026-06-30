using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Digitamentos.Services
{
    public class OfflineFallbackData
    {
        public List<string> Prose { get; set; } = new List<string>();
        public Dictionary<string, Dictionary<string, List<string>>> Code { get; set; } = new Dictionary<string, Dictionary<string, List<string>>>();
    }

    public class OfflineTextGenerator : ITextProvider
    {
        private readonly Random _random = new Random();
        private OfflineFallbackData _fallbackData = new OfflineFallbackData();

        public OfflineTextGenerator()
        {
            string fallbackPath = "texts_fallback.json";
            if (File.Exists(fallbackPath))
            {
                try
                {
                    string json = File.ReadAllText(fallbackPath);
                    var data = JsonSerializer.Deserialize<OfflineFallbackData>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    if (data != null)
                        _fallbackData = data;
                }
                catch { }
            }

            if (_fallbackData.Prose == null || _fallbackData.Prose.Count == 0)
            {
                _fallbackData.Prose = new List<string> {
                    "A imaginação é mais importante que o conhecimento.",
                    "O sucesso é ir de fracasso em fracasso sem perder o entusiasmo."
                };
            }
        }

        public Task<string> GenerateTextAsync(TextMode mode, TextLength length, TextDifficulty difficulty, string language = "")
        {
            if (mode == TextMode.RealText || mode == TextMode.Random)
            {
                return Task.FromResult(GetRealText(length));
            }
            else if (mode == TextMode.Code)
            {
                return Task.FromResult(GetCodeSnippet(language, difficulty, length));
            }

            return Task.FromResult("Erro: Modo não suportado pelo OfflineTextGenerator.");
        }

        private string GetRealText(TextLength length)
        {
            var options = _fallbackData.Prose;
            string result = options[_random.Next(options.Count)];

            if (length == TextLength.Medium)
            {
                result += " " + options[_random.Next(options.Count)];
            }
            else if (length == TextLength.Long)
            {
                result += " " + options[_random.Next(options.Count)] + " " + options[_random.Next(options.Count)];
            }

            return result;
        }

        private string GetCodeSnippet(string language, TextDifficulty difficulty, TextLength length)
        {
            string diffKey = difficulty == TextDifficulty.Basic ? "Basic" : "Hard";
            var snippets = new List<string>();

            if (_fallbackData.Code != null && _fallbackData.Code.ContainsKey(language) && _fallbackData.Code[language].ContainsKey(diffKey))
            {
                snippets = _fallbackData.Code[language][diffKey];
            }

            if (snippets == null || snippets.Count == 0)
            {
                snippets = new List<string> { $"// Algoritmo em {language}\nfunction main() {{\n    return true;\n}}" };
            }

            string result = snippets[_random.Next(snippets.Count)];

            if (length == TextLength.Long)
            {
                result += "\n\n" + snippets[_random.Next(snippets.Count)];
            }

            return result;
        }
    }
}
