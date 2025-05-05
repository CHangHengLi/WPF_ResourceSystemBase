using System;
using System.Windows;
using System.Windows.Input;
using WPFThemingDemo.Services;

namespace WPFThemingDemo.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly ThemeService _themeService;
        private string _currentTheme;

        public MainViewModel()
        {
            _themeService = ThemeService.Instance;
            _currentTheme = _themeService.CurrentTheme;

            // 订阅主题变更事件
            _themeService.ThemeChanged += (sender, theme) =>
            {
                CurrentTheme = theme;
            };

            // 命令
            ToggleThemeCommand = new RelayCommand(ToggleTheme);
            ExitCommand = new RelayCommand(Exit);
        }

        public string CurrentTheme
        {
            get => _currentTheme;
            private set => SetProperty(ref _currentTheme, value);
        }

        public bool IsLightTheme => CurrentTheme == "Light";
        public bool IsDarkTheme => CurrentTheme == "Dark";

        public ICommand ToggleThemeCommand { get; }
        public ICommand ExitCommand { get; }

        private void ToggleTheme(object parameter)
        {
            _themeService.ToggleTheme();
            OnPropertyChanged(nameof(IsLightTheme));
            OnPropertyChanged(nameof(IsDarkTheme));
        }

        private void Exit(object parameter)
        {
            Application.Current.Shutdown();
        }
    }
} 