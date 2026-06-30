using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Digitamentos.Services
{
    public class OpenAITextGenerator : ITextProvider
    {
        private readonly string _apiKey;
        private readonly HttpClient _httpClient;

        public OpenAITextGenerator(string apiKey)
        {
            _apiKey = apiKey;
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");
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
                prompt = $"Gere um texto real com sentido (pode ser um trecho de um livro, artigo ou notícia), em português, com aproximadamente {approxWords} palavras. Retorne apenas o texto, sem aspas ou comentários extras.";
            }
            else if (mode == TextMode.Code)
            {
                string difficultyLevel = difficulty == TextDifficulty.Basic ? "sintaxes simples como declaração de variáveis ou estruturas básicas" : "estruturas densas, algoritmos ou classes";
                prompt = $"Gere um trecho de código fonte válido na linguagem {language}. Nível de complexidade: {difficultyLevel}. O código deve ter tamanho equivalente a aproximadamente {approxWords} palavras de código (linhas proporcionais). Retorne apenas o código, sem formatação markdown (sem ```).";
            }
            else
            {
                throw new ArgumentException("Unsupported mode for OpenAITextGenerator");
            }

            var requestBody = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
                    new { role = "system", content = "Você é um gerador de textos e códigos estrito. Retorne apenas o conteúdo solicitado, sem explicações ou introduções." },
                    new { role = "user", content = prompt }
                },
                temperature = 0.7,
                max_tokens = approxWords * 4
            };

            var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

            try
            {
                var response = await _httpClient.PostAsync("https://api.openai.com/v1/chat/completions", content);
                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();
                var jsonResponse = JObject.Parse(responseString);

                string generatedText = jsonResponse["choices"]?[0]?["message"]?["content"]?.ToString() ?? string.Empty;
                return generatedText.Trim();
            }
            catch (Exception ex)
            {
                return $"[Erro ao gerar texto com IA. Verifique sua conexão ou API Key.]\nDetalhes: {ex.Message}";
            }
        }
    }
}
