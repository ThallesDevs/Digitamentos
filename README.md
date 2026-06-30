# Digitamentos ⌨️🚀

![Licença](https://img.shields.io/badge/License-MIT-blue.svg)
![.NET 8](https://img.shields.io/badge/.NET-8.0-purple.svg)
![WPF](https://img.shields.io/badge/WPF-Windows-blue)

**Digitamentos** é um software de treino de digitação moderno, rápido e personalizável para Windows, construído sob a plataforma .NET 8 (WPF). Ele integra design minimalista e moderno — suportado pelo *ModernWpfUI* —, testes de digitação rigorosos com telemetria exata, e geração dinâmica de textos turbinada por Inteligência Artificial.

## ✨ Principais Funcionalidades

- **Design Premium & Minimalista:** Uma interface limpa, com estética de terminal *Glassmorphism*, suporte a Dark/Light mode dinâmico e opções robustas de personalização visual (cores customizadas em HEX, animações de cursor, fontes e espaçamentos).
- **Métricas Matemáticas de Alta Precisão:** Cálculo rigoroso e blindado de **TPM** (Teclas por Minuto) e **WPM** (Palavras por Minuto), processando precisão, teclas erradas, gráficos em tempo real de decaimento de ritmo e velocidade.
- **Geração de Textos por Inteligência Artificial:** Ao invés de decorar frases estáticas, o sistema consome *endpoints* de IA (Google Gemini e outros provedores) para gerar parágrafos sobre tópicos aleatórios, trechos de código em C#, Python, JavaScript, ou histórias curtas de diferentes graus de complexidade.
- **Modo Offline & Scripts Nativos:** Um gerador *offline* local garante funcionamento contínuo, mesmo quando sem internet.
- **Gráficos em Tempo Real:** Tela de resultados exibindo plotagens interativas construídas usando *OxyPlot*.

## 🛠️ Tecnologias Utilizadas

- **C# / .NET 8.0:** Alta performance, runtime otimizado e base orientada a objetos.
- **WPF (Windows Presentation Foundation):** Para construção da interface nativa para Windows.
- **MVVM Pattern:** Arquitetura sólida viabilizada pelo `CommunityToolkit.Mvvm`, separando de forma clara lógicas de interface (XAML) das regras de negócio (ViewModels e Models).
- **Bibliotecas:**
  - `ModernWpfUI`: Para as janelas modernas estilo Windows 11 Fluent Design.
  - `OxyPlot.Wpf`: Para as plotagens dos gráficos de progresso.
  - `Newtonsoft.Json`: Serialização e parsing de configurações.

## 🚀 Como Executar Localmente

### Pré-requisitos
- Sistema Operacional: Windows 10/11
- [.NET 8.0 Desktop Runtime / SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

### Passos de Instalação

1. Clone o repositório em sua máquina:
```bash
git clone https://github.com/SeuUsuario/Digitamentos.git
```
2. Abra a pasta do projeto:
```bash
cd Digitamentos
```
3. (Opcional) Faça o Build Standalone da aplicação para gerar um `.exe` direto e sem dependências separadas:
```bash
dotnet publish -c Release -r win-x64 -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true --self-contained true -o .\Publish
```
4. Caso queira rodar diretamente os fontes pelo terminal:
```bash
dotnet run -c Release
```

## ⚙️ Configurando a IA (Opcional)
Para habilitar a geração de textos dinâmica baseada em IA:
1. Abra a aplicação.
2. Acesse o menu lateral de **Configurações**.
3. Insira sua chave de API do **Google Gemini** (Gere uma gratuitamente no [Google AI Studio](https://aistudio.google.com/)).
4. Sem a chave, o app vai realizar o *fallback* para o gerador dinâmico de textos nativo `OfflineTextGenerator`.

## 🤝 Contribuições
Contribuições são super bem-vindas! Se você tem ideias para novos modos de treino de digitação, gráficos extras, ou otimizações, fique à vontade para abrir uma *Issue* ou submeter um *Pull Request*.

## 📄 Licença
Este projeto é distribuído sob a Licença MIT. Veja o arquivo `LICENSE` para mais detalhes.
