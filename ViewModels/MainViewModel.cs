using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using Digitamentos.Models;
using Digitamentos.Services;
using ModernWpf;

namespace Digitamentos.ViewModels
{
    public partial class MainViewModel : BaseViewModel
    {
        private BaseViewModel _currentViewModel;
        public BaseViewModel CurrentViewModel
        {
            get => _currentViewModel;
            set => SetProperty(ref _currentViewModel, value);
        }

        private UserSettings _settings;
        public UserSettings Settings
        {
            get => _settings;
            set => SetProperty(ref _settings, value);
        }

        public System.Collections.ObjectModel.ObservableCollection<string> AvailableFonts { get; } = new System.Collections.ObjectModel.ObservableCollection<string>
        {
            "Consolas", "Courier New", "JetBrains Mono", "Fira Code", "Roboto Mono", "Cascadia Code"
        };

        public System.Collections.ObjectModel.ObservableCollection<string> AvailableThemes { get; } = new System.Collections.ObjectModel.ObservableCollection<string>
        {
            "System", "Light", "Dark", "Dracula", "Nord", "GitHub Dark", "Solarized", "Custom"
        };

        public ICommand ApplySettingsCommand { get; }
        public ICommand ToggleThemeCommand { get; }
        public ICommand NavigateHomeCommand { get; }


        public MainViewModel()
        {
            ToggleThemeCommand = new RelayCommand(ToggleTheme);
            ApplySettingsCommand = new RelayCommand(ApplySettings);

            Settings = Services.SettingsManager.LoadSettings();
            ApplySettings();

            NavigateToHome();
        }

        private void ApplySettings()
        {
            Services.SettingsManager.SaveSettings(Settings);

            string bgHex = "";
            string fgHex = "";
            string defaultHex = "";
            string correctHex = "";
            string errorHex = "";
            string cursorHex = "";

            if (Settings.ThemePreset == "Light")
            {
                ThemeManager.Current.ApplicationTheme = ApplicationTheme.Light;
                bgHex = "#f4f4f5"; fgHex = "#18181b";
                defaultHex = "#a1a1aa"; correctHex = "#18181b"; errorHex = "#e11d48"; cursorHex = "#2563eb";
            }
            else if (Settings.ThemePreset == "Dark")
            {
                ThemeManager.Current.ApplicationTheme = ApplicationTheme.Dark;
                bgHex = "#1e1e2e"; fgHex = "#f8f8f2";
                defaultHex = "#6272a4"; correctHex = "#f8f8f2"; errorHex = "#ff5555"; cursorHex = "#50fa7b";
            }
            else if (Settings.ThemePreset == "System")
            {
                ThemeManager.Current.ApplicationTheme = null;
                bool isDark = ThemeManager.Current.ActualApplicationTheme == ApplicationTheme.Dark;
                bgHex = isDark ? "#1e1e2e" : "#f4f4f5";
                fgHex = isDark ? "#f8f8f2" : "#18181b";
                defaultHex = isDark ? "#6272a4" : "#a1a1aa";
                correctHex = isDark ? "#f8f8f2" : "#18181b";
                errorHex = isDark ? "#ff5555" : "#e11d48";
                cursorHex = isDark ? "#50fa7b" : "#2563eb";
            }
            else if (Settings.ThemePreset == "Dracula")
            {
                ThemeManager.Current.ApplicationTheme = ApplicationTheme.Dark;
                bgHex = "#282a36"; fgHex = "#f8f8f2";
                defaultHex = "#6272a4"; correctHex = "#f8f8f2"; errorHex = "#ff5555"; cursorHex = "#50fa7b";
            }
            else if (Settings.ThemePreset == "Nord") { ThemeManager.Current.ApplicationTheme = ApplicationTheme.Dark; bgHex = "#2e3440"; fgHex = "#eceff4"; defaultHex = "#4c566a"; correctHex = "#eceff4"; errorHex = "#bf616a"; cursorHex = "#88c0d0"; }
            else if (Settings.ThemePreset == "GitHub Dark") { ThemeManager.Current.ApplicationTheme = ApplicationTheme.Dark; bgHex = "#0d1117"; fgHex = "#c9d1d9"; defaultHex = "#8b949e"; correctHex = "#c9d1d9"; errorHex = "#f85149"; cursorHex = "#58a6ff"; }
            else if (Settings.ThemePreset == "Solarized") { ThemeManager.Current.ApplicationTheme = ApplicationTheme.Dark; bgHex = "#002b36"; fgHex = "#839496"; defaultHex = "#586e75"; correctHex = "#839496"; errorHex = "#dc322f"; cursorHex = "#268bd2"; }
            else if (Settings.ThemePreset == "Custom")
            {
                ThemeManager.Current.ApplicationTheme = ApplicationTheme.Dark;
                bgHex = Settings.CustomBackgroundHex;
                fgHex = Settings.CustomForegroundHex;
                defaultHex = "#6272a4"; correctHex = fgHex; errorHex = "#ff5555"; cursorHex = "#50fa7b";
            }

            Action<string, string> SetBrush = (key, hex) =>
            {
                if (!string.IsNullOrWhiteSpace(hex))
                {
                    try
                    {
                        var color = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(hex);
                        System.Windows.Application.Current.Resources[key] = new System.Windows.Media.SolidColorBrush(color);
                    }
                    catch { }
                }
                else
                {
                    System.Windows.Application.Current.Resources.Remove(key);
                }
            };

            SetBrush("SystemControlBackgroundAltHighBrush", bgHex);
            SetBrush("SystemControlPageBackgroundAltHighBrush", bgHex);
            SetBrush("SystemControlForegroundBaseMediumBrush", defaultHex);
            SetBrush("SystemControlForegroundBaseHighBrush", correctHex);
            SetBrush("TypingTextDefaultBrush", defaultHex);
            SetBrush("TypingTextCorrectBrush", correctHex);
            SetBrush("TypingTextIncorrectBrush", errorHex);
            SetBrush("TypingCursorBrush", cursorHex);
        }

        private void ToggleTheme()
        {
            var currentTheme = ThemeManager.Current.ApplicationTheme;
            ThemeManager.Current.ApplicationTheme = currentTheme == ApplicationTheme.Dark ? ApplicationTheme.Light : ApplicationTheme.Dark;
        }

        public void NavigateToHome()
        {
            CurrentViewModel = new TypingViewModel(this);
        }

        public void NavigateToResults(TypingMetrics metrics)
        {
            CurrentViewModel = new ResultsViewModel(this, metrics);
        }
    }
}
