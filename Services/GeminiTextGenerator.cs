using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Digitamentos.Services
{
    public class GeminiTextGenerator : ITextProvider
    {
        private readonly string _apiKey;
        private readonly HttpClient _httpClient;

        public GeminiTextGenerator(string apiKey)
        {
            _apiKey = apiKey;
            _httpClient = new HttpClient();
        }

        public async Task<string> GenerateTextAsync(TextMode mode, TextLength length, TextDifficulty difficulty, string language = "")
        {
            int approxWords = length switch
            {
                TextLength.Short => 25,
                TextLength.Medium => 65,
                TextLength.Long => 150,
                _ => 65
            };

            string prompt = "";

            if (mode == TextMode.RealText)
            {
                prompt = $"Gere um texto real com sentido (pode ser um trecho de um livro, crônica, artigo ou notícia), em português, com aproximadamente {approxWords} palavras. Retorne apenas o texto puro, sem aspas e sem formatação markdown. PROIBIÇÃO ABSOLUTA: Não inclua absolutamente nenhuma linha de código, variáveis, sintaxes de programação, tags ou jargões técnicos. O texto deve ser inteiramente em linguagem natural humana.";
            }
            else if (mode == TextMode.Code)
            {
                string difficultyLevel = difficulty == TextDifficulty.Basic ? "sintaxes simples como declaração de variáveis ou estruturas básicas" : "estruturas densas, algoritmos complexos ou classes avançadas";
                prompt = $"Gere um trecho de código fonte válido na linguagem {language}. Nível de complexidade: {difficultyLevel}. O código deve ter tamanho equivalente a aproximadamente {approxWords} palavras de código (linhas proporcionais). Retorne apenas o código puro, sem formatação markdown (sem ```) e sem explicações. ATENÇÃO: Garanta altíssima variedade, utilize estruturas lógicas criativas e diferentes abordagens (ex: processamento de dados, UI, lógicas matemáticas) para não repetir exemplos comuns.";
            }
            else
            {
                throw new ArgumentException("Unsupported mode for GeminiTextGenerator");
            }

            var requestBody = new
            {
                contents = new[]
                {
                    new
                    {
                        parts = new[]
                        {
                            new { text = prompt }
                        }
                    }
                },
                generationConfig = new
                {
                    temperature = 0.7,
                    maxOutputTokens = approxWords * 4
                }
            };

            string url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash:generateContent?key={_apiKey}";
            var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

            try
            {
                var response = await _httpClient.PostAsync(url, content);
                var responseString = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return $"[Erro da API Google Gemini: {response.StatusCode}]\n{responseString}";
                }

                var jsonResponse = JObject.Parse(responseString);
                string generatedText = jsonResponse["candidates"]?[0]?["content"]?["parts"]?[0]?["text"]?.ToString() ?? string.Empty;
                return generatedText.Trim();
            }
            catch (Exception ex)
            {
                return $"[Erro ao gerar texto com IA. Verifique sua conexão ou API Key do Google.]\nDetalhes: {ex.Message}";
            }
        }
    }
}
