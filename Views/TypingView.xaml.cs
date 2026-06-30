using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Digitamentos.ViewModels;

namespace Digitamentos.Views
{
    public partial class TypingView : UserControl
    {
        private TypingViewModel _viewModel;
        private string _lastBuiltTarget = "";

        public TypingView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            _viewModel = DataContext as TypingViewModel;
            if (_viewModel != null)
            {
                _viewModel.OnTextChanged = UpdateTextBlock;
                UpdateTextBlock();
            }
            this.Focus();
        }

        private void UpdateTextBlock()
        {
            if (_viewModel == null) return;
            string target = _viewModel.TargetText;
            string typed = _viewModel.TypedText;

            // Rebuild inlines only if target text changes
            if (target != _lastBuiltTarget)
            {
                BuildInlines(target);
                _lastBuiltTarget = target;
            }

            UpdateInlines(target, typed);
            UpdateCursorPosition(typed.Length);
        }

        private void BuildInlines(string target)
        {
            DisplayTextBlock.Inlines.Clear();
            foreach (char c in target)
            {
                DisplayTextBlock.Inlines.Add(new Run(c.ToString()));
            }
        }

        private void UpdateInlines(string target, string typed)
        {
            Brush defaultBrush = (Application.Current.TryFindResource("TypingTextDefaultBrush") as Brush) ?? new SolidColorBrush(Color.FromRgb(98, 114, 164));
            Brush correctBrush = (Application.Current.TryFindResource("TypingTextCorrectBrush") as Brush) ?? new SolidColorBrush(Color.FromRgb(248, 248, 242));
            Brush errorBrush = (Application.Current.TryFindResource("TypingTextIncorrectBrush") as Brush) ?? new SolidColorBrush(Color.FromRgb(255, 85, 85));
            Brush errorBgBrush = new SolidColorBrush(Color.FromArgb(50, 255, 85, 85)); // Keep subtle BG for errors but text is opaque

            int currentIndex = typed.Length;
            if (currentIndex >= target.Length) currentIndex = target.Length - 1;

            for (int i = 0; i < target.Length; i++)
            {
                var run = (Run)DisplayTextBlock.Inlines.ElementAt(i);
                char c = target[i];

                Brush targetFg = null;
                Brush targetBg = Brushes.Transparent;
                TextDecorationCollection targetDecorations = null;
                string targetText = c.ToString();

                if (i < typed.Length)
                {
                    if (typed[i] == c)
                    {
                        targetFg = correctBrush;
                    }
                    else
                    {
                        targetFg = errorBrush;
                        targetBg = errorBgBrush;
                        targetDecorations = TextDecorations.Underline;
                        if (c == ' ') targetText = "_";
                        if (c == '\n') targetText = "↵\n";
                    }
                }
                else
                {
                    targetFg = defaultBrush;
                }

                if (run.Text != targetText) run.Text = targetText;
                if (run.TextDecorations != targetDecorations) run.TextDecorations = targetDecorations;
                if (run.Background != targetBg) run.Background = targetBg;
                if (run.Foreground != targetFg) run.Foreground = targetFg;
            }
        }

        private void UpdateCursorPosition(int index)
        {
            if (DisplayTextBlock.Inlines.Count == 0) return;

            int targetIndex = Math.Min(index, DisplayTextBlock.Inlines.Count - 1);
            var run = (Run)DisplayTextBlock.Inlines.ElementAt(targetIndex);

            // Layout pass is automatically handled, removing sync layout blocking

            Rect rect;
            if (index >= DisplayTextBlock.Inlines.Count)
            {
                // Cursor at the very end
                rect = run.ContentEnd.GetCharacterRect(LogicalDirection.Backward);
            }
            else
            {
                rect = run.ContentStart.GetCharacterRect(LogicalDirection.Forward);
            }

            if (rect.IsEmpty) return;

            double fontSize = _viewModel.MainViewModel.Settings.FontSize;
            var shape = _viewModel.MainViewModel.Settings.CursorShape;

            if (shape == Digitamentos.Models.CursorShape.Line)
            {
                AnimatedCursor.Width = 2;
                AnimatedCursor.Height = rect.Height > 0 ? rect.Height * 0.8 : fontSize * 1.2;
            }
            else if (shape == Digitamentos.Models.CursorShape.Block)
            {
                AnimatedCursor.Width = fontSize * 0.6;
                AnimatedCursor.Height = rect.Height > 0 ? rect.Height * 0.8 : fontSize * 1.2;
            }
            else // Underline
            {
                AnimatedCursor.Width = fontSize * 0.6;
                AnimatedCursor.Height = 3;
            }

            double topOffset = rect.Top + (rect.Height - AnimatedCursor.Height) / 2;
            if (shape == Digitamentos.Models.CursorShape.Underline) topOffset = rect.Bottom;

            TimeSpan animDuration = _viewModel.MainViewModel.Settings.SmoothCursor ? TimeSpan.FromMilliseconds(100) : TimeSpan.Zero;
            var animX = new DoubleAnimation(rect.Left, animDuration) { EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseOut } };
            var animY = new DoubleAnimation(topOffset, animDuration) { EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseOut } };

            CursorTransform.BeginAnimation(TranslateTransform.XProperty, animX);
            CursorTransform.BeginAnimation(TranslateTransform.YProperty, animY);
        }

        private Brush CloneBrush(Brush original, double opacity)
        {
            var brush = original.Clone();
            brush.Opacity = opacity;
            return brush;
        }

        private void UserControl_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (_viewModel == null || string.IsNullOrEmpty(e.Text)) return;
            char c = e.Text[0];
            if (c >= 32 || c == '\n')
            {
                _viewModel.ProcessKeystroke(c, false);
                e.Handled = true;
            }
        }

        private void UserControl_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (_viewModel == null) return;

            if (e.Key == Key.Back)
            {
                e.Handled = true;
                _viewModel.ProcessKeystroke('\0', true);
            }
            else if (e.Key == Key.Enter)
            {
                e.Handled = true;
                _viewModel.ProcessKeystroke('\n', false, true);
            }
            else if (e.Key == Key.Space)
            {
                e.Handled = true;
                _viewModel.ProcessKeystroke(' ', false);
            }
            else if (e.Key == Key.Tab)
            {
                e.Handled = true;
                _viewModel.ProcessTab();
            }
        }
    }
}
