using System.Configuration;
using System.Data;
using System.Windows;
using WPFThemingDemo.Services;

namespace WPFThemingDemo;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        
        // 初始化主题服务
        var themeService = ThemeService.Instance;
    }
}

