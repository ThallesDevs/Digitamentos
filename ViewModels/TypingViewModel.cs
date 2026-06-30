using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using CommunityToolkit.Mvvm.Input;
using Digitamentos.Models;
using Digitamentos.Services;

namespace Digitamentos.ViewModels
{
    public class TypingViewModel : BaseViewModel
    {
        private readonly MainViewModel _mainViewModel;
        public MainViewModel MainViewModel => _mainViewModel;

        // === ComboBox Option Lists ===
        public ObservableCollection<string> ModeOptions { get; } = new ObservableCollection<string>
        {
            "aleatório", "real", "código"
        };

        public ObservableCollection<string> LengthOptions { get; } = new ObservableCollection<string>
        {
            "curto", "médio", "longo"
        };

        public ObservableCollection<string> DifficultyOptions { get; } = new ObservableCollection<string>
        {
            "básico", "avançado"
        };

        public ObservableCollection<string> AvailableLanguages { get; } = new ObservableCollection<string>
        {
            "Python", "C#", "JavaScript", "C++", "Java", "Go", "Rust", "HTML", "SQL"
        };

        // === Internal enum state ===
        private TextMode _selectedMode = TextMode.RealText;
        public TextMode SelectedMode
        {
            get => _selectedMode;
            set
            {
                if (SetProperty(ref _selectedMode, value))
                {
                    OnPropertyChanged(nameof(IsLanguageSelectionVisible));
                    OnPropertyChanged(nameof(IsDifficultyEnabled));
                    OnPropertyChanged(nameof(DifficultyOpacity));
                    OnPropertyChanged(nameof(DifficultyBasicLabel));
                    OnPropertyChanged(nameof(DifficultyHardLabel));
                }
            }
        }

        private TextLength _selectedLength = TextLength.Medium;
        public TextLength SelectedLength
        {
            get => _selectedLength;
            set => SetProperty(ref _selectedLength, value);
        }

        private TextDifficulty _selectedDifficulty = TextDifficulty.Basic;
        public TextDifficulty SelectedDifficulty
        {
            get => _selectedDifficulty;
            set => SetProperty(ref _selectedDifficulty, value);
        }

        // === Display string bridge properties (ComboBox binds to these) ===
        private string _selectedModeDisplay = "real";
        public string SelectedModeDisplay
        {
            get => _selectedModeDisplay;
            set
            {
                if (SetProperty(ref _selectedModeDisplay, value))
                {
                    SelectedMode = value switch
                    {
                        "aleatório" => TextMode.Random,
                        "real" => TextMode.RealText,
                        "código" => TextMode.Code,
                        _ => TextMode.RealText
                    };
                }
            }
        }

        private string _selectedLengthDisplay = "médio";
        public string SelectedLengthDisplay
        {
            get => _selectedLengthDisplay;
            set
            {
                if (SetProperty(ref _selectedLengthDisplay, value))
                {
                    SelectedLength = value switch
                    {
                        "curto" => TextLength.Short,
                        "médio" => TextLength.Medium,
                        "longo" => TextLength.Long,
                        _ => TextLength.Medium
                    };
                }
            }
        }

        private string _selectedDifficultyDisplay = "básico";
        public string SelectedDifficultyDisplay
        {
            get => _selectedDifficultyDisplay;
            set
            {
                if (SetProperty(ref _selectedDifficultyDisplay, value))
                {
                    SelectedDifficulty = value switch
                    {
                        "básico" => TextDifficulty.Basic,
                        "avançado" => TextDifficulty.Hard,
                        _ => TextDifficulty.Basic
                    };
                }
            }
        }

        // === Computed UI properties ===
        public bool IsLanguageSelectionVisible => SelectedMode == TextMode.Code;
        public bool IsDifficultyEnabled => SelectedMode != TextMode.RealText;
        public double DifficultyOpacity => SelectedMode == TextMode.RealText ? 0.2 : 1.0;

        public string DifficultyBasicLabel => SelectedMode == TextMode.RealText ? "básico (não aplicável)" : "básico";
        public string DifficultyHardLabel => SelectedMode == TextMode.RealText ? "avançado (não aplicável)" : "avançado";
        private string _selectedLanguage = "C#";
        public string SelectedLanguage
        {
            get => _selectedLanguage;
            set => SetProperty(ref _selectedLanguage, value);
        }
        // Typing State
        private Stopwatch _stopwatch;
        private DispatcherTimer _timer;

        private string _targetText = "";
        public string TargetText
        {
            get => _targetText;
            set
            {
                if (SetProperty(ref _targetText, value))
                {
                    OnTextChanged?.Invoke();
                }
            }
        }

        private string _typedText = "";
        public string TypedText
        {
            get => _typedText;
            set
            {
                SetProperty(ref _typedText, value);
                OnTypedTextChanged();
            }
        }

        private bool _isLoading = false;
        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        private string _timeDisplay = "00:00";
        public string TimeDisplay
        {
            get => _timeDisplay;
            set => SetProperty(ref _timeDisplay, value);
        }

        private bool _isTestStarted = false;
        public bool IsTestStarted
        {
            get => _isTestStarted;
            set => SetProperty(ref _isTestStarted, value);
        }

        public ICommand RestartCommand { get; }
        public ICommand NewTextCommand { get; }

        private TypingMetrics _metrics;
        public Action? OnTextChanged;

        public TypingViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;

            RestartCommand = new RelayCommand(RestartTest);
            NewTextCommand = new RelayCommand(ChangeText);

            _stopwatch = new Stopwatch();
            _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            _timer.Tick += (s, e) =>
            {
                UpdateTimeDisplay();
                RecordWpmSample();
            };

            ChangeText();
        }

        private void RecordWpmSample()
        {
            var ts = _stopwatch.Elapsed;
            double segundos = ts.TotalSeconds;
            if (segundos < 1.0)
            {
                segundos = 1.0;
            }
            double minutos = segundos / 60.0;

            double currentTpm = (double)_metrics.TotalKeystrokes / minutos;
            int tpmFinal = (int)Math.Round(currentTpm);

            _metrics.WpmHistory.Add(new Tuple<double, double>(ts.TotalSeconds, tpmFinal));
        }

        private void RestartTest()
        {
            _stopwatch.Stop();
            _timer.Stop();
            _stopwatch.Reset();

            _metrics = new TypingMetrics();
            _metrics.TotalProposedCharacters = TargetText.Length;
            TypedText = "";
            IsTestStarted = false;
            TimeDisplay = "00:00";
        }

        private void ChangeText()
        {
            _stopwatch.Stop();
            _timer.Stop();
            _stopwatch.Reset();

            _metrics = new TypingMetrics();
            TypedText = "";
            IsTestStarted = false;
            TimeDisplay = "00:00";

            _ = LoadTextAsync();
        }

        private async Task LoadTextAsync()
        {
            IsLoading = true;

            ITextProvider textProvider;
            if (SelectedMode == TextMode.Random)
            {
                textProvider = new RandomTextGenerator();
            }
            else
            {
                textProvider = new OfflineTextGenerator();
            }

            TargetText = await textProvider.GenerateTextAsync(SelectedMode, SelectedLength, SelectedDifficulty, SelectedLanguage);
            TargetText = TargetText.Replace("\r\n", "\n").Replace("\r", "\n");
            _metrics.TotalProposedCharacters = TargetText.Length;

            IsLoading = false;
        }

        private void UpdateTimeDisplay()
        {
            var ts = _stopwatch.Elapsed;
            TimeDisplay = $"{(int)ts.TotalMinutes:D2}:{ts.Seconds:D2}";
        }

        public void ProcessTab()
        {
            if (IsLoading) return;
            if (TypedText.Length >= TargetText.Length) return;

            int currentIndex = TypedText.Length;
            int spaceCount = 0;

            // Count how many consecutive spaces are next, up to 4.
            while (currentIndex + spaceCount < TargetText.Length && 
                   TargetText[currentIndex + spaceCount] == ' ' && 
                   spaceCount < 4)
            {
                spaceCount++;
            }

            if (spaceCount > 0)
            {
                // Simulate typing those spaces correctly
                for (int i = 0; i < spaceCount; i++)
                {
                    ProcessKeystroke(' ', false);
                }
            }
            else
            {
                // Fallback: process a standard tab character (will likely register as an error if target is not a tab)
                ProcessKeystroke('\t', false);
            }
        }

        public void ProcessKeystroke(char c, bool isBackspace, bool isEnter = false)
        {
            if (IsLoading) return;
            if (TypedText.Length >= TargetText.Length && !isBackspace) return;

            if (!IsTestStarted && !isBackspace)
            {
                IsTestStarted = true;
            }

            if (!_stopwatch.IsRunning && !isBackspace)
            {
                _stopwatch.Start();
                _timer.Start();
            }

            if (isBackspace)
            {
                if (TypedText.Length > 0)
                {
                    TypedText = TypedText.Substring(0, TypedText.Length - 1);
                    _metrics.TotalKeystrokes++;
                }
            }
            else
            {
                _metrics.TotalKeystrokes++;

                int currentIndex = TypedText.Length;
                char expectedChar = TargetText[currentIndex];
                bool isCorrect = (c == expectedChar);

                if (!isCorrect)
                {
                    _metrics.IncorrectKeystrokes++;
                    double segundos = _stopwatch.Elapsed.TotalSeconds;
                    if (segundos < 1.0)
                    {
                        segundos = 1.0;
                    }
                    double minutos = segundos / 60.0;
                    double currentTpm = (double)_metrics.TotalKeystrokes / minutos;
                    int tpmFinal = (int)Math.Round(currentTpm);

                    _metrics.ErrorPoints.Add(new Tuple<double, double>(_stopwatch.Elapsed.TotalSeconds, tpmFinal));
                }

                TypedText += c;

                if (TypedText.Length == TargetText.Length)
                {
                    FinishTest();
                }
            }
        }

        private void OnTypedTextChanged()
        {
            OnTextChanged?.Invoke();
        }

        private void FinishTest()
        {
            _stopwatch.Stop();
            _timer.Stop();

            int finalCorrect = 0;
            for (int i = 0; i < TargetText.Length; i++)
            {
                if (TargetText[i] == TypedText[i])
                    finalCorrect++;
            }
            _metrics.CorrectKeystrokes = finalCorrect;
            _metrics.TotalTime = _stopwatch.Elapsed;

            _mainViewModel.NavigateToResults(_metrics);
        }
    }
}
